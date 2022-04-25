<template>
  <div class="container">
    <button class="button">
      <span class="relative text-base text-black"
        >Check the National Weather Service API Health</span
      >
    </button>
  </div>
  <span class="relative uppercase text-base text-gray">
    {{ state.isHealthy }} - {{ state.count }}
  </span>
</template>

<script setup lang="ts">
import { reactive } from "vue";
import { GetHealthAsync } from "@/services/LocationService";

const state = reactive({
  count: 0,
  isHealthy: "false",
});

async function increment(): Promise<void> {
  state.count++;
  state.isHealthy = await (await GetHealthAsync()).status;
}
</script>

<style scoped></style>
