<template>
  <n-space class="health" style="display: center">
    <n-button
      size="large"
      type="tertiary"
      :loading="loading"
      @click="handleClick"
    >
      Click Me
    </n-button>
  </n-space>
</template>

<script lang="ts">
import { defineComponent, ref, reactive } from "vue";
import { GetHealthAsync } from "@/services/LocationService";
import { NButton, NSpace, useMessage, useLoadingBar } from "naive-ui";

export default defineComponent({
  components: {},
  setup() {
    const loadingBar = useLoadingBar();
    const loadingRef = ref(false);
    const message = useMessage();

    return {
      handleClick() {
        loadingBar.error();
        loadingRef.value = true;
        setTimeout(() => {
          loadingRef.value = false;
        }, 200);

        message.success("Nice one!");
      },
      loading: loadingRef,
    };
  },
});

const state = reactive({
  count: 0,
  isHealthy: "false",
});

async function increment(): Promise<void> {
  state.count++;
  state.isHealthy = await (await GetHealthAsync()).status;
}
</script>
