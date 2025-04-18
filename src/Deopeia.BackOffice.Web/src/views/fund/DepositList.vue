<template>
  <TableToolbar>
    <el-form :model="query" :inline="true">
      <el-form-item :label="$t('identity.code')">
        <el-input v-model="query.code" />
      </el-form-item>
      <el-form-item :label="$t('common.status')">
        <SelectBoolean
          v-model="query.isEnabled"
          locale-key="status.isEnabled"
        />
      </el-form-item>
    </el-form>
  </TableToolbar>

  <el-table v-loading="loading" :data="result.items">
    <el-table-column prop="id" label="ID" />
    <el-table-column prop="userName" :label="$t('common.name')" />
    <el-table-column prop="currency" :label="$t('common.currency')" />
    <TableColumnDecimal prop="amount" :label="$t('finance.amount')" />
    <TableColumnBoolean
      prop="isEnabled"
      :label="$t('common.status')"
      localeKey="status.isEnabled"
    />
    <TableColumnDateTime prop="openedAt" :label="$t('trading.openedAt')" />
    <el-table-column :label="$t('common.operations')">
      <template #default="{ row }">
        <TextLink
          :to="{ name: 'identity.permission.edit', params: { code: row.code } }"
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
import type { DepositRow, GetDepositsQuery } from '@/api/fund-api';
import { defaultQuery, defaultResult, type PageResult } from '@/models/page';

const loading = ref(false);
const query: GetDepositsQuery = reactive({
  ...defaultQuery,
});
const result: PageResult<DepositRow> = reactive(defaultResult());

// watch(
//   query,
//   (query) => {
//     if (!loading.value) {
//       loading.value = true;
//       permissionApi
//         .getList(query)
//         .then((x) => reassign(query, result, x.data))
//         .finally(() => (loading.value = false));
//     }
//   },
//   { immediate: true },
// );
</script>
