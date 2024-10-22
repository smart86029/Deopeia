<template>
  <el-table-column :prop="prop" align="right">
    <template #default="{ row }">
      <span
        :class="{
          positive: row[prop] > (comparisonProp ? row[comparisonProp] : 0),
          negative: row[prop] < (comparisonProp ? row[comparisonProp] : 0),
        }"
      >
        {{ row[prop] !== undefined ? $n(row[prop], 'decimal') : '' }}
      </span>
    </template>
  </el-table-column>
</template>

<script setup lang="ts">
defineProps<{
  prop: string;
  comparisonProp?: string;
}>();
</script>

<style scoped lang="scss">
.positive {
  color: #f23645;
}

.negative {
  color: #089981;
}
</style>
