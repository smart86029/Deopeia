<template>
  <div class="toolbar">
    <el-form :inline="true" :model="query">
      <el-form-item :label="$t('common.status')">
        <SelectEnum
          v-model="query.approvalStatus"
          :enum="ApprovalStatus"
          localeKey="status.approval"
        />
      </el-form-item>
    </el-form>
    <FlexDivider />
    <ButtonCreate route="leave.apply" :text="$t('operation.apply')" />
  </div>
  <el-table v-loading="loading" :data="result.items">
    <el-table-column prop="type" :label="$t('common.type')" />
    <el-table-column prop="startedAt" :label="$t('common.startTime')" />
    <el-table-column prop="endedAt" :label="$t('common.endTime')" />
    <el-table-column prop="duration" :label="$t('common.duration')" />
    <el-table-column prop="status" :label="$t('common.status')" />
    <el-table-column :label="$t('common.operations')">
      <template #default="{ row }">
        <TextLink
          :to="{ name: 'operator.edit', params: { id: row.id } }"
          :text="$t('operation.edit')"
        />
      </template>
    </el-table-column>
  </el-table>
</template>

<script setup lang="ts">
import leaveApi, { type GetLeavesQuery, type Leave } from '@/api/leave-api';
import { ApprovalStatus } from '@/models/approval-status';
import { defaultQuery, defaultResult, type PageResult } from '@/models/page';
import { dayjs } from 'element-plus';

const loading = ref(false);
const query: GetLeavesQuery = reactive({
  startedAt: dayjs(),
  endedAt: dayjs(),
  approvalStatus: undefined,
  ...defaultQuery,
});
const result: PageResult<Leave> = reactive(defaultResult());

watch(
  query,
  (query) => {
    loading.value = true;
    leaveApi
      .getList(query)
      .then((x) => Object.assign(result, x.data))
      .finally(() => (loading.value = false));
  },
  { immediate: true },
);
</script>
