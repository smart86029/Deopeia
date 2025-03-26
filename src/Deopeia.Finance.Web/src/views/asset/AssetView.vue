<template>
  <div class="router-view">
    <div class="overview">
      <div class="statistic-card balance">
        <el-statistic
          :value="balance"
          precision="2"
          :title="$t('trading.balance')"
        />
      </div>
      <div class="statistic-card">
        <el-statistic
          :value="accountBalance"
          precision="2"
          :title="$t('trading.account')"
        />
      </div>
      <div class="statistic-card">
        <el-statistic
          :value="positionValue"
          precision="2"
          :title="$t('trading.positionValue')"
        />
      </div>
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
  justify-content: center;

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

.el-statistic {
  --el-statistic-content-font-size: 28px;
}

.statistic-card {
  height: 100%;
  padding: 20px;
  border-radius: 4px;
  background-color: var(--el-bg-color-overlay);
  text-align: center;
  flex: 1;
}

.balance {
  flex: 2;
}

.el-menu {
  width: 256px;
}
</style>
