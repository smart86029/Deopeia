<template>
  <TableContract v-loading="loading" :data="result.items" />
  <TablePagination
    v-model:current-page="query.pageIndex"
    v-model:page-size="query.pageSize"
    :total="result.itemCount"
  />
</template>

<script setup lang="ts">
import {
  marketApi,
  type Commodity,
  type GetCommodityQuery,
} from '@/api/market/market-api';

import {
  defaultQuery,
  defaultResult,
  reassign,
  type PageResult,
} from '@/models/page';

const loading = ref(false);
const query: GetCommodityQuery = reactive({
  ...defaultQuery,
});
const result: PageResult<Commodity> = reactive(defaultResult());

watch(
  query,
  (query) => {
    if (!loading.value) {
      loading.value = true;
      marketApi
        .getCommodity(query)
        .then((x) => reassign(query, result, x.data))
        .finally(() => (loading.value = false));
    }
  },
  { immediate: true },
);
</script>
