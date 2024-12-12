<template>
  <el-table v-loading="loading" :data="result.items" table-layout="auto">
    <el-table-column :label="$t('finance.symbol')">
      <template #default="{ row }">
        <TextLink
          :to="{ name: 'symbol.view', params: { symbol: row.symbol } }"
          :text="row.symbol"
        />
      </template>
    </el-table-column>
    <el-table-column prop="name" :label="$t('common.name')" />
    <el-table-column prop="price" :label="$t('finance.price')" />
    <el-table-column prop="priceChange" :label="$t('finance.priceChange')" />
    <el-table-column prop="volume" :label="$t('finance.volume')" />
    <el-table-column :label="$t('route.trading')">
      <template #default="{ row }">
        <el-button
          size="small"
          :type="positive"
          @click="$router.push({ name: 'trading' })"
        >
          {{ $t(`trading.orderSide.${OrderSide.Buy}`) }}
        </el-button>
        <el-button
          size="small"
          :type="negative"
          @click="$router.push({ name: 'trading' })"
        >
          {{ $t(`trading.orderSide.${OrderSide.Sell}`) }}
        </el-button>
      </template>
    </el-table-column>
  </el-table>

  <TablePagination
    v-model:current-page="query.pageIndex"
    v-model:page-size="query.pageSize"
    :total="result.itemCount"
  />
</template>

<script setup lang="ts">
import marketApi, {
  type Forex,
  type GetForexQuery,
} from '@/api/market/market-api';
import {
  defaultQuery,
  defaultResult,
  reassign,
  type PageResult,
} from '@/models/page';
import { OrderSide } from '@/models/trading/order-side';
import { usePreferencesStore } from '@/stores/preferences';

const { positive, negative } = storeToRefs(usePreferencesStore());

const loading = ref(false);
const query: GetForexQuery = reactive({
  ...defaultQuery,
});
const result: PageResult<Forex> = reactive(defaultResult());

watch(
  query,
  (query) => {
    if (!loading.value) {
      loading.value = true;
      marketApi
        .getForex(query)
        .then((x) => reassign(query, result, x.data))
        .finally(() => (loading.value = false));
    }
  },
  { immediate: true },
);
</script>
