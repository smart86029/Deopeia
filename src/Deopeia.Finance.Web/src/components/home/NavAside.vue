<template>
  <el-menu :default-active="$route.name?.toString().split('.')[0]" router>
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

const menus = [
  {
    name: 'screener.default',
    children: [{ name: 'screener.stock' }],
  },
  {
    name: 'exchange.default',
    children: [{ name: 'exchange.list' }],
  },
  {
    name: 'trading',
    children: [{ name: 'asset.list' }, { name: 'strategy.list' }],
  },
  {
    name: 'identity',
    children: [
      { name: 'user.list' },
      { name: 'role.list' },
      { name: 'permission.list' },
    ],
  },
] as Menu[];
</script>
