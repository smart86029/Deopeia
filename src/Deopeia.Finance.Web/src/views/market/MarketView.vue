<template>
  <div class="flex">
    <el-menu
      :default-active="$route.name"
      mode="horizontal"
      :ellipsis="false"
      router
    >
      <el-menu-item
        index="market.favorite"
        :route="{ name: 'market.favorite' }"
      >
        {{ $t(`route.market.favorite`) }}
      </el-menu-item>
      <el-menu-item
        v-for="menu in menus"
        :key="menu.name"
        :index="menu.name"
        :route="{ name: menu.name }"
      >
        {{ $t(`route.${menu.name}`) }}
      </el-menu-item>
    </el-menu>
    <RouterView />
  </div>
</template>

<script setup lang="ts">
import type { Menu } from '@/models/menu';
import { UnderlyingType } from '@/models/underlying-type';

const types: UnderlyingType[] = [
  UnderlyingType.Stock,
  UnderlyingType.Index,
  UnderlyingType.Commodity,
  UnderlyingType.Forex,
  UnderlyingType.Cryptocurrency,
];
const menus: Menu[] = types.map((type) => ({
  name: `market.${UnderlyingType[type].toLocaleLowerCase()}`,
}));
</script>

<style lang="scss" scoped>
.el-menu {
  margin-bottom: 20px;
}

.el-menu--horizontal {
  --el-menu-horizontal-height: 40px;

  &.el-menu {
    border-bottom: 0;
  }
}

.flex {
  display: flex;
  flex-direction: column;
  align-items: center;

  > * {
    width: 1400px;
  }
}
</style>
