<template>
  <div class="toolbar">
    <el-form :inline="true" :model="query">
      <el-form-item :label="$t('common.status')">
        <SelectBoolean v-model="query.isEnabled" localeKey="status.isEnabled" />
      </el-form-item>
    </el-form>
    <FlexDivider />
    <ButtonCreate route="department.create" :text="$t('operation.create')" />
  </div>
  <el-table v-loading="loading" :data="result.items">
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
        <TextLink :to="{ name: 'department.edit', params: { id: row.id } }" />
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
import departmentApi, {
  type DepartmentRow,
  type GetDepartmentQuery,
} from '@/api/department-api';
import { defaultQuery, defaultResult, type PageResult } from '@/models/page';

const loading = ref(false);
const query: GetDepartmentQuery = reactive({
  isEnabled: undefined as boolean | undefined,
  ...defaultQuery,
});
const result: PageResult<DepartmentRow> = reactive(defaultResult());

watch(
  query,
  (query) => {
    loading.value = true;
    departmentApi
      .getList(query)
      .then((x) => Object.assign(result, x.data))
      .finally(() => (loading.value = false));
  },
  { immediate: true },
);
</script>
