<template>
  <el-select 
    v-model="model" 
    :clearable="clearable"
    :placeholder="placeholder"
    :disabled="disabled"
    :loading="loading"
  >
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

// Define model with proper typing
const model = defineModel<TValue>();

// Component props with defaults
withDefaults(
  defineProps<{
    /** Available options for selection */
    options: OptionResult<TValue>[];
    /** Whether the select can be cleared */
    clearable?: boolean;
    /** Placeholder text when no value is selected */
    placeholder?: string;
    /** Whether the select is disabled */
    disabled?: boolean;
    /** Whether the select is in loading state */
    loading?: boolean;
  }>(),
  {
    clearable: true,
    placeholder: '',
    disabled: false,
    loading: false,
  },
);
</script>

<style scoped lang="scss">
.el-select {
  min-width: var(--el-form-inline-content-width);
}
</style>
