<script setup lang="ts">
import { RouterLink, RouterView } from "vue-router";
import IconCommunity from "./icons/IconCommunity.vue";
</script>

<template>
  <div style="margin: 25px">
    RadarLite
    <a href="https://localhost:5000" class="navbar-brand"
      ><IconCommunity> </IconCommunity>
    </a>
  </div>
  <nav>
    <n-tabs type="line">
      <n-tab name="Home"><RouterLink to="/">Home</RouterLink></n-tab>
      <n-tab name="About"><RouterLink to="/about">About</RouterLink></n-tab>
      <n-tab name="Login"><RouterLink to="/login">Login</RouterLink></n-tab>
      <n-tab name="Logout"><RouterLink to="/logout">Logout</RouterLink></n-tab>
      <n-tab name="Signup"
        ><RouterLink to="/create-account">Signup</RouterLink></n-tab
      >
    </n-tabs>
    <RouterView></RouterView>
  </nav>
</template>
