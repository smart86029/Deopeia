<template>MarketCryptocurrency</template>

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
