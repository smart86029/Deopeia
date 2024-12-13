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
    <TableColumnFluctuation
      prop="price"
      :label="$t('finance.price')"
      :comparison="90"
    />
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
import marketApi, {
  type GetIndexQuery,
  type Index,
} from '@/api/market/market-api';

import {
  defaultQuery,
  defaultResult,
  reassign,
  type PageResult,
} from '@/models/page';
import { useQuoteStore } from '@/stores/quote';

const quote = useQuoteStore();

const loading = ref(false);
const query: GetIndexQuery = reactive({
  ...defaultQuery,
});
const result: PageResult<Index> = reactive(defaultResult());

watch(
  query,
  (query) => {
    if (!loading.value) {
      loading.value = true;
      marketApi
        .getIndex(query)
        .then((x) => {
          x.data.items = x.data.items.map((item) => ({
            ...item,
            get price() {
              return quote.quotes.get(item.symbol)?.value || 0;
            },
          }));
          reassign(query, result, x.data);
        })
        .finally(() => (loading.value = false));
    }
  },
  { immediate: true },
);
</script>
