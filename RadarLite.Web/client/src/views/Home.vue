<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png" />
    <HelloWorld name="Welcome to Your Vue.js + TypeScript App" />
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import HelloWorld from "@/components/HelloWorld.vue"; // @ is an alias to /src
export default defineComponent({
  name: "Home",
  components: {
    HelloWorld,
  },
  data() {
    return {
      name: "Link",
    };
  },
});
// @Options({
//   components: {
//     HelloWorld,
//   },
// })
// export default class Home extends Vue {}
</script>
