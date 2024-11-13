<template>
  <TableToolbar>
    <el-form :model="query" :inline="true">
      <el-form-item :label="$t('common.status')">
        <SelectBoolean v-model="query.isEnabled" localeKey="status.isEnabled" />
      </el-form-item>
      <el-form-item :label="$t('common.currency')">
        <SelectOption v-model="query.currencyCode" :options="currencies" />
      </el-form-item>
    </el-form>
    <template #right>
      <ButtonCreate route="account.create" />
    </template>
  </TableToolbar>

  <el-table v-loading="loading" :data="result.items">
    <el-table-column
      prop="accountNumber"
      :label="$t('trading.accountNumber')"
    />
    <TableColumnBoolean
      prop="isEnabled"
      :label="$t('common.status')"
      localeKey="status.isEnabled"
    />
    <el-table-column prop="currency" :label="$t('common.currency')">
      <template #default="{ row }">
        {{ currencies.find((x) => x.value === row.currencyCode)?.name }}
      </template>
    </el-table-column>
    <TableColumnDecimal prop="balance" :label="$t('trading.balance')" />
    <el-table-column :label="$t('common.operations')">
      <template #default="{ row }">
        <el-space spacer="|">
          <TextLink :to="{ name: 'account.edit', params: { id: row.id } }" />
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
        </el-space>
      </template>
    </el-table-column>
  </el-table>

  <TablePagination
    v-model:current-page="query.pageIndex"
    v-model:page-size="query.pageSize"
    :total="result.itemCount"
  />

  <DialogDeposit v-model="depositVisible" :account="account" />
  <DialogWithdraw v-model="withdrawVisible" :account="account" />
</template>

<script setup lang="ts">
import optionApi from '@/api/option-api';
import accountApi, {
  type AccountRow,
  type GetAccountsQuery,
} from '@/api/trading/account-api';
import type { OptionResult } from '@/models/option-result';
import { defaultQuery, defaultResult, type PageResult } from '@/models/page';

const loading = ref(false);
const depositVisible = ref(false);
const withdrawVisible = ref(false);
const account = ref({} as AccountRow);
const currencies: Ref<OptionResult<string>[]> = ref([]);
const query: GetAccountsQuery = reactive({
  ...defaultQuery,
});
const result: PageResult<AccountRow> = reactive(defaultResult());

optionApi.getCurrencies().then((x) => (currencies.value = x.data));

const deposit = (row: AccountRow) => {
  account.value = row;
  depositVisible.value = true;
};

const withdraw = (row: AccountRow) => {
  account.value = row;
  withdrawVisible.value = true;
};

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
