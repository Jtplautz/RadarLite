<script setup lang="ts">
import { RouterLink, RouterView } from "vue-router";
import { NTimeline } from "naive-ui";
import HealthCheck from "./components/HealthCheck.vue";
</script>

<template>
  <nav>
    <RouterLink to="/">Home</RouterLink>
    <RouterLink to="/about">About</RouterLink>
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
  <div class="timeline">
    <n-timeline>
      <n-timeline-item content="Oops" />
      <n-timeline-item
        type="success"
        title="Success"
        content="JCRadarLite inception"
        time="2020-07-12 20:46"
      />
      <n-timeline-item
        type="error"
        content="Restart JCRadarLiteV1"
        time="2020-10-05 20:46"
      />
      <n-timeline-item
        type="warning"
        title="Warning"
        content="Almost quit development"
        time="2021-02-25 20:46"
      />
      <n-timeline-item
        type="info"
        title="Info"
        content="working still"
        time="2022-04-25 20:46"
        line-type="dashed"
      />
      <n-timeline-item type="success" content="Success!" />
    </n-timeline>
  </div>
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
.timeline {
  display: "flexbox";
  align-content: center;
}
</style>
