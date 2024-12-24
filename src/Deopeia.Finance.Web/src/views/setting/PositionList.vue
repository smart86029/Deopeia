<template>
  <TableToolbar>
    <el-form :model="query" :inline="true">
      <el-form-item :label="$t('common.type')">
        <SelectEnum
          v-model="query.type"
          :enum="PositionType"
          localeKey="trading.positionType"
        />
      </el-form-item>
    </el-form>
  </TableToolbar>

  <el-table v-loading="loading" :data="result.items" table-layout="auto">
    <el-table-column prop="accountNumber" :label="$t('trading.account')" />
    <el-table-column prop="name" :label="$t('common.name')" />
    <TableColumnEnum
      prop="type"
      :label="$t('common.type')"
      localeKey="trading.positionType"
    />
    <TableColumnInteger prop="volume" :label="$t('trading.volume')" />
    <el-table-column prop="currency" :label="$t('common.currency')" />
    <TableColumnDecimal prop="openPrice" :label="$t('trading.openPrice')" />
    <TableColumnDecimal prop="margin" :label="$t('trading.margin')" />
    <TableColumnFluctuation
      prop="price"
      :label="$t('finance.price')"
      comparisonProp="openPrice"
    />
    <TableColumnFluctuation
      prop="unrealisedPnL"
      :label="$t('trading.unrealisedPnL')"
    />
    <TableColumnDateTime prop="openedAt" :label="$t('trading.openedAt')" />
    <el-table-column :label="$t('common.operations')">
      <template #default="{ row }">
        <TextLink
          :to="{ name: 'position.close', params: { id: row.id } }"
          :text="$t('trading.close')"
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
import positionApi, {
  type GetPositionsQuery,
  type PositionRow,
} from '@/api/trading/position-api';
import { defaultQuery, defaultResult, type PageResult } from '@/models/page';
import { PositionType } from '@/models/trading/position-type';
import { useQuoteStore } from '@/stores/quote';

const loading = ref(false);
const query: GetPositionsQuery = reactive({
  ...defaultQuery,
});
const result: PageResult<PositionRow> = reactive(defaultResult());
const { ticks } = storeToRefs(useQuoteStore());

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

watch(
  ticks,
  (ticks) => {
    result.items.forEach((x) => {
      const sign = x.type == PositionType.Long ? 1 : -1;
      const price = ticks.get(x.symbol)!.price;
      x.price = price;
      x.unrealisedPnL = (price - x.openPrice) * 1000 * sign;
    });
  },
  { deep: true },
);
</script>
