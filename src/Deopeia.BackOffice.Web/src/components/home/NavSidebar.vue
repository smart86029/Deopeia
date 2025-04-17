<template>
  <el-menu :default-active="defaultActive" router>
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

const menus = [
  {
    name: 'dashboard.default',
  },
  {
    name: 'client.module',
    children: [
      { name: 'client.trader.list' },
      { name: 'client.introducingBroker.list' },
      { name: 'client.kyc.list' },
    ],
  },
  {
    name: 'fund.module',
    children: [{ name: 'fund.deposit.list' }, { name: 'fund.withdrawal.list' }],
  },
  {
    name: 'trading.module',
    children: [
      { name: 'trading.position.list' },
      { name: 'trading.order.list' },
      { name: 'trading.trade.list' },
    ],
  },
  {
    name: 'risk.module',
    children: [
      { name: 'risk.overview' },
      { name: 'risk.marginCall.list' },
      { name: 'risk.forcedLiquidation.list' },
    ],
  },
  {
    name: 'report.module',
    children: [{ name: 'report.profitAndLoss' }, { name: 'report.cashFlow' }],
  },
  {
    name: 'setting.module',
    children: [{ name: 'setting.contract.list' }],
  },
  {
    name: 'identity.module',
    children: [
      { name: 'identity.user.list' },
      { name: 'identity.role.list' },
      { name: 'identity.permission.list' },
    ],
  },
] as Menu[];

const defaultActive = computed(() => {
  const matched = route.matched;
  return matched.length > 2 ? matched[2].name : matched[1].name;
});
</script>
