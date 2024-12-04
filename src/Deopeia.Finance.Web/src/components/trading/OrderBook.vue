<template>
  <el-table
    :data="orders"
    :row-style="depthBackground"
    @row-click="changePrice"
  >
    <el-table-column :label="$t('finance.price')" align="right">
      <template #default="{ row }">
        <el-text v-if="row.price" :type="row.type">
          {{ $n(row.price, 'decimal') }}
        </el-text>
        <template v-else>-</template>
      </template>
    </el-table-column>
    <el-table-column
      :label="$t('finance.size')"
      class-name="depth"
      align="right"
    >
      <template #default="{ row }">
        <template v-if="row.size">{{ $n(row.size, 'integer') }}</template>
      </template>
    </el-table-column>
  </el-table>
</template>

<script setup lang="ts">
import { type Order } from '@/models/quote/order';
import { usePreferencesStore } from '@/stores/preferences';

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
const { positive, negative } = storeToRefs(usePreferencesStore());

const orders = computed(() => {
  const results: OrderItem[] = new Array(11);
  results.fill({
    price: 0,
    size: 0,
  });
  [...props.asks]
    .sort((a, b) => a.price - b.price)
    .forEach(
      (ask, index) => (results[4 - index] = { ...ask, type: negative.value }),
    );
  results[5] = { price: props.price, size: 0 };
  [...props.bids]
    .sort((a, b) => b.price - a.price)
    .forEach(
      (bid, index) => (results[6 + index] = { ...bid, type: positive.value }),
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

const depth = (size: number) =>
  maxSize.value === 0 ? '0' : n(size / maxSize.value, 'percent');

const depthBackground = ({ row }) => {
  const width = depth(row.size);
  return `background: linear-gradient(90deg, var(--el-table-tr-bg-color) ${depth(maxSize.value - row.size)}, var(--el-color-${row.type}-light-5) ${width})`;
};

const changePrice = (row: any) => emits('update', row.price);
</script>

<style scoped lang="scss">
:deep(.el-table__row:hover) {
  cursor: pointer;
}

.depth {
  position: absolute;
  top: 0;
  right: 0;
  bottom: 0;
  width: 50%;
  background: red;
  opacity: 0.1;
}
</style>
