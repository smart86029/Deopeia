<template>
  <h2>{{ instrument.name }}</h2>

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
</template>

<script setup lang="ts">
import instrumentApi, { type Instrument } from '@/api/instrument-api';

const props = defineProps<{
  symbol: string;
}>();

const menus = ['symbol.default', 'symbol.financials', 'symbol.news'];
const activeIndex = ref(menus[0] as string | undefined);
const router = useRouter();
const instrument = ref({} as Instrument);

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
</style>
