<template>
  <TableToolbar>
    <el-form :model="query" :inline="true">
      <el-form-item :label="$t('common.status')">
        <SelectBoolean v-model="query.isEnabled" localeKey="status.isEnabled" />
      </el-form-item>
    </el-form>
    <template #right>
      <ButtonCreate route="client.trader.create" />
    </template>
  </TableToolbar>

  <el-table v-loading="loading" :data="result.items">
    <el-table-column prop="name" :label="$t('common.name')" />
    <TableColumnBoolean
      prop="isEnabled"
      :label="$t('common.status')"
      localeKey="status.isEnabled"
    />
    <TableColumnDecimal prop="balance" :label="$t('trading.balance')" />
    <el-table-column :label="$t('common.operations')">
      <template #default="{ row }">
        <DividerSpace>
          <TextLink
            :to="{ name: 'client.trader.edit', params: { id: row.id } }"
          />
          <el-link type="primary" @click="deposit(row)">
            {{ $t('finance.deposit') }}
          </el-link>
          <el-link
            type="primary"
            @click="withdraw(row)"
            :disabled="row.balance <= 0"
          >
            {{ $t('finance.withdraw') }}
          </el-link>
        </DividerSpace>
      </template>
    </el-table-column>
  </el-table>

  <TablePagination
    v-model:current-page="query.pageIndex"
    v-model:page-size="query.pageSize"
    :total="result.itemCount"
  />

  <DialogDeposit v-model="depositVisible" :trader="trader" />
  <DialogWithdraw v-model="withdrawVisible" :trader="trader" />
</template>

<script setup lang="ts">
import {
  traderApi,
  type GetTradersQuery,
  type TraderRow,
} from '@/api/setting/trader-api';
import { defaultQuery, defaultResult, type PageResult } from '@/models/page';

const loading = ref(false);
const depositVisible = ref(false);
const withdrawVisible = ref(false);
const trader = ref({} as TraderRow);
const query: GetTradersQuery = reactive({
  ...defaultQuery,
});
const result: PageResult<TraderRow> = reactive(defaultResult());

const deposit = (row: TraderRow) => {
  trader.value = row;
  depositVisible.value = true;
};

const withdraw = (row: TraderRow) => {
  trader.value = row;
  withdrawVisible.value = true;
};

watch(
  query,
  (query) => {
    loading.value = true;
    traderApi
      .getList(query)
      .then((x) => Object.assign(result, x.data))
      .finally(() => (loading.value = false));
  },
  { immediate: true },
);
</script>
