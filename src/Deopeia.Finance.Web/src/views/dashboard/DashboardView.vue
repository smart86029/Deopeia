<template>
  <FundOverview :funds="funds" />
  <div class="quote">
    <OrderBook class="order-book" :bids="bids" :asks="asks" :price="price" />
    <TradeForm class="trade-form" />
  </div>
</template>

<script setup lang="ts">
import { useQuoteStore } from '@/stores/quote';

const funds = [
  { currencyCode: 'USD', name: '美元', marginUsed: 200, balance: 8000 },
  { currencyCode: 'EUR', name: '歐元', marginUsed: 1000, balance: 10000 },
  { currencyCode: 'GBP', name: '英鎊', marginUsed: 4000, balance: 6000 },
  { currencyCode: 'JPY', name: '日圓', marginUsed: 1000, balance: 10000 },
];
const { quotes, bids, asks } = storeToRefs(useQuoteStore());
const price = computed(() => quotes.value.get('GCZ2024').value);
</script>

<style scoped lang="scss">
.quote {
  display: flex;
  justify-content: space-between;
  gap: 16px;
}

.order-book {
  flex: 1;
}

.trade-form {
  width: 300px;
}
</style>
