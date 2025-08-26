<template>
  <TableToolbar>
    <el-form :model="query" :inline="true">
      <el-form-item :label="$t('identity.userName')">
        <el-input v-model="query.userName" />
      </el-form-item>
      <el-form-item :label="$t('common.status')">
        <SelectBoolean v-model="query.isEnabled" locale-key="status.isEnabled" />
      </el-form-item>
      <el-form-item :label="$t('identity.role')">
        <SelectOption v-model="query.roleCode" :options="roles" />
      </el-form-item>
    </el-form>

    <template #right>
      <ButtonCreate route="identity.user.create" />
    </template>
  </TableToolbar>

  <el-table v-loading="isLoading" :data="data?.items">
    <el-table-column prop="userName" :label="$t('identity.userName')" />
    <TableColumnBoolean
      prop="isEnabled"
      :label="$t('common.status')"
      localeKey="status.isEnabled"
    />
    <el-table-column :label="$t('identity.role')">
      <template #default="{ row }">
        <TagList v-model="row.roleCodes" :options="roles" />
      </template>
    </el-table-column>
    <el-table-column :label="$t('common.operations')">
      <template #default="{ row }">
        <TextLink :to="{ name: 'identity.user.edit', params: { id: row.id } }" />
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
import { roleApi } from '@/api/identity/role-api';
import { userApi, type GetUsersQuery } from '@/api/identity/user-api';

const { data: roles } = useQuery({
  queryKey: ['roleApi.getList'],
  queryFn: () => roleApi.getOptions(),
  initialData: [],
});

const query: GetUsersQuery = reactive({
  ...defaultQuery,
});
const { data, isFetching } = useQuery({
  queryKey: ['userApi.getList', query],
  queryFn: () => userApi.getList(query),
});
const { isLoading } = useDeferredLoading(isFetching);
</script>
