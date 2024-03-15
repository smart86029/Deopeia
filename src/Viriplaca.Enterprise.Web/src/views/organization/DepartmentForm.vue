<template>
  <el-form :model="form" label-width="200px" @submit.prevent="save">
    <el-form-item :label="$t('common.name')">
      <el-input v-model="form.name" />
    </el-form-item>
    <el-form-item :label="$t('status.isEnabled.name')">
      <el-switch v-model="form.isEnabled" />
    </el-form-item>
    <el-form-item :label="$t('organization.department')">
      <SelectOption :options="departments" v-model="form.parentId" />
    </el-form-item>
    <el-form-item>
      <ButtonBack />
      <ButtonSave />
    </el-form-item>
  </el-form>
</template>

<script setup lang="ts">
import departmentApi, { type Department } from '@/api/department-api';
import { Guid } from '@/models/guid';
import type { OptionResult } from '@/models/option-result';
import { success } from '@/plugins/element';

const props = defineProps<{
  action: 'create' | 'edit';
  id: Guid;
}>();
const loading = ref(true);
const departments = ref([] as OptionResult<Guid>[]);
const form = reactive({
  id: Guid.empty,
  name: '',
  isEnabled: true,
  parentId: undefined as Guid | undefined,
});

departmentApi.getOptions().then((x) => (departments.value = x.data));

if (props.action === 'edit') {
  departmentApi
    .get(props.id)
    .then((x) => Object.assign(form, x.data))
    .finally(() => (loading.value = false));
}

const save = () => {
  const post =
    props.action === 'create' ? departmentApi.create : departmentApi.update;
  post(form as Department).then(() => success(props.action));
};
</script>

<style scoped lang="scss">
.el-form {
  max-width: 1000px;
}
</style>
