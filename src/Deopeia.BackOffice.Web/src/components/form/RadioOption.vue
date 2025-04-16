<template>
  <el-radio-group>
    <el-radio
      v-for="option in options"
      :key="option.value"
      :label="option.name"
      :value="option.value"
      :disabled="!option.isEnabled"
    />
  </el-radio-group>
</template>

<script setup lang="ts" generic="TValue">
import type { OptionResult } from '@/models/option-result';

defineProps<{
  options: OptionResult<TValue>[];
}>();
</script>
