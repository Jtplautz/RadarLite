import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";
import { authStore } from "@/stores/AuthStore";
import LoginView from "../views/LoginView.vue";
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
    component: LoginView,
  },
  {
    path: "/:catchAll(.*)",
    name: "PageNotFound",
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
//   const store = new authStore();
//   console.log(store.isAuth);

//   if (store.isAuth)
//     return router.push("https:\\localhost:7506/index/account/login");
// });

export default router;
