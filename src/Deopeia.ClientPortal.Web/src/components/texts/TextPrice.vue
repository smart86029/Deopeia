<template>
  <span :class="{ positive: value > comparison, negative: value < comparison }">
    {{ $n(value, percentage ? 'percent' : 'decimal') }}
  </span>
</template>

<script setup lang="ts">
withDefaults(
  defineProps<{
    value: number;
    comparison?: number;
    percentage?: boolean;
  }>(),
  { comparison: 0 },
);
</script>

<style scoped lang="scss">
.positive {
  color: var(--el-color-positive);
}

.negative {
  color: var(--el-color-negative);
}
</style>
