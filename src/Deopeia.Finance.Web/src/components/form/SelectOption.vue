<template>
  <el-select v-model="model" clearable>
    <el-option
      v-for="option in options"
      :key="option.value"
      :label="option.name"
      :value="option.value"
      :disabled="!option.isEnabled"
    />
  </el-select>
</template>

<script setup lang="ts" generic="TValue">
import type { OptionResult } from '@/models/option-result';

const model = defineModel<TValue>();

defineProps<{
  options: OptionResult<TValue>[];
}>();
</script>

<style scoped lang="scss">
.el-select {
  min-width: var(--el-form-inline-content-width);
}
</style>
