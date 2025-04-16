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
      <RouterView class="router-view" />
    </div>
    <OrderBook
      class="order-book"
      :bids="bids"
      :asks="asks"
      :price="price"
      @update="changePrice"
    />
    <TradeForm class="trade-form" :price="selectPrice" />
    <TablePosition class="position" />
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
$gap: 10px;

.trading {
  display: grid;
  grid-template-columns: 4fr 1fr 1fr;
  grid-template-rows: 1fr 200px;
  gap: $gap;
  height: calc(100vh - 100px);
}

.contract {
  grid-row: 1/2;
  grid-column: 1/2;
  display: flex;
  flex-direction: column;

  .router-view {
    flex: 1;
  }
}

.trade-form {
  grid-row: 1/3;
  grid-column: 3/4;
}

.position {
  grid-row: 2/3;
  grid-column: 1/3;
}

.el-menu {
  margin-bottom: $gap;
}

.el-menu--horizontal {
  --el-menu-horizontal-height: 40px;

  &.el-menu {
    border-bottom: 0;
  }
}
</style>
