<template>
  <div class="authentication-code">
    <el-input
      v-for="(_, index) in inputs"
      :key="index"
      ref="inputRefs"
      v-model="inputs[index]"
      size="large"
      maxlength="1"
      placeholder=""
      @paste.prevent="handlePaste"
      @input="move(index)"
      @keydown="(event: KeyboardEvent) => backspace(event, index)"
    />
  </div>
</template>

<script setup lang="ts">
const length = 6;
const inputs: Ref<string[]> = ref(Array(length).fill(''));
const inputRefs: Ref<HTMLInputElement[]> = ref([]);

const handlePaste = (event: ClipboardEvent) => {
  if (!event.clipboardData) {
    return;
  }

  const data = event.clipboardData.getData('text').slice(0, length);
  for (let i = 0; i < data.length; i++) {
    inputs.value[i] = data[i] || '';
  }
};

const move = (index: number) => {
  if (inputs.value[index] && index < length - 1) {
    inputRefs.value[index + 1].focus();
  }
};

const backspace = (event: KeyboardEvent, index: number) => {
  if (event.key === 'Backspace' && inputs.value[index] === '' && index > 0) {
    inputRefs.value[index - 1].focus();
  }
};

onMounted(() => {
  inputRefs.value[0].focus();
});
</script>

<style lang="scss" scoped>
.authentication-code {
  display: flex;
  justify-content: space-between;
  width: 100%;
}

.el-input {
  $width: calc(var(--el-input-height) * 1.5);
  width: $width;
  height: $width;
  font-size: 2em;

  :deep(.el-input__wrapper:has(input:not(:placeholder-shown))) {
    box-shadow: inset 0 0 0 1px var(--el-color-primary);
  }

  :deep(input) {
    text-align: center;
    color: var(--el-color-primary);
  }
}
</style>
