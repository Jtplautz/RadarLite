import { defineStore } from "pinia";
import type { PropType } from "vue";

type Role = {
  name: string;
};

export const authStore = defineStore("authStore", {
  state: () => ({
    isAuth: false,
    username: "",
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
  },
});
