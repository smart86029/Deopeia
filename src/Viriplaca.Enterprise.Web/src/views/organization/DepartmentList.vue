<template>
  <div class="toolbar">
    <el-form :inline="true" :model="form">
      <el-form-item :label="$t('common.status')">
        <SelectBoolean v-model="form.isEnabled" localeKey="status.isEnabled" />
      </el-form-item>
    </el-form>
    <FlexDivider />
    <ButtonCreate route="leave.apply" :text="$t('operation.apply')" />
  </div>
  <el-table v-loading="loading" :data="departments">
    <el-table-column prop="name" :label="$t('common.name')" />
    <el-table-column :label="$t('common.status')">
      <template #default="{ row }">
        <TextBoolean :value="row.isEnabled" localeKey="status.isEnabled" />
      </template>
    </el-table-column>
    <el-table-column prop="headName" :label="$t('organization.head')" />
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
import departmentApi, { type Department } from '@/api/department-api';

const loading = ref(false);
const departments: Ref<Department[]> = ref([]);
const form = reactive({
  isEnabled: undefined as boolean | undefined,
});

watch(
  form,
  (form) => {
    loading.value = true;
    departmentApi
      .getList(form.isEnabled)
      .then((x) => {
        departments.value = x.data.items;
      })
      .finally(() => (loading.value = false));
  },
  { immediate: true },
);
</script>
