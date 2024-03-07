<template>
  <div class="toolbar">
    <el-form :inline="true" :model="query">
      <el-form-item :label="$t('common.status')">
        <SelectBoolean v-model="query.isEnabled" localeKey="status.isEnabled" />
      </el-form-item>
    </el-form>
    <FlexDivider />
    <ButtonCreate route="leave.apply" />
  </div>
  <el-table v-loading="loading" :data="result.items">
    <el-table-column prop="title" :label="$t('organization.jobTitle')" />
    <el-table-column prop="isEnabled" :label="$t('common.status')">
      <template #default="{ row }">
        <TextBoolean :value="row.isEnabled" localeKey="status.isEnabled" />
      </template>
    </el-table-column>
    <el-table-column
      prop="employeeCount"
      :label="$t('organization.employeeCount')"
    />
    <el-table-column :label="$t('common.operations')">
      <template #default="{ row }">
        <TextLink
          :to="{ name: 'operator.edit', params: { id: row.id } }"
          :text="$t('operation.edit')"
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
import jobApi, { type GetJobsQuery, type Job } from '@/api/job-api';
import { defaultQuery, defaultResult, type PageResult } from '@/models/page';

const loading = ref(false);
const query: GetJobsQuery = reactive({
  isEnabled: undefined as boolean | undefined,
  ...defaultQuery,
});
const result: PageResult<Job> = reactive(defaultResult());

watch(
  query,
  (query) => {
    loading.value = true;
    jobApi
      .getList(query)
      .then((x) => Object.assign(result, x.data))
      .finally(() => (loading.value = false));
  },
  { immediate: true },
);
</script>
