<template>
  <div>
    <h2>{{ instrument.name }}</h2>
    <div class="quote">
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
</template>

<script setup lang="ts">
import instrumentApi, { type Instrument } from '@/api/instrument-api';
import { useQuoteStore } from '@/stores/quote';

const props = defineProps<{
  symbol: string;
}>();

const menus = ['symbol.default', 'symbol.financials', 'symbol.news'];
const activeIndex = ref(menus[0] as string | undefined);
const router = useRouter();
const instrument = ref({} as Instrument);
const { lastTradedPrice, priceChange, priceRateOfChange } =
  storeToRefs(useQuoteStore());

instrumentApi.get(props.symbol).then((x) => (instrument.value = x.data));

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
.el-menu {
  margin-bottom: 20px;
}

.quote {
  display: flex;
  gap: 10px;
  align-items: baseline;
}

.ltp {
  font-weight: bold;
  font-size: 32px;
}
</style>
