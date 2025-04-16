<template>
  <TableContract v-loading="loading" :data="result.items" />
  <TablePagination
    v-model:current-page="query.pageIndex"
    v-model:page-size="query.pageSize"
    :total="result.itemCount"
  />
</template>

<script setup lang="ts">
import { marketApi, type Favorite } from '@/api/market/market-api';
import { type GetStocksQuery } from '@/api/stock-api';
import {
  defaultQuery,
  defaultResult,
  reassign,
  type PageResult,
} from '@/models/page';

const loading = ref(false);
const query: GetStocksQuery = reactive({
  ...defaultQuery,
});
const result: PageResult<Favorite> = reactive(defaultResult());

watch(
  query,
  (query) => {
    if (!loading.value) {
      loading.value = true;
      marketApi
        .getFavorite(query)
        .then((x) => reassign(query, result, x.data))
        .finally(() => (loading.value = false));
    }
  },
  { immediate: true },
);
</script>
