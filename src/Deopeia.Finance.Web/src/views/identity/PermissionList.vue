<template>
  <TableToolbar>
    <template #right>
      <ButtonCreate route="permission.create" />
    </template>
  </TableToolbar>

  <el-table v-loading="loading" :data="result.items">
    <el-table-column prop="name" :label="$t('common.name')" />
    <el-table-column prop="isEnabled" :label="$t('status.isEnabled.name')">
      <template #default="{ row }">
        <TextBoolean :value="row.isEnabled" localeKey="status.isEnabled" />
      </template>
    </el-table-column>
    <el-table-column prop="permissionName" :label="$t('common.name')" />
    <el-table-column :label="$t('common.operations')">
      <template #default="{ row }">
        <TextLink :to="{ name: 'permission.edit', params: { id: row.id } }" />
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
import permissionApi, {
  type GetPermissionsQuery,
  type PermissionRow,
} from '@/api/identity/permission-api';
import {
  defaultQuery,
  defaultResult,
  reassign,
  type PageResult,
} from '@/models/page';

const loading = ref(false);
const query: GetPermissionsQuery = reactive({
  ...defaultQuery,
});
const result: PageResult<PermissionRow> = reactive(defaultResult());

watch(
  query,
  (query) => {
    if (!loading.value) {
      loading.value = true;
      permissionApi
        .getList(query)
        .then((x) => reassign(query, result, x.data))
        .finally(() => (loading.value = false));
    }
  },
  { immediate: true },
);
</script>
