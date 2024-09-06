<template>
  <el-menu
    :default-active="
      $route.matched.length > 2 ? $route.matched[2].path : $route.path
    "
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
          :index="child.path"
        >
          {{ $t(`route.${child.name}`) }}
        </el-menu-item>
      </el-sub-menu>
      <el-menu-item v-else :index="menu.path">
        {{ $t(`route.${menu.name}`) }}
      </el-menu-item>
    </template>
  </el-menu>
</template>

<script setup lang="ts">
import type { Menu } from '@/models/menu';

const menus = [
  {
    name: 'screener.default',
    children: [{ name: 'screener.stock', path: '/screeners/stock' }],
  },
  {
    name: 'exchange.default',
    children: [{ name: 'exchange.list', path: '/exchanges' }],
  },
  {
    name: 'identity',
    children: [{ name: 'user.list', path: '/users' }],
  },
] as Menu[];
</script>
