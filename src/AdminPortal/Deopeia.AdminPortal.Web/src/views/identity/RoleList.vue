<template>
  <TableToolbar>
    <el-form :model="query" :inline="true">
      <el-form-item :label="$t('common.status')">
        <SelectBoolean v-model="query.isEnabled" locale-key="status.isEnabled" />
      </el-form-item>
    </el-form>

    <template #right>
      <ButtonCreate route="identity.role.create" />
    </template>
  </TableToolbar>

  <el-table v-loading="isLoading" :data="data?.items">
    <el-table-column prop="code" :label="$t('common.code')" />
    <el-table-column prop="name" :label="$t('common.name')" />
    <el-table-column prop="description" :label="$t('common.description')" show-overflow-tooltip />
    <TableColumnBoolean
      prop="isEnabled"
      :label="$t('common.status')"
      localeKey="status.isEnabled"
    />
    <el-table-column :label="$t('common.actions')">
      <template #default="{ row }">
        <TextLink :to="{ name: 'identity.role.edit', params: { code: row.code } }" />
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
import { roleApi, type GetRolesQuery } from '@/api/identity/role-api';

const query: GetRolesQuery = reactive({
  isEnabled: undefined,
  ...defaultQuery,
});
const { data, isFetching } = useQuery({
  queryKey: ['roleApi.getList', query],
  queryFn: () => roleApi.getList(query),
});
const { isLoading } = useDeferredLoading(isFetching);
</script>
