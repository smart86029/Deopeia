<template>
  <div class="quote">
    <ContractChart class="chart" />
    <OrderBook
      class="order-book"
      :bids="bids"
      :asks="asks"
      :price="price"
      @update="changePrice"
    />
    <TradeForm class="trade-form" :price="selectPrice" />
  </div>
</template>

<script setup lang="ts">
import { useQuoteStore } from '@/stores/quote';

const { symbol, quotes, bids, asks } = storeToRefs(useQuoteStore());
const price = computed(() => quotes.value.get(symbol.value).value);
const selectPrice = ref(undefined as number | undefined);

const changePrice = (price: number) => (selectPrice.value = price);
</script>

<style scoped lang="scss">
.quote {
  display: flex;
  justify-content: end;
  gap: 16px;
}

.chart {
  flex: 1;
}

.order-book {
  flex: 0 0 300px;
}

.trade-form {
  flex: 0 0 300px;
}
</style>
