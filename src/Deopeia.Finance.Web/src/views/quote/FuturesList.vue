<template>
  <TableToolbar>
    <template #right>
      <ButtonCreate route="futures.create" />
    </template>
  </TableToolbar>

  <el-table v-loading="loading" :data="result.items" table-layout="auto">
    <el-table-column prop="symbol" :label="$t('finance.symbol')" />
    <el-table-column prop="name" :label="$t('common.name')" />
    <el-table-column prop="exchange" :label="$t('finance.exchange')" />
    <el-table-column
      prop="underlyingAsset"
      :label="$t('finance.underlyingAsset')"
    />
    <el-table-column prop="currency" :label="$t('common.currency')" />
    <el-table-column :label="$t('common.operations')">
      <template #default="{ row }">
        <TextLink :to="{ name: 'futures.edit', params: { id: row.id } }" />
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
import assetApi from '@/api/quote/asset-api';
import futuresApi, {
  type FuturesRow,
  type GetFuturessQuery,
} from '@/api/quote/futures-api';
import type { Guid } from '@/models/guid';
import type { OptionResult } from '@/models/option-result';
import { defaultQuery, defaultResult, type PageResult } from '@/models/page';

const loading = ref(false);
const assets: Ref<OptionResult<Guid>[]> = ref([]);
const query: GetFuturessQuery = reactive({
  ...defaultQuery,
});
const result: PageResult<FuturesRow> = reactive(defaultResult());

assetApi.getOptions().then((x) => (assets.value = x.data));

watch(
  query,
  (query) => {
    loading.value = true;
    futuresApi
      .getList(query)
      .then((x) => Object.assign(result, x.data))
      .finally(() => (loading.value = false));
  },
  { immediate: true },
);
</script>
