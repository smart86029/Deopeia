<template>
  <div class="order-book">
    <el-table :data="bids">
      <TableColumnInteger prop="size" :label="$t('finance.size')" />
      <TableColumnFluctuation
        prop="price"
        :label="$t('finance.bid')"
        :comparison="price"
      />
    </el-table>
    <el-table :data="asks">
      <TableColumnFluctuation
        prop="price"
        :label="$t('finance.ask')"
        :comparison="price"
      />
      <TableColumnInteger prop="size" :label="$t('finance.size')" />
    </el-table>
  </div>
</template>

<script setup lang="ts">
import { type Order } from '@/models/quote/order';

defineProps<{
  bids: Order[];
  asks: Order[];
  price: number;
}>();
</script>

<style scoped lang="scss">
.order-book {
  display: flex;
  flex-direction: row;
}
</style>
