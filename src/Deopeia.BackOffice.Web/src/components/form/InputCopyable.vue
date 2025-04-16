<template>
  <el-input ref="input" v-model="model" readonly @click="selectAll">
    <template #suffix>
      <IconContentCopy
        @mouseover="selectAll"
        @mouseleave="onMouseLeave"
        @click="copy"
      />
    </template>
  </el-input>
</template>

<script setup lang="ts">
import { ElMessage } from 'element-plus';

const model = defineModel<string>({ default: '' });

const input = ref<HTMLInputElement | null>(null);
const { t } = useI18n();

const selectAll = () => {
  input.value?.select();
};

const onMouseLeave = () => {
  window.getSelection()?.removeAllRanges();
};

const copy = () => {
  navigator.clipboard.writeText(model.value).then(() =>
    ElMessage.success({
      message: t('common.message.copied'),
    }),
  );
};
</script>

<style lang="scss" scoped>
.el-input {
  :deep(.el-input__suffix) {
    cursor: pointer;
  }
}
</style>
