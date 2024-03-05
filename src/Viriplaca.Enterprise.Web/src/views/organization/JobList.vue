<template>
  <div class="toolbar">
    <el-form :inline="true" :model="form">
      <el-form-item :label="$t('common.status')">
        <SelectBoolean v-model="form.isEnabled" localeKey="status.isEnabled" />
      </el-form-item>
    </el-form>
    <FlexDivider />
    <ButtonCreate route="leave.apply" />
  </div>
  <el-table v-loading="loading" :data="jobs">
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
</template>

<script setup lang="ts">
import jobApi, { type Job } from '@/api/job-api';

const loading = ref(false);
const jobs: Ref<Job[]> = ref([]);
const form = reactive({
  isEnabled: undefined as boolean | undefined,
});

watch(
  form,
  (form) => {
    loading.value = true;
    jobApi
      .getList(form.isEnabled)
      .then((x) => {
        jobs.value = x.data.items;
      })
      .finally(() => (loading.value = false));
  },
  { immediate: true },
);
</script>
