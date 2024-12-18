<template>
  <div class="trading">
    <div class="contract">
      <div class="quote">
        {{ symbol }}
        {{ instrument.name }}
        <span class="ltp">{{ $n(lastTradedPrice, 'decimal') }}</span>
        <span class="currency">{{ instrument.currencyCode }}</span>
        <TextPrice :value="priceChange" />
        <TextPrice :value="priceRateOfChange" percentage />
      </div>
      <el-menu :default-active="activeIndex" mode="horizontal" router>
        <el-menu-item
          v-for="menu of menus"
          :key="menu"
          :index="menu"
          :route="{ name: menu }"
        >
          {{ $t(`route.${menu}`) }}
        </el-menu-item>
      </el-menu>
      <RouterView />
    </div>
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
import type { Instrument } from '@/models/trading/instrument';
import { useQuoteStore } from '@/stores/quote';
import { useTradingStore } from '@/stores/trading';

const menus = ['trading.chart', 'trading.info'];
const activeIndex = ref(menus[0] as string | undefined);
const router = useRouter();
const instrument = ref({} as Instrument);
const { lastTradedPrice, priceChange, priceRateOfChange } =
  storeToRefs(useQuoteStore());
const { getInstrument } = useTradingStore();

const { symbol, ticks, bids, asks } = storeToRefs(useQuoteStore());
const price = computed(() => ticks.value.get(symbol.value)?.price || 0);
const selectPrice = ref(undefined as number | undefined);

const changePrice = (price: number) => (selectPrice.value = price);

getInstrument(symbol.value).then((x) => (instrument.value = x));

watch(
  () => router.currentRoute,
  (currentRoute) => {
    activeIndex.value = currentRoute.value.name?.toString();
  },
  {
    immediate: true,
    deep: true,
  },
);
</script>

<style scoped lang="scss">
.trading {
  display: flex;
  align-items: flex-start;
  gap: 16px;
}

.contract {
  flex: 4;
}

.quote {
  display: flex;
  gap: 10px;
  align-items: baseline;
}

.order-book {
  flex: 1;
}

.trade-form {
  flex: 1;
}

.el-menu {
  margin-bottom: 20px;
}

.el-menu--horizontal {
  --el-menu-horizontal-height: 40px;

  &.el-menu {
    border-bottom: 0;
  }
}
</style>
