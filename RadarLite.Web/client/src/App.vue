<script setup lang="ts">
import { RouterLink, RouterView } from "vue-router";
import HealthCheck from "./components/HealthCheck.vue";
</script>

<template>
  <nav>
    <RouterLink to="/">Home</RouterLink>
    <RouterLink to="/about">About</RouterLink>
    <RouterLink to="/login">Login</RouterLink>
    <RouterView></RouterView>
  </nav>
  <div class="health">
    <HealthCheck class="health"></HealthCheck>
  </div>
  <p class="health">
    <a
      href="https://open.spotify.com/track/1jDit1o3MxkQxFDVNdYX9v?si=00658221951d41d0"
      target="_blank"
      rel="noopener"
      >I've got a rock in my boot!</a
    >
  </p>
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
