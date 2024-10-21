<template>
  <TableToolbar>
    <template #right>
      <ButtonCreate route="position.create" />
    </template>
  </TableToolbar>

  <el-table v-loading="loading" :data="result.items">
    <el-table-column
      prop="accountNumber"
      :label="$t('trading.accountNumber')"
    />
    <el-table-column prop="name" :label="$t('common.name')" />
    <el-table-column prop="side" :label="$t('trading.orderSide.name')" />
    <el-table-column prop="volume" :label="$t('trading.volume')" />
    <el-table-column prop="margin" :label="$t('trading.margin')" />
    <el-table-column prop="openPrice" :label="$t('common.openPrice')" />
    <el-table-column prop="openedAt" :label="$t('common.openedAt')" />
    <el-table-column :label="$t('common.operations')">
      <template #default="{ row }">
        <!-- <TextLink :to="{ name: 'position.edit', params: { id: row.id } }" /> -->
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
import positionApi, {
  type GetPositionsQuery,
  type Position,
} from '@/api/trading/position-api';
import { defaultQuery, defaultResult, type PageResult } from '@/models/page';

const loading = ref(false);
const query: GetPositionsQuery = reactive({
  ...defaultQuery,
});
const result: PageResult<Position> = reactive(defaultResult());

watch(
  query,
  (query) => {
    loading.value = true;
    positionApi
      .getList(query)
      .then((x) => Object.assign(result, x.data))
      .finally(() => (loading.value = false));
  },
  { immediate: true },
);
</script>
