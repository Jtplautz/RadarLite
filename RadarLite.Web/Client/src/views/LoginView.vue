<script setup lang="ts">
import LoginForm from "../components/LoginForm.vue";
import NMessageProver from "naive-ui";
</script>

<template>
  <main>
    <div class="flex flex-wrap">
      <div class="w-full">
        <n-message-provider>
          <LoginForm :example="7">Time to login!</LoginForm>
        </n-message-provider>
      </div>
    </div>
  </main>
</template>
