<template>
  <div class="list">
    <el-tag v-for="item in items" :key="item.value">
      {{ item.name }}
    </el-tag>
  </div>
</template>

<script setup lang="ts" generic="TValue">
import type { OptionResult } from '@/models/option-result';

const model = defineModel<TValue[]>({ default: [] });

const props = defineProps<{
  options: OptionResult<TValue>[];
}>();

const items = computed(() =>
  props.options.filter((x) => model.value.includes(x.value)),
);
</script>

<style scoped lang="scss">
.list {
  display: flex;
  gap: 10px;
}
</style>
