import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";
import { authStore } from "@/stores/AuthStore";
import LoginView from "../views/LoginView.vue";
import type { StoreDefinition } from "pinia";
import { authenticate } from "@/Services/AuthenticationService";
import path from "path";
import axios, { AxiosResponse, AxiosRequestConfig } from "axios";
import AxiosInstance from "@/common/AxiosInstance";
import { ErrorResponse, UnauthorizedError } from "@/common/ErrorModels";
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

  const result = AxiosInstance.get<JsonMapper.IGenericObject>("/bff/user", {
    headers: {
      "x-csrf": 1,
    },
  })
    .then((response) => {
      console.log(response.status);
      if (response.status !== 200) {
        store.isAuth = false;

        if (to.name !== "login" && !store.isAuth) {
          window.location.href = "/bff/login";
          //next({ name: "/login" });
        } else {
          throw new UnauthorizedError("Response was not 401 and not 200.", 401);
        }
      }
    })
    .catch();

  //  if(error instanceof InternalServerError) {
  //   window.location.href = "/bff/login";
  //  }

  //  window.location.href = "/bff/login";
  // };

  //   // check link param exit using route param method
  // if (link exit) {
  //     //  window.location.href =   add redirtect url
  // } else {
  //   next();
  // }
});

export default router;
