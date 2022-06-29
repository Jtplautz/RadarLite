<script setup lang="ts">
import { RouterLink, RouterView } from "vue-router";
</script>

<template>
  <nav>
    <n-tabs type="line">
      <n-tab name="Home"><RouterLink to="/">Home</RouterLink></n-tab>
      <n-tab name="About"><RouterLink to="/about">About</RouterLink></n-tab>
    </n-tabs>
    <RouterLink to="/login">Login</RouterLink>
    <RouterLink to="/logout">Logout</RouterLink>
    <RouterLink to="/create-account">Sign Up</RouterLink>
    <RouterView></RouterView>
  </nav>
</template>
