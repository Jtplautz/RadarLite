<script setup lang="ts">
import { reactive, ref } from "vue";
import { GetHealthAsync } from "@/services/LocationService";
import { NButton, NSpace } from "naive-ui";
import loading from "naive-ui/lib/_internal/loading";

export interface Props {
  msg?: string;
  labels?: string[];
}

const props = withDefaults(defineProps<Props>(), {
  msg: "hello",
  labels: () => ["one", "two"],
  loading: ref(false),
});

const emit = defineEmits<{
  (e: "change", id: number): void;
  (e: "update", value: string): void;
}>();

const state = reactive({
  count: 0,
  isHealthy: "false",
});

const loadingRef = ref(false);

function handleChange(event: Event) {
  console.log((event.target as HTMLInputElement).value);
  loadingRef.value = true;
  setTimeout(() => {
    loadingRef.value = false;
  }, 2000);
}

async function increment(): Promise<void> {
  state.count++;
  loadingRef.value = true;
  setTimeout(() => {
    loadingRef.value = false;
  }, 2000);
  state.isHealthy = await (await GetHealthAsync()).status;
}
</script>

<template>
  <n-space>
    <n-button
      size="large"
      :loading="loading"
      @click="increment"
      type="tertiary"
    >
      Tertiary
    </n-button>
  </n-space>
  <span class="relative uppercase text-base text-gray">
    {{ state.isHealthy }} - {{ state.count }}
  </span>
</template>

<style scoped></style>
