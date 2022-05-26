<script setup lang="ts">
import { RouterLink, RouterView } from "vue-router";
import LoginView from "./views/LoginView.vue";
</script>

<template>
  <nav>
    <RouterLink to="/">Home</RouterLink>
    <RouterLink to="/about">About</RouterLink>
    <RouterLink to="/bff/login">Login</RouterLink>
    <RouterView></RouterView>
  </nav>
</template>

<style>
@import "@/assets/base.css";

header {
  line-height: 1.5;
}

.health {
  font-size: 12px;
  text-align: center;
  margin-top: 2rem;
  display: block;
}
.logo {
  display: block;
  margin: 0 auto 2rem;
}

nav {
  width: 100%;
  font-size: 12px;
  text-align: center;
  margin-top: 2rem;
}

nav a.router-link-exact-active {
  color: var(--color-text);
}

nav a.router-link-exact-active:hover {
  background-color: transparent;
}

nav a {
  display: inline-block;
  padding: 0 1rem;
  border-left: 1px solid var(--color-border);
}

nav a:first-of-type {
  border: 0;
}
</style>
