<template>
  <el-select clearable>
    <el-option
      v-for="option in options"
      :key="option.value"
      :label="option.name"
      :value="option.value"
    />
  </el-select>
</template>

<script setup lang="ts" generic="TValue">
import type { OptionResult } from '@/models/option-result';

defineProps<{
  options: OptionResult<TValue>[];
}>();
</script>

<style scoped lang="scss">
.el-select {
  min-width: var(--el-form-inline-content-width);
}
</style>
