<template>
  <n-loading-bar-provider>
    <n-message-provider>
      <n-notification-provider>
        <n-dialog-provider>
          <slot></slot>
        </n-dialog-provider>
      </n-notification-provider>
    </n-message-provider>
  </n-loading-bar-provider>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import {
  NMessageProvider,
  NLoadingBarProvider,
  NDialogProvider,
  NNotificationProvider,
} from "naive-ui";

export default defineComponent({
  name: "AppProvider",
  components: {
    NMessageProvider,
    NLoadingBarProvider,
    NNotificationProvider,
    NDialogProvider,
  },
});
</script>
