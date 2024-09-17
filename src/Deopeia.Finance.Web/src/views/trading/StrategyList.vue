<template>
  <TableToolbar>
    <template #right>
      <ButtonCreate route="strategy.create" />
    </template>
  </TableToolbar>

  <el-table v-loading="loading" :data="result.items">
    <el-table-column prop="name" :label="$t('common.name')" />
    <el-table-column prop="description" :label="$t('common.description')">
      <template #default="{ row }">
        <el-text truncated>{{ row.description }}</el-text>
      </template>
    </el-table-column>
    <el-table-column prop="isEnabled" :label="$t('common.status')">
      <template #default="{ row }">
        <TextBoolean :value="row.isEnabled" localeKey="status.isEnabled" />
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
import strategyApi, {
  type GetStrategiesQuery,
  type Strategy,
} from '@/api/trading/strategy-api';
import { defaultQuery, defaultResult, type PageResult } from '@/models/page';

const loading = ref(false);
const query: GetStrategiesQuery = reactive({
  ...defaultQuery,
});
const result: PageResult<Strategy> = reactive(defaultResult());

watch(
  query,
  (query) => {
    loading.value = true;
    strategyApi
      .getList(query)
      .then((x) => Object.assign(result, x.data))
      .finally(() => (loading.value = false));
  },
  { immediate: true },
);
</script>
