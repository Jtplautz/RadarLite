<script setup lang="ts">
import HealthCheck from "../components/HealthCheck.vue";
</script>

<template>
  <n-divider>Self Responsive</n-divider>
  <n-grid cols="2 400:4 600:6">
    <n-grid-item>
      <div class="light-green">1</div>
    </n-grid-item>
    <n-grid-item>
      <div class="health">
        <HealthCheck class="health"></HealthCheck>
      </div>
    </n-grid-item>
    <n-grid-item>
      <div class="light-green">3</div>
    </n-grid-item>
    <n-grid-item>
      <div class="green">4</div>
    </n-grid-item>
    <n-grid-item>
      <div class="light-green">
        <p class="health">
          <a
            href="https://open.spotify.com/track/1jDit1o3MxkQxFDVNdYX9v?si=00658221951d41d0"
            target="_blank"
            rel="noopener"
            >I've got a rock in my boot!</a
          >
        </p>
      </div>
    </n-grid-item>
    <n-grid-item>
      <div class="green">6</div>
    </n-grid-item>
  </n-grid>
  <n-divider>Screen Responsive</n-divider>
  <n-grid cols="2 s:3 m:4 l:5 xl:6 2xl:7" responsive="screen">
    <n-grid-item>
      <div class="light-green">1</div>
    </n-grid-item>
    <n-grid-item>
      <div class="green">2</div>
    </n-grid-item>
    <n-grid-item>
      <div class="light-green">3</div>
    </n-grid-item>
    <n-grid-item>
      <div class="green">4</div>
    </n-grid-item>
    <n-grid-item>
      <div class="light-green">5</div>
    </n-grid-item>
    <n-grid-item>
      <div class="green">6</div>
    </n-grid-item>
    <n-grid-item>
      <div class="light-green">7</div>
    </n-grid-item>
  </n-grid>
</template>
