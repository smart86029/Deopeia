<template>
  <el-select v-model="model">
    <el-option
      v-for="option in options"
      :key="option.value"
      :label="option.label"
      :value="option.value"
    />
  </el-select>
</template>

<script setup lang="ts">
import { DayOfWeek } from '@/models/day-of-week';
import { dayjs } from 'element-plus';

const model = defineModel<DayOfWeek>();

const weekdays = dayjs.weekdays();
const keys = Object.keys(DayOfWeek);
const options = keys.slice(0, keys.length / 2).map((x) => ({
  value: +x,
  label: weekdays[+x],
}));
</script>

<style lang="scss" scoped>
.el-select {
  min-width: var(--el-form-inline-content-width);
}
</style>
