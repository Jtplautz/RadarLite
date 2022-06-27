import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";
import { authStore } from "@/stores/AuthStore";
import LoginView from "../views/LoginView.vue";
import LoggedOutView from "../views/LoggedOutView.vue";
import type { StoreDefinition } from "pinia";
import { authenticate } from "@/Services/AuthenticationService";
import path from "path";
import axios, { AxiosResponse, AxiosRequestConfig } from "axios";
import AxiosInstance from "@/common/AxiosInstance";
import { ErrorResponse, UnauthorizedError } from "@/common/ErrorModels";
import UserSessionModel from "@/common/UserSessionModel";
import { DeserializeArray } from "@/Helpers/JsonMapper";
import CreateAccountVue from "@/components/CreateAccount.vue";
const routes = [
  {
    path: "/",
    name: "home",
    component: HomeView,
  },
  {
    path: "/about",
    name: "about",
    // route level code-splitting
    // this generates a separate chunk (About.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import("../views/AboutView.vue"),
  },
  {
    path: "/login",
    name: "login",
    meta: { requresAuth: false },
    component: LoginView,
  },
  {
    path: "/logout",
    name: "logout",
    meta: { requresAuth: true },
    component: LoggedOutView,
  },
  {
    path: "/create-account",
    name: "create-account",
    meta: { requresAuth: false },
    component: CreateAccountVue,
  },
  {
    path: "/:catchAll(.*)",
    name: "PageNotFound",
    meta: { requresAuth: true },
    component: () => import("../views/PageNotFoundView.vue"),
  },
];
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
});
// router.beforeEach((to, from, next) => {
//   // ✅ This will work because the router starts its navigation after
//   // the router is installed and pinia will be installed too
//   // const store = new authStore();
//   // console.log(store.isAuth);
//   // if (store.isAuth) {
//   //   console.log("authenticated");
//   // } else if (to.matched.some((record) => record.meta.requresAuth)) {
//   //   store.authenticate(to.path);
//   // }
// });

router.beforeEach((to, from, next) => {
  const store = new authStore();
  //move logic into store
  const result = AxiosInstance.get<JsonMapper.IGenericObject[]>("/bff/user", {
    headers: {
      "x-csrf": 1,
    },
  })
    .then((response) => {
      console.log(response.status);
      //401 indicates user is not logged in
      if (response.status === 401) {
        store.isAuth = false;
        //if they are going to log in and aren't auth, let them
        if (to.name === "login" && !store.isAuth) {
          window.location.href = "/bff/login";
        }
        if (to.name === "create-account" && !store.isAuth) {
          window.location.href = "/bff/signup";
          //next();
        }
      }
      //if the response is a 200, then the user is logged in
      if (response.status === 200) {
        store.isAuth = true;
        const val = DeserializeArray(UserSessionModel, response.data);

        //if they are logged in and want to log out, let them
        if (to.name === "logout" && store.isAuth) {
          //make sure our session link is valid and take them there
          if (val[8].value !== "") {
            window.location.href = "" + val[8].value;
          } else {
            //we don't know what went wrong
            throw new UnauthorizedError(
              "There was an issue with validating the session.",
              500
            );
          }
        }

        //if they aren't going to login and log out and they are logged in, let them
        if (to.name !== "login" && store.isAuth) {
          next();
        }
      }
    })
    .catch();
});

export default router;
