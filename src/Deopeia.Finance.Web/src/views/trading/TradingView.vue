<template>
  <div class="trading">
    <div class="contract">
      <SymbolQuote />
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
import { useQuoteStore } from '@/stores/quote';

const router = useRouter();
const { symbol, ticks, bids, asks } = storeToRefs(useQuoteStore());

const menus = ['trading.chart', 'trading.info'];
const activeIndex = ref(menus[0] as string | undefined);
const price = computed(() => ticks.value.get(symbol.value)?.price || 0);
const selectPrice = ref(undefined as number | undefined);

const changePrice = (price: number) => (selectPrice.value = price);

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
