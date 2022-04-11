<template>
  <div class="health">
    <button @click="increment">
      Check health of the National Weather Service API
    </button>
  </div>
  <span> {{ state.isHealthy }} - {{ state.count }} </span>
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

<style scoped>
.health {
  width: max-content;
}
</style>
