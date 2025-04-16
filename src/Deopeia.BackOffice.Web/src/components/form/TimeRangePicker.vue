<template>
  <el-time-picker
    is-range
    range-separator="~"
    :start-placeholder="$t('common.startTime')"
    :end-placeholder="$t('common.endTime')"
    :default-value="defaultValue"
    :disabled-seconds
  />
</template>

<script setup lang="ts">
import { dayjs } from 'element-plus';

const defaultValue: [Date, Date] = [
  dayjs('00:00:00').toDate(),
  dayjs('05:00:00').toDate(),
];

const disabledSeconds = () => {
  const results: number[] = [];
  for (let i = 1; i <= 59; i++) {
    results.push(i);
  }
  return results;
};
</script>
