<template>
  <div class="toolbar">
    <el-form :inline="true" :model="form">
      <el-form-item :label="$t('organization.department')">
        <SelectOption :options="departments" v-model="form.departmentId" />
      </el-form-item>
      <el-form-item :label="$t('organization.jobTitle')">
        <SelectOption :options="jobs" v-model="form.jobId" />
      </el-form-item>
    </el-form>
    <FlexDivider />
    <ButtonCreate route="leave.apply" />
  </div>
  <el-table v-loading="loading" :data="employees">
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
</template>

<script setup lang="ts">
import departmentApi from '@/api/department-api';
import employeeApi, { type Employee } from '@/api/employee-api';
import jobApi from '@/api/job-api';
import { Guid } from '@/models/guid';
import type { OptionResult } from '@/models/option-result';

const loading = ref(false);
const departments: Ref<OptionResult<Guid>[]> = ref([]);
const jobs: Ref<OptionResult<Guid>[]> = ref([]);
const employees: Ref<Employee[]> = ref([]);
const form = reactive({
  departmentId: undefined as Guid | undefined,
  jobId: undefined as Guid | undefined,
});

departmentApi.getOptions().then((x) => (departments.value = x.data));
jobApi.getOptions().then((x) => (jobs.value = x.data));

watch(
  form,
  ({ departmentId, jobId }) => {
    loading.value = true;
    employeeApi
      .getList(departmentId, jobId)
      .then((x) => {
        employees.value = x.data.items;
      })
      .finally(() => (loading.value = false));
  },
  { immediate: true },
);
</script>
