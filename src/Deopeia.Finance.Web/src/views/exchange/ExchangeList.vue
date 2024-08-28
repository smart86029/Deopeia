<template>
  <TableToolbar>
    <template #right>
      <ButtonCreate route="exchange.create" />
    </template>
  </TableToolbar>

  <el-table v-loading="loading" :data="result.items">
    <el-table-column prop="code" :label="$t('finance.code')" />
    <el-table-column prop="name" :label="$t('common.name')" />
    <el-table-column prop="openingTime" :label="$t('finance.openingTime')" />
    <el-table-column prop="closingTime" :label="$t('finance.closingTime')" />
    <el-table-column prop="timeZone" :label="$t('common.timeZone')" />
    <el-table-column :label="$t('common.operations')">
      <template #default="{ row }">
        <TextLink :to="{ name: 'exchange.edit', params: { id: row.id } }" />
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
import exchangeApi, {
  type Exchange,
  type GetExchangesQuery,
} from '@/api/exchange-api';
import { defaultQuery, defaultResult, type PageResult } from '@/models/page';

const loading = ref(false);
const query: GetExchangesQuery = reactive({
  ...defaultQuery,
});
const result: PageResult<Exchange> = reactive(defaultResult());

watch(
  query,
  (query) => {
    loading.value = true;
    exchangeApi
      .getList(query)
      .then((x) => Object.assign(result, x.data))
      .finally(() => (loading.value = false));
  },
  { immediate: true },
);
</script>
