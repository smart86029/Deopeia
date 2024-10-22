<template>
  <el-table-column :prop="prop">
    <template #default="{ row }">
      <TextEnum :value="row[prop]" :localeKey="localeKey" />
    </template>
  </el-table-column>
</template>

<script setup lang="ts">
defineProps<{
  prop: string;
  localeKey: string;
}>();
</script>
