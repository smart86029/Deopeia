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
        <TextLink
          :to="{ name: 'symbol.view', params: { symbol: row.symbol } }"
          :text="row.symbol"
        />
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
import industryApi from '@/api/industry-api';
import stockApi, { type GetStocksQuery, type Stock } from '@/api/stock-api';
import type { OptionResult } from '@/models/option-result';
import {
  defaultQuery,
  defaultResult,
  reassign,
  type PageResult,
} from '@/models/page';

const loading = ref(false);
const industries: Ref<OptionResult<number>[]> = ref([]);
const query: GetStocksQuery = reactive({
  ...defaultQuery,
});
const result: PageResult<Stock> = reactive(defaultResult());

industryApi.getOptions().then((x) => (industries.value = x.data));

watch(
  query,
  (query) => {
    if (!loading.value) {
      loading.value = true;
      stockApi
        .getList(query)
        .then((x) => reassign(query, result, x.data))
        .finally(() => (loading.value = false));
    }
  },
  { immediate: true },
);
</script>
