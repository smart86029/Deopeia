<template>
  <el-table-column :prop="prop" align="right">
    <template #default="{ row }">
      {{ row[prop] === undefined ? '' : $n(row[prop], 'decimal') }}
    </template>
  </el-table-column>
</template>

<script setup lang="ts">
defineProps<{
  prop: string;
}>();
</script>
