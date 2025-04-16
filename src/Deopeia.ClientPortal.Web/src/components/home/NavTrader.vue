<template>
  <el-menu
    :default-active="defaultActive"
    mode="horizontal"
    :ellipsis="false"
    router
  >
    <template v-for="menu in menus" :key="menu.name">
      <el-sub-menu v-if="menu.children" :index="menu.name">
        <template #title>
          {{ $t(`route.${menu.name}`) }}
        </template>
        <el-menu-item
          v-for="child in menu.children"
          :key="child.name"
          :index="child.name"
          :route="{ name: child.name }"
        >
          {{ $t(`route.${child.name}`) }}
        </el-menu-item>
      </el-sub-menu>
      <el-menu-item v-else :index="menu.name" :route="{ name: menu.name }">
        {{ $t(`route.${menu.name}`) }}
      </el-menu-item>
    </template>
  </el-menu>
</template>

<script setup lang="ts">
import type { Menu } from '@/models/menu';

const route = useRoute();
const menus: Menu[] = [
  { name: 'market.list' },
  { name: 'trading.view' },
  { name: 'asset.view' },
];

const defaultActive = computed(() => {
  if (route.name && route.name.toString().split('.')[0] === 'market') {
    return 'market.list';
  } else if (route.name && route.name.toString().split('.')[0] === 'trading') {
    return 'trading.view';
  }
});
</script>
