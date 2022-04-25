<script setup lang="ts">
import { RouterLink, RouterView } from "vue-router";
import HealthCheckVue from "./components/HealthCheck.vue";
import HealthCheckButton from "./components/HealthCheckButton.vue";
</script>

<template>
  <nav>
    <RouterLink to="/">Home</RouterLink>
    <RouterLink to="/about">About</RouterLink>
  </nav>
  <div
    class="p-6 max-w-sm mx-auto rounded-xl shadow-lg flex items-right space-x-4"
  >
    <HealthCheckButton></HealthCheckButton>
  </div>
  <p class="text-bottom">
    <a
      href="https://open.spotify.com/track/1jDit1o3MxkQxFDVNdYX9v?si=00658221951d41d0"
      target="_blank"
      rel="noopener"
      >I don't know..</a
    >
  </p>
  <div class="container">here is where the weather information will come</div>
</template>

<style>
@import "@/assets/base.css";

header {
  line-height: 1.5;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}
.container {
  text-align: center;
}

a,
.green {
  text-decoration: none;
  color: hsla(160, 100%, 37%, 1);
  transition: 0.4s;
}

.wrapper {
  vertical-align: text-top;
}

@media (hover: hover) {
  a:hover {
    background-color: hsla(160, 100%, 37%, 0.2);
  }
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
