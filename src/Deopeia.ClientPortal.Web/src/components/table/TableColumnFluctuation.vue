<template>
  <el-table-column :prop="prop" align="right">
    <template #default="{ row }">
      <span
        :class="{
          positive:
            row[prop] > (comparisonProp ? row[comparisonProp] : comparison),
          negative:
            row[prop] < (comparisonProp ? row[comparisonProp] : comparison),
        }"
      >
        {{ row[prop] !== undefined ? $n(row[prop], 'decimal') : '-' }}
      </span>
    </template>
  </el-table-column>
</template>

<script setup lang="ts">
withDefaults(
  defineProps<{
    prop: string;
    comparisonProp?: string;
    comparison?: number;
  }>(),
  {
    comparison: 0,
  },
);
</script>

<style lang="scss" scoped>
.positive {
  color: var(--el-color-positive);
}

.negative {
  color: var(--el-color-negative);
}
</style>
