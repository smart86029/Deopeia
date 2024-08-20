<template>
  <el-table v-loading="loading" :data="result.items">
    <el-table-column prop="symbol" :label="$t('finance.symbol')" />
    <el-table-column prop="name" :label="$t('common.name')" />
    <el-table-column prop="price" :label="$t('finance.price')" />
    <el-table-column prop="priceChange" :label="$t('finance.priceChange')" />
    <el-table-column prop="volume" :label="$t('finance.volume')" />
    <el-table-column
      prop="priceToEarningsRatio"
      :label="$t('finance.priceToEarningsRatio')"
    />
    <el-table-column
      prop="priceBookRatio"
      :label="$t('finance.priceBookRatio')"
    />
    <el-table-column
      prop="dividendYield"
      :label="$t('finance.dividendYield')"
    />
    <el-table-column prop="sector" :label="$t('finance.sector')" />
  </el-table>
  <TablePagination
    v-model:current-page="query.pageIndex"
    v-model:page-size="query.pageSize"
    :total="result.itemCount"
  />
</template>

<script setup lang="ts">
import stockApi, { type GetStocksQuery, type Stock } from '@/api/stock-api';
import { defaultQuery, defaultResult, type PageResult } from '@/models/page';

const loading = ref(false);
const query: GetStocksQuery = reactive({
  ...defaultQuery,
});
const result: PageResult<Stock> = reactive(defaultResult());

watch(
  query,
  (query) => {
    loading.value = true;
    stockApi
      .getList(query)
      .then((x) => Object.assign(result, x.data))
      .finally(() => (loading.value = false));
  },
  { immediate: true },
);
</script>
