<template>
  <TableToolbar>
    <template #right>
      <ButtonCreate route="user.create" />
    </template>
  </TableToolbar>

  <el-table v-loading="loading" :data="result.items">
    <el-table-column prop="userName" :label="$t('identity.userName')" />
    <el-table-column prop="isEnabled" :label="$t('status.isEnabled.name')">
      <template #default="{ row }">
        <TextBoolean :value="row.isEnabled" localeKey="status.isEnabled" />
      </template>
    </el-table-column>
    <el-table-column prop="userName" :label="$t('common.name')" />
    <el-table-column :label="$t('common.operations')">
      <template #default="{ row }">
        <TextLink :to="{ name: 'user.edit', params: { id: row.id } }" />
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
import userApi, {
  type GetUsersQuery,
  type UserRow,
} from '@/api/identity/user-api';
import { defaultQuery, defaultResult, type PageResult } from '@/models/page';

const loading = ref(false);
const query: GetUsersQuery = reactive({
  isEnabled: undefined as boolean | undefined,
  ...defaultQuery,
});
const result: PageResult<UserRow> = reactive(defaultResult());

watch(
  query,
  (query) => {
    loading.value = true;
    userApi
      .getList(query)
      .then((x) => Object.assign(result, x.data))
      .finally(() => (loading.value = false));
  },
  { immediate: true },
);
</script>
