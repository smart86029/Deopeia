<template>
  <el-date-picker
    type="datetimerange"
    range-separator="~"
    :start-placeholder="$t('common.startTime')"
    :end-placeholder="$t('common.endTime')"
    :default-value="defaultValue"
  />
</template>

<script setup lang="ts">
import { dayjs } from 'element-plus';

const defaultValue: Date = dayjs().add(-1, 'month').toDate();
</script>
