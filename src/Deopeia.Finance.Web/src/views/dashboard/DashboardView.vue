<template>
  <FundOverview :funds="funds" />
  <TradeForm class="trade-form" />
</template>

<script setup lang="ts">
const funds = [
  { currencyCode: 'USD', name: '美元', marginUsed: 200, balance: 8000 },
  { currencyCode: 'EUR', name: '歐元', marginUsed: 1000, balance: 10000 },
  { currencyCode: 'GBP', name: '英鎊', marginUsed: 4000, balance: 6000 },
  { currencyCode: 'JPY', name: '日圓', marginUsed: 1000, balance: 10000 },
];
</script>

<style scoped lang="scss">
.trade-form {
  width: 300px;
}
</style>
