import { defineStore } from "pinia";
import type { PropType } from "vue";
import {
  getAccessToken,
  authenticate,
  signIn,
  CreateNewUser,
} from "@/Services/AuthenticationService";
import type UserModel from "@/common/UserModel";

type Role = {
  name: string;
};

export const authStore = defineStore("authStore", {
  state: () => ({
    isAuth: false,
    username: "",
    email: "",
    password: "",
    name: "",
    lastName: "",
    currentUserId: Number,
    currentUserUsername: String,
    currentUserRoles: Array as PropType<Role[]>,
    isLoggedIn: false,
  }),

  getters: {
    getisAuth(state): boolean {
      return state.isAuth;
    },
    getUserName(state): string {
      return state.username;
    },
    getFullName(state): string {
      return state.username + " " + state.lastName;
    },
    getCurrentUserId: (state) => state.currentUserId,
    getCurrentUsername: (state) => state.currentUserUsername,
    getCurrentUserRoles: (state) => state.currentUserRoles,
    isUserLoggedIn: (state) => state.isLoggedIn,
    //getUserById: (state) => {
    //return (role: string) => state.currentUserRoles.includes(role);
    //},
  },
  actions: {
    setUser(username: string) {
      this.username = username;
    },

    setAuth(boolean: boolean) {
      this.isAuth = boolean;
    },
    async setToken(user: UserModel) {
      const usertofind = await getAccessToken(user);
      if (usertofind === "") {
        this.isAuth = true;
        console.log(usertofind);
      }
    },
    async getUser(returnpath: string) {
      if (!(await authenticate(returnpath))) {
        signIn(returnpath);
      }
    },
    async createUser(user: UserModel) {
      const success: boolean = await CreateNewUser(user);
      return success;
    },
  },
});
