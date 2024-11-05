<template>
  <div class="order-book">
    <el-table :data="bids">
      <TableColumnDecimal prop="size" :label="$t('trading.openPrice')" />
      <TableColumnFluctuation
        prop="price"
        :label="$t('finance.price')"
        :comparison="100.5"
      />
    </el-table>
    <el-table :data="asks">
      <TableColumnFluctuation
        prop="price"
        :label="$t('finance.price')"
        :comparison="100.5"
      />
      <TableColumnDecimal prop="size" :label="$t('trading.openPrice')" />
    </el-table>
  </div>
</template>

<script setup lang="ts">
interface Order {
  price: number;
  size: number;
}

const bids: Order[] = reactive([
  { price: 100, size: 10 },
  { price: 99, size: 10 },
  { price: 98, size: 10 },
  { price: 97, size: 10 },
  { price: 96, size: 10 },
]);

const asks: Order[] = reactive([
  { price: 101, size: 10 },
  { price: 102, size: 10 },
  { price: 103, size: 10 },
  { price: 104, size: 10 },
  { price: 105, size: 10 },
]);
</script>

<style scoped lang="scss">
.order-book {
  display: flex;
  flex-direction: row;

  .el-table:not(:first-child) {
    margin: 0;
  }
}
</style>
