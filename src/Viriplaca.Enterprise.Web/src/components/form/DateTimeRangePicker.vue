<template>
  <el-date-picker
    type="datetimerange"
    range-separator="~"
    :start-placeholder="$t('timer.startDate')"
    :end-placeholder="$t('timer.endDate')"
    :default-time="defaultTime"
  />
</template>

<script setup lang="ts">
const defaultTime: [Date, Date] = [
  new Date(2000, 1, 1, 0, 0, 0),
  new Date(2000, 2, 1, 23, 59, 59),
];
</script>
