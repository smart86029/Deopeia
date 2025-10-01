<template>
  <TableToolbar>
    <el-form :model="request" :inline="true">
      <el-form-item :label="$t('identity.code')">
        <el-input v-model="request.code" />
      </el-form-item>
      <el-form-item :label="$t('common.status')">
        <SelectBoolean v-model="request.isEnabled" locale-key="status.isEnabled" />
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
        <DividerSpace>
          <ButtonLink :to="{ name: 'identity.permission.edit', params: { code: row.code } }" />
          <el-button type="danger" link @click="deletePermission(row.code)">
            {{ $t('action.delete') }}
          </el-button>
        </DividerSpace>
      </template>
    </el-table-column>
  </el-table>

  <TablePagination
    v-model:current-page="request.pageIndex"
    v-model:page-size="request.pageSize"
    :total="data?.totalCount"
  />
</template>

<script setup lang="ts">
import { permissionApi, type GetPermissionsRequest } from '@/api/identity/permission-api';

const request: GetPermissionsRequest = reactive({
  code: undefined,
  isEnabled: undefined,
  ...defaultQuery,
});
const queryClient = useQueryClient();
const { data, isFetching } = useQuery({
  queryKey: ['permissionApi.getList', request],
  queryFn: () => permissionApi.getList(request),
});
const { isLoading } = useDeferredLoading(isFetching);
const { confirmDelete } = useConfirm();
const { success } = useNotify();

const deletePermission = async (code: string) => {
  await confirmDelete('identity.permission', code);
  await permissionApi.delete(code);
  queryClient.invalidateQueries({ queryKey: ['permissionApi.getList', request] });
  success('delete');
};
</script>
