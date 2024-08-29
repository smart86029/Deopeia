<template>
  <TableToolbar>
    <el-form :model="query" :inline="true">
      <el-form-item :label="$t('finance.industry')">
        <SelectOption v-model="query.industry" :options="industries" />
      </el-form-item>
    </el-form>
  </TableToolbar>

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
    <el-table-column prop="industry" :label="$t('finance.industry')" />
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
import { defaultQuery, defaultResult, type PageResult } from '@/models/page';

const loading = ref(false);
const industries: Ref<OptionResult<number>[]> = ref([]);
const query: GetStocksQuery = reactive({
  industry: undefined as number | undefined,
  ...defaultQuery,
});
const result: PageResult<Stock> = reactive(defaultResult());

industryApi.getOptions().then((x) => (industries.value = x.data));

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
