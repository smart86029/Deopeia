<template>
  <span :class="{ positive: value > 0, negative: value < 0 }"
    ><template v-if="value > 0">+</template>{{ formatted }}<template v-if="percentage">%</template></span
  >
</template>

<script setup lang="ts">
const props = defineProps<{
  value: number;
  percentage?: boolean;
}>();

const formatted = computed(() =>
  props.percentage ? props.value.toFixed(2) : props.value.toString(),
);
</script>

<style scoped lang="scss">
.positive {
  color: #f23645;
}

.negative {
  color: #089981;
}
</style>
