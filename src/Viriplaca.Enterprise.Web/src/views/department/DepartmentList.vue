<template>
  <div class="toolbar">
    <el-form :inline="true" :model="form">
      <el-form-item :label="$t('common.status')">
        <el-select v-model="form.isEnabled" clearable>
          <el-option :label="$t('status.isEnabled.true')" :value="true" />
          <el-option :label="$t('status.isEnabled.false')" :value="false" />
        </el-select>
      </el-form-item>
    </el-form>
    <FlexDivider />
    <ButtonCreate route="leave.apply" :text="$t('operation.apply')" />
  </div>
  <el-table v-loading="loading" :data="departments">
    <el-table-column prop="name" :label="$t('common.name')" />
    <el-table-column prop="isEnabled" :label="$t('common.status')" />
    <el-table-column prop="head" :label="$t('department.head')" />
    <el-table-column
      prop="employeeCount"
      :label="$t('department.employeeCount')"
    />
    <el-table-column :label="$t('common.operations')">
      <template #default="{ row }">
        <TextLink
          :to="{ name: 'operator.edit', params: { id: row.id } }"
          :text="$t('operation.edit')"
        />
      </template>
    </el-table-column>
  </el-table>
</template>

<script setup lang="ts">
import departmentApi, { type Department } from '@/api/department-api';

const loading = ref(false);
const departments: Ref<Department[]> = ref([]);
const form = reactive({
  isEnabled: undefined as boolean | undefined,
});

watch(form, (form) => {
  loading.value = true;
  departmentApi
    .getList(form.isEnabled)
    .then((x) => {
      departments.value = x.data.items;
    })
    .finally(() => (loading.value = false));
});
</script>

<style scoped lang="scss">
.el-select {
  min-width: var(--el-input-width);
}
</style>
