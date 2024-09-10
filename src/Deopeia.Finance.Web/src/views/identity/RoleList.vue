<template>
  <TableToolbar>
    <template #right>
      <ButtonCreate route="role.create" />
    </template>
  </TableToolbar>

  <el-table v-loading="loading" :data="result.items">
    <el-table-column prop="name" :label="$t('common.name')" />
    <el-table-column prop="isEnabled" :label="$t('status.isEnabled.name')">
      <template #default="{ row }">
        <TextBoolean :value="row.isEnabled" localeKey="status.isEnabled" />
      </template>
    </el-table-column>
    <el-table-column :label="$t('common.operations')">
      <template #default="{ row }">
        <TextLink :to="{ name: 'role.edit', params: { id: row.id } }" />
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
import roleApi, {
  type GetRolesQuery,
  type RoleRow,
} from '@/api/identity/role-api';
import { defaultQuery, defaultResult, type PageResult } from '@/models/page';

const loading = ref(false);
const query: GetRolesQuery = reactive({
  isEnabled: undefined as boolean | undefined,
  ...defaultQuery,
});
const result: PageResult<RoleRow> = reactive(defaultResult());

watch(
  query,
  (query) => {
    loading.value = true;
    roleApi
      .getList(query)
      .then((x) => Object.assign(result, x.data))
      .finally(() => (loading.value = false));
  },
  { immediate: true },
);
</script>
