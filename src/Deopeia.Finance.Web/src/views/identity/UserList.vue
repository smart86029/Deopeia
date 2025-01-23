<template>
  <TableToolbar>
    <el-form :model="query" :inline="true">
      <el-form-item :label="$t('identity.userName')">
        <el-input v-model="query.userName" />
      </el-form-item>
      <el-form-item :label="$t('common.status')">
        <SelectBoolean
          v-model="query.isEnabled"
          locale-key="status.isEnabled"
        />
      </el-form-item>
      <el-form-item :label="$t('identity.role')">
        <SelectOption v-model="query.roleCode" :options="roles" />
      </el-form-item>
    </el-form>

    <template #right>
      <ButtonCreate route="user.create" />
    </template>
  </TableToolbar>

  <el-table v-loading="loading" :data="result.items">
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
import { roleApi } from '@/api/identity/role-api';
import {
  userApi,
  type GetUsersQuery,
  type UserRow,
} from '@/api/identity/user-api';
import type { OptionResult } from '@/models/option-result';
import {
  defaultQuery,
  defaultResult,
  reassign,
  type PageResult,
} from '@/models/page';

const loading = ref(false);
const roles: Ref<OptionResult<string>[]> = ref([]);
const query: GetUsersQuery = reactive({
  ...defaultQuery,
});
const result: PageResult<UserRow> = reactive(defaultResult());

roleApi.getOptions().then((x) => (roles.value = x.data));

watchDebounced(
  query,
  (query) => {
    if (!loading.value) {
      loading.value = true;
      userApi
        .getList(query)
        .then((x) => reassign(query, result, x.data))
        .finally(() => (loading.value = false));
    }
  },
  { debounce: 500, immediate: true },
);
</script>
