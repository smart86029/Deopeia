<template>
  <el-table-column :formatter="dateTimeFormatter" />
</template>

<script setup lang="ts">
import { formatDateTime } from '@/plugins/dayjs';

const dateTimeFormatter = (row: any, column: any, cellValue: any): string =>
  formatDateTime(cellValue);
</script>
