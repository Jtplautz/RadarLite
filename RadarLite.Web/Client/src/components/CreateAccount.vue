<template>
  <n-form ref="formRef" :model="modelRef" :rules="rules">
    <n-form-item path="email" label="Email">
      <n-input
        v-model:value="modelRef.email"
        type="email"
        placeholder="Email Address"
        @keydown.enter.prevent
      />
    </n-form-item>
    <n-form-item path="username" label="Username">
      <n-input
        v-model:value="modelRef.username"
        type="username"
        placeholder="Username"
        @keydown.enter.prevent
      />
    </n-form-item>
    <n-form-item path="password" label="Password">
      <n-input
        v-model:value="modelRef.password"
        type="password"
        placeholder="Password"
        @input="handlePasswordInput"
        @keydown.enter.prevent
      />
    </n-form-item>
    <n-form-item
      ref="rPasswordFormItemRef"
      first
      path="reenteredPassword"
      label="Re-enter Password"
    >
      <n-input
        v-model:value="modelRef.reenteredPassword"
        :disabled="!modelRef"
        placeholder="Renter Password"
        type="password"
        @keydown.enter.prevent
      />
    </n-form-item>
    <n-row :gutter="[0, 24]">
      <n-col :span="24">
        <div style="display: flex; justify-content: flex-end">
          <n-button
            :disabled="modelRef.email === null"
            round
            type="primary"
            @click="handleValidateButtonClick"
          >
            Sign up!
          </n-button>
        </div>
      </n-col>
    </n-row>
  </n-form>
</template>
<script setup lang="ts">
//https://www.naiveui.com/en-US/dark/components/form
import { onMounted, ref } from "vue";
import type {
  FormInst,
  FormItemInst,
  FormItemRule,
  FormValidationError,
  FormRules,
} from "naive-ui";
//import { useMessage } from "naive-ui";
//import { NMessageProvider, NLoadingBarProvider } from "naive-ui";
import { authStore } from "@/stores/AuthStore";
import UserModel from "@/common/UserModel";
import type { StoreDefinition } from "pinia";
import router from "@/router/index";

interface ModelType {
  email: string | null;
  username: string | null;
  password: string | null;
  reenteredPassword: string | null;
}

const formRef = ref<FormInst | null>(null);
const rPasswordFormItemRef = ref<FormItemInst | null>(null);
const message = "Sucess!"; //useMessage();
const modelRef = ref<ModelType>({
  email: null,
  username: null,
  password: null,
  reenteredPassword: null,
});
//testing refs
const test = ref<string>();
//test $refs
//see shawn wildermuth video on this. is it worth it? proably just a side quest to find out..
//let tests = $ref<string>();

//testing onMounted and some fun stuff with props and emits.
onMounted(async () => {
  console.log("LoginForm mounted!" + props.example);
  emits("Blah!");
  console.log("Blah! emitted.");
  if (props.example > 10) test.value = "La Di Da Di Dah!";
  console.log(test);
});
const props = defineProps({
  example: {
    type: Number,
    default: 75,
  },
});
const emits = defineEmits(["Blah!"]);
function validatePasswordStartWith(rule: FormItemRule, value: string): boolean {
  return (
    !!modelRef.value.password &&
    modelRef.value.password.startsWith(value) &&
    modelRef.value.password.length >= value.length
  );
}
function validatePasswordSame(rule: FormItemRule, value: string): boolean {
  return value === modelRef.value.password;
}

const rules: FormRules = {
  email: [
    {
      required: true,
      validator(rule: FormItemRule, value: string) {
        if (!value) {
          return new Error("Email is required");
        }
        // else if (!/^\d*$/.test(value)) {
        //   return new Error("Email should be an integer");
        // } else if (Number(value) < 18) {
        //   return new Error("Email should be above 18");
        // }
        return true;
      },
      trigger: ["input", "blur"],
    },
  ],

  password: [
    {
      required: true,
      message: "Password is required",
    },
  ],

  reenteredPassword: [
    {
      required: true,
      message: "Re-entered password is required",
      trigger: ["input", "blur"],
    },
    {
      validator: validatePasswordStartWith,
      message: "Password is not same as re-entered password!",
      trigger: "input",
    },
    {
      validator: validatePasswordSame,
      message: "Password is not same as re-entered password!",
      trigger: ["blur", "password-input"],
    },
  ],
  userName: [
    {
      required: true,
      message: "a username is required!",
      trigger: ["input", "blur"],
    },
  ],
};
function handlePasswordInput(): void {
  if (modelRef.value.reenteredPassword) {
    rPasswordFormItemRef.value?.validate({ trigger: "password-input" });
  }
}
async function handleValidateButtonClick(e: MouseEvent): Promise<void> {
  const user: UserModel = new UserModel();
  user.email = modelRef.value.email;
  user.username = modelRef.value.username;
  user.password = modelRef.value.reenteredPassword;

  e.preventDefault();
  formRef.value?.validate(
    async (errors: Array<FormValidationError> | undefined) => {
      if (!errors) {
        //message.success("Valid! Creating account...");
        //do the loading thing..
        //TODO-- Call service to create account. Redirect to login page if successful.
        const store: StoreDefinition = new authStore();
        //await store.setToken(user);
        const isSuccesfulUserCreation = await store.createUser(user);
        if (isSuccesfulUserCreation) {
          router.push("/login");
        }
      } else {
        console.log(errors);
        message.error("Invalid");
      }
    }
  );
}
</script>
