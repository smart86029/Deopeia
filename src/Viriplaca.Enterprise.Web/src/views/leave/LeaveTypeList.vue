<template>
  <div class="toolbar">
    <el-form :inline="true" :model="query"></el-form>
    <FlexDivider />
    <ButtonCreate route="leave.type.create" />
  </div>
  <el-table v-loading="loading" :data="result.items">
    <el-table-column prop="name" :label="$t('common.name')" />
    <el-table-column :label="$t('leave.canCarryForward')">
      <template #default="{ row }">
        <TextBoolean :value="row.canCarryForward" localeKey="status.yesNo" />
      </template>
    </el-table-column>
    <el-table-column :label="$t('common.operations')">
      <template #default="{ row }">
        <TextLink :to="{ name: 'leave.type.edit', params: { id: row.id } }" />
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
import leaveTypeApi, {
  type GetLeaveTypesQuery,
  type LeaveTypeRow,
} from '@/api/leave/leave-type-api';
import { defaultQuery, defaultResult, type PageResult } from '@/models/page';
import { rangeWeek } from '@/plugins/dayjs';
const loading = ref(false);
const range = ref(rangeWeek());
const query: GetLeaveTypesQuery = reactive({
  ...defaultQuery,
});
const result: PageResult<LeaveTypeRow> = reactive(defaultResult());

watch(
  [query, range],
  ([query, [startedAt, endedAt]]) => {
    loading.value = true;
    leaveTypeApi
      .getList(Object.assign(query, { startedAt, endedAt }))
      .then((x) => Object.assign(result, x.data))
      .finally(() => (loading.value = false));
  },
  { immediate: true },
);
</script>
