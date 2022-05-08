<template>
  <n-form ref="formRef" :model="modelRef" :rules="rules">
    <n-form-item path="age" label="Age">
      <n-input v-model:value="modelRef.age" @keydown.enter.prevent />
    </n-form-item>
    <n-form-item path="password" label="Password">
      <n-input
        v-model:value="modelRef.password"
        type="password"
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
        type="password"
        @keydown.enter.prevent
      />
    </n-form-item>
    <n-row :gutter="[0, 24]">
      <n-col :span="24">
        <div style="display: flex; justify-content: flex-end">
          <n-button
            :disabled="modelRef.age === null"
            round
            type="primary"
            @click="handleValidateButtonClick"
          >
            Validate
          </n-button>
        </div>
      </n-col>
    </n-row>
  </n-form>

  <pre
    >{{ JSON.stringify(modelRef, null, 2) }}
</pre
  >
  <label>{{ test }}</label>
</template>

<script setup lang="ts">
import { onMounted, ref } from "vue";
import type {
  FormInst,
  FormItemInst,
  FormItemRule,
  FormValidationError,
  FormRules,
} from "naive-ui";
import { useMessage } from "naive-ui";

interface ModelType {
  age: string | null;
  password: string | null;
  reenteredPassword: string | null;
}

const formRef = ref<FormInst | null>(null);
const rPasswordFormItemRef = ref<FormItemInst | null>(null);
const message = useMessage();
const modelRef = ref<ModelType>({
  age: null,
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
  age: [
    {
      required: true,
      validator(rule: FormItemRule, value: string) {
        if (!value) {
          return new Error("Age is required");
        } else if (!/^\d*$/.test(value)) {
          return new Error("Age should be an integer");
        } else if (Number(value) < 18) {
          return new Error("Age should be above 18");
        }
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
};
function handlePasswordInput(): void {
  if (modelRef.value.reenteredPassword) {
    rPasswordFormItemRef.value?.validate({ trigger: "password-input" });
  }
}
function handleValidateButtonClick(e: MouseEvent): void {
  e.preventDefault();
  formRef.value?.validate((errors: Array<FormValidationError> | undefined) => {
    if (!errors) {
      message.success("Valid");
    } else {
      console.log(errors);
      message.error("Invalid");
    }
  });
}
</script>
