<template>
  <TableToolbar>
    <el-form :model="query" :inline="true">
      <el-form-item :label="$t('identity.code')">
        <el-input v-model="query.code" />
      </el-form-item>
      <el-form-item :label="$t('common.status')">
        <SelectBoolean v-model="query.isEnabled" locale-key="status.isEnabled" />
      </el-form-item>
    </el-form>

    <template #right>
      <ButtonCreate route="identity.permission.create" />
    </template>
  </TableToolbar>

  <el-table v-loading="isLoading" :data="data?.items">
    <el-table-column prop="code" :label="$t('identity.code')" />
    <el-table-column prop="name" :label="$t('common.name')" />
    <el-table-column prop="description" :label="$t('common.description')" show-overflow-tooltip />
    <TableColumnBoolean
      prop="isEnabled"
      :label="$t('common.status')"
      localeKey="status.isEnabled"
    />
    <el-table-column :label="$t('common.actions')">
      <template #default="{ row }">
        <TextLink :to="{ name: 'identity.permission.edit', params: { code: row.code } }" />
      </template>
    </el-table-column>
  </el-table>

  <TablePagination
    v-model:current-page="query.pageIndex"
    v-model:page-size="query.pageSize"
    :total="data?.totalCount"
  />
</template>

<script setup lang="ts">
import { permissionApi, type GetPermissionsQuery } from '@/api/identity/permission-api';

const query: GetPermissionsQuery = reactive({
  code: undefined,
  isEnabled: undefined,
  ...defaultQuery,
});
const { data, isFetching } = useQuery({
  queryKey: ['permissionApi.getList', query],
  queryFn: () => permissionApi.getList(query),
});
const { isLoading } = useDeferredLoading(isFetching);
</script>
