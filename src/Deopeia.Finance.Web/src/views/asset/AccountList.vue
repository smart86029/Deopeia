<template>
  <div v-for="account in accounts" :key="account.currencyCode" class="account">
    <span class="currency">
      {{ currencies.find((x) => x.value === account.currencyCode)?.name }}
    </span>
    <span class="balance">{{ $n(account.balance, 'decimal') }}</span>
    <span class="frozen">{{ $n(account.frozen, 'decimal') }}</span>
    <el-button-group class="operation">
      <el-button>{{ $t('trading.deposit') }}</el-button>
      <el-button>{{ $t('trading.withdraw') }}</el-button>
    </el-button-group>
  </div>
</template>

<script setup lang="ts">
import { type Account, assetApi } from '@/api/trading/asset-api';
import { useOptionStore } from '@/stores/option';

const { currencies } = storeToRefs(useOptionStore());
const accounts: Ref<Account[]> = ref([]);

assetApi.getList().then((x) => (accounts.value = x.data));
</script>

<style lang="scss" scoped>
.account {
  display: flex;
  width: 100%;
}

.currency {
  flex: 1;
}

.balance {
  flex: 1;
}

.frozen {
  flex: 1;
}
</style>
