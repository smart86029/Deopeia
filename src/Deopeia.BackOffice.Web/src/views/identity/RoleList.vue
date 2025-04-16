<template>
  <TableToolbar>
    <el-form :model="query" :inline="true">
      <el-form-item :label="$t('common.status')">
        <SelectBoolean
          v-model="query.isEnabled"
          locale-key="status.isEnabled"
        />
      </el-form-item>
    </el-form>

    <template #right>
      <ButtonCreate route="role.create" />
    </template>
  </TableToolbar>

  <el-table v-loading="loading" :data="result.items">
    <el-table-column prop="code" :label="$t('common.code')" />
    <el-table-column prop="name" :label="$t('common.name')" />
    <el-table-column
      prop="description"
      :label="$t('common.description')"
      show-overflow-tooltip
    />
    <TableColumnBoolean
      prop="isEnabled"
      :label="$t('common.status')"
      localeKey="status.isEnabled"
    />
    <el-table-column :label="$t('common.operations')">
      <template #default="{ row }">
        <TextLink :to="{ name: 'role.edit', params: { code: row.code } }" />
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
import {
  roleApi,
  type GetRolesQuery,
  type RoleRow,
} from '@/api/identity/role-api';
import {
  defaultQuery,
  defaultResult,
  reassign,
  type PageResult,
} from '@/models/page';

const loading = ref(false);
const query: GetRolesQuery = reactive({
  isEnabled: undefined,
  ...defaultQuery,
});
const result: PageResult<RoleRow> = reactive(defaultResult());

watch(
  query,
  (query) => {
    if (!loading.value) {
      loading.value = true;
      roleApi
        .getList(query)
        .then((x) => reassign(query, result, x.data))
        .finally(() => (loading.value = false));
    }
  },
  { immediate: true },
);
</script>
