<template>
  <n-space class="health" style="display: center">
    <n-button
      size="large"
      type="default"
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
import { convertToBoolean } from "@/helpers/Conversions/ConversionHelper";
import { NButton, NSpace, useMessage, useLoadingBar } from "naive-ui";

export default defineComponent({
  components: {},
  setup() {
    const loadingBar = useLoadingBar();
    const loadingRef = ref(false);
    const message = useMessage();

    return {
      async handleClick() {
        loadingBar.start();
        loadingRef.value = true;
        const isAPIHealthy: boolean = convertToBoolean(
          (await GetHealthAsync()).status
        );

        state.isHealthy = isAPIHealthy;
        state.count++;

        loadingRef.value = state.isHealthy;

        if (state.isHealthy) {
          message.success("Nice one! The National Weather Service is up!");
          loadingRef.value = false;
        } else {
          loadingBar.error();
          message.error(
            "Yikes! The National Weather Service connection is interrupted!"
          );

          loadingRef.value = false;
        }

        loadingBar.finish();
      },
      loading: loadingRef,
    };
  },
});

const state = reactive({
  count: 0,
  isHealthy: false,
});

async function increment(): Promise<void> {
  state.count++;
  const isAPIHealthy: boolean = convertToBoolean(
    await (
      await GetHealthAsync()
    ).status
  );
  state.isHealthy = isAPIHealthy;
}
</script>
