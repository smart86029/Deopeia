<template>
  <el-table-column :prop="prop" align="right">
    <template #default="{ row }">
      {{ row[prop] ? $n(row[prop], 'integer') : '' }}
    </template>
  </el-table-column>
</template>

<script setup lang="ts">
defineProps<{
  prop: string;
}>();
</script>
