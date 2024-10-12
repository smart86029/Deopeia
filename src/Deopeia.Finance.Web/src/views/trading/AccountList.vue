<template>
  <TableToolbar>
    <template #right>
      <ButtonCreate route="account.create" />
    </template>
  </TableToolbar>

  <el-table v-loading="loading" :data="result.items">
    <el-table-column prop="accountNumber" :label="$t('trading.accountNumber')" />
    <el-table-column prop="isEnabled" :label="$t('common.status')">
      <template #default="{ row }">
        <TextBoolean :value="row.isEnabled" localeKey="status.isEnabled" />
      </template>
    </el-table-column>
    <el-table-column :label="$t('common.operations')">
      <template #default="{ row }">
        <TextLink :to="{ name: 'account.edit', params: { id: row.id } }" />
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
import accountApi, {
  type GetAccountsQuery,
  type AccountRow,
} from '@/api/trading/account-api';
import { defaultQuery, defaultResult, type PageResult } from '@/models/page';

const loading = ref(false);
const query: GetAccountsQuery = reactive({
  ...defaultQuery,
});
const result: PageResult<AccountRow> = reactive(defaultResult());

watch(
  query,
  (query) => {
    loading.value = true;
    accountApi
      .getList(query)
      .then((x) => Object.assign(result, x.data))
      .finally(() => (loading.value = false));
  },
  { immediate: true },
);
</script>
