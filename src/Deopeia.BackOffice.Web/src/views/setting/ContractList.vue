<template>
  <TableToolbar>
    <el-form :model="query" :inline="true">
      <el-form-item :label="$t('finance.underlyingType.name')">
        <SelectEnum
          v-model="query.underlyingType"
          :enum="UnderlyingType"
          localeKey="finance.underlyingType"
        />
      </el-form-item>
    </el-form>
    <template #right>
      <ButtonCreate route="contract.create" />
    </template>
  </TableToolbar>

  <el-table v-loading="loading" :data="result.items" table-layout="auto">
    <el-table-column prop="symbol" :label="$t('finance.symbol')" />
    <el-table-column prop="name" :label="$t('common.name')" />
    <TableColumnEnum
      prop="underlyingType"
      :label="$t('finance.underlyingType.name')"
      localeKey="finance.underlyingType"
    />
    <el-table-column prop="currency" :label="$t('common.currency')" />
    <el-table-column :label="$t('common.operations')">
      <template #default="{ row }">
        <TextLink
          :to="{ name: 'contract.edit', params: { symbol: row.symbol } }"
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
import {
  contractApi,
  type ContractRow,
  type GetContractsQuery,
} from '@/api/trading/contract-api';
import { defaultQuery, defaultResult, type PageResult } from '@/models/page';
import { UnderlyingType } from '@/models/underlying-type';

const loading = ref(false);
const query: GetContractsQuery = reactive({
  ...defaultQuery,
});
const result: PageResult<ContractRow> = reactive(defaultResult());

watch(
  query,
  (query) => {
    loading.value = true;
    contractApi
      .getList(query)
      .then((x) => Object.assign(result, x.data))
      .finally(() => (loading.value = false));
  },
  { immediate: true },
);
</script>
