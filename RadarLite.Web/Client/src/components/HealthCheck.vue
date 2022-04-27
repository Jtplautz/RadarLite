<script setup lang="ts">
import LoadingButton from "./LoadingButton.vue";
import { NMessageProvider, NLoadingBarProvider } from "naive-ui";
</script>

<template>
  <div class="health">
    <n-loading-bar-provider>
      <n-message-provider class="health">
        <LoadingButton class="health"></LoadingButton
      ></n-message-provider>
    </n-loading-bar-provider>
  </div>
</template>

<style scoped></style>
