<template>
  <el-table :data="orders" :row-style="depth" @row-click="changePrice">
    <el-table-column :label="$t('finance.price')" align="right">
      <template #default="{ row }">
        <el-text v-if="row.price" :type="row.type">
          {{ $n(row.price, 'decimal') }}
        </el-text>
        <template v-else>-</template>
      </template>
    </el-table-column>
    <TableColumnInteger prop="size" :label="$t('finance.size')" />
  </el-table>
</template>

<script setup lang="ts">
import { type Order } from '@/models/quote/order';

interface OrderItem extends Order {
  type?: string;
}

const props = defineProps<{
  bids: Order[];
  asks: Order[];
  price: number;
}>();
const emits = defineEmits<{
  update: [value: number];
}>();

const { n } = useI18n();

const orders = computed(() => {
  const results: OrderItem[] = new Array(11);
  results.fill({
    price: 0,
    size: 0,
  });
  [...props.asks]
    .sort((a, b) => a.price - b.price)
    .forEach(
      (ask, index) => (results[4 - index] = { ...ask, type: 'negative' }),
    );
  results[5] = { price: props.price, size: 0 };
  [...props.bids]
    .sort((a, b) => b.price - a.price)
    .forEach(
      (bid, index) => (results[6 + index] = { ...bid, type: 'positive' }),
    );
  return results;
});

const maxSize = computed(
  () =>
    orders.value
      .map((x) => x.size)
      .sort((a, b) => b - a)
      .shift() || 0,
);

const depth = (data: { row: OrderItem }) => {
  const width =
    maxSize.value === 0
      ? '0'
      : n((maxSize.value - data.row.size) / maxSize.value, 'percent');
  return `background: linear-gradient(90deg, var(--el-table-tr-bg-color) ${width}, var(--el-color-${data.row.type}) 150%`;
};

const changePrice = (row: any) => emits('update', row.price);
</script>

<style scoped lang="scss">
:deep(.el-table__row:hover) {
  cursor: pointer;
}
</style>
