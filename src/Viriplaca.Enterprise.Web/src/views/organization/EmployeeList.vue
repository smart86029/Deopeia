<template>
  <div class="toolbar">
    <el-form :inline="true" :model="query">
      <el-form-item :label="$t('organization.department')">
        <SelectOption :options="departments" v-model="query.departmentId" />
      </el-form-item>
      <el-form-item :label="$t('organization.jobTitle')">
        <SelectOption :options="jobs" v-model="query.jobId" />
      </el-form-item>
    </el-form>
    <FlexDivider />
    <ButtonCreate route="leave.apply" />
  </div>
  <el-table v-loading="loading" :data="result.items">
    <el-table-column prop="name" :label="$t('common.name')" />
    <el-table-column
      prop="departmentName"
      :label="$t('organization.department')"
    />
    <el-table-column prop="jobTitle" :label="$t('organization.jobTitle')" />
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
import departmentApi from '@/api/department-api';
import employeeApi, {
  type Employee,
  type GetEmployeesQuery,
} from '@/api/employee-api';
import jobApi from '@/api/job-api';
import { Guid } from '@/models/guid';
import type { OptionResult } from '@/models/option-result';
import { defaultQuery, defaultResult, type PageResult } from '@/models/page';

const loading = ref(false);
const departments: Ref<OptionResult<Guid>[]> = ref([]);
const jobs: Ref<OptionResult<Guid>[]> = ref([]);
const query: GetEmployeesQuery = reactive({
  departmentId: undefined as Guid | undefined,
  jobId: undefined as Guid | undefined,
  ...defaultQuery,
});
const result: PageResult<Employee> = reactive(defaultResult());

departmentApi.getOptions().then((x) => (departments.value = x.data));
jobApi.getOptions().then((x) => (jobs.value = x.data));

watch(
  query,
  (query) => {
    loading.value = true;
    employeeApi
      .getList(query)
      .then((x) => Object.assign(result, x.data))
      .finally(() => (loading.value = false));
  },
  { immediate: true },
);
</script>
