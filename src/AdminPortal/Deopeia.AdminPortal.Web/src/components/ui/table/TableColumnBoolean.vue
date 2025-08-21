<template>
  <el-table-column :prop="prop" :label="$t(`${localeKey}.name`)">
    <template #default="{ row }">
      <TextBoolean :value="row[prop]" :localeKey="localeKey" />
    </template>
  </el-table-column>
</template>

<script setup lang="ts">
defineProps<{
  prop: string;
  localeKey: string;
}>();
</script>
