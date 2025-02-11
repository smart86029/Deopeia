<template>
  <div class="router-view">
    <div class="overview">
      <el-card class="balance">
        <el-text>{{ $t('trading.balance') }}</el-text>
        <el-text class="amount">
          {{ $n(balance, 'decimal') }}
        </el-text>
      </el-card>
      <el-card>
        <el-text>{{ $t('trading.account') }}</el-text>
        <el-text class="amount">
          {{ $n(accountBalance, 'decimal') }}
        </el-text>
      </el-card>
      <el-card>
        <el-text>{{ $t('trading.positionValue') }}</el-text>
        <el-text class="amount">
          {{ $n(positionValue, 'decimal') }}
        </el-text>
      </el-card>
    </div>
    <div class="flex">
      <el-menu :default-active="activeIndex" router>
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
  </div>
</template>

<script setup lang="ts">
import { assetApi, type Account } from '@/api/trading/asset-api';

const menus = ['asset.account', 'asset.position'];
const activeIndex = ref(menus[0] as string | undefined);
const accounts: Ref<Account[]> = ref([]);
const balance = computed(() => accountBalance.value + positionValue);
const accountBalance = computed(() =>
  accounts.value.reduce((x, y) => x + y.balance, 0),
);
const positionValue = 50;

assetApi.getList().then((x) => (accounts.value = x.data));
</script>

<style lang="scss" scoped>
.router-view {
  display: flex;
  flex-direction: column;
  align-items: center;

  > * {
    width: 1400px;
  }
}

.overview {
  display: flex;
  gap: 16px;
}

.flex {
  display: flex;
  gap: 16px;
  margin-top: 16px;
}

.el-card {
  flex: 1;
  background-color: var(--el-color-primary-light-5);

  :deep(.el-card__body) {
    display: flex;
    gap: 8px;
  }
}

.balance {
  flex: 2;
}

.el-text {
  font-size: 1.5em;
}

.amount {
  flex: 1;
  text-align: right;
}

.el-menu {
  width: 256px;
}
</style>
