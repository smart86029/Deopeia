<template>
  <TableToolbar>
    <template #right>
      <ButtonCreate route="strategy.create" />
    </template>
  </TableToolbar>

  <el-table v-loading="loading" :data="result.items">
    <el-table-column prop="code" :label="$t('common.code')" />
    <el-table-column prop="name" :label="$t('common.name')" />
    <el-table-column prop="description" :label="$t('common.description')">
      <template #default="{ row }">
        <el-text truncated>{{ row.description }}</el-text>
      </template>
    </el-table-column>
    <el-table-column :label="$t('common.operations')">
      <template #default="{ row }">
        <TextLink :to="{ name: 'strategy.edit', params: { id: row.id } }" />
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
import assetApi, {
  type Asset,
  type GetAssetsQuery,
} from '@/api/trading/asset-api';
import { defaultQuery, defaultResult, type PageResult } from '@/models/page';

const loading = ref(false);
const query: GetAssetsQuery = reactive({
  ...defaultQuery,
});
const result: PageResult<Asset> = reactive(defaultResult());

watch(
  query,
  (query) => {
    loading.value = true;
    assetApi
      .getList(query)
      .then((x) => Object.assign(result, x.data))
      .finally(() => (loading.value = false));
  },
  { immediate: true },
);
</script>
