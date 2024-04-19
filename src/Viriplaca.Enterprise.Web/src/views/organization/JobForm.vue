<template>
  <el-form :model="form" label-width="200px" @submit.prevent="save">
    <el-form-item :label="$t('organization.jobTitle')">
      <el-input v-model="form.title" />
    </el-form-item>
    <el-form-item :label="$t('status.isEnabled.name')">
      <el-switch v-model="form.isEnabled" />
    </el-form-item>
    <el-form-item>
      <ButtonBack />
      <ButtonSave :loading="loading" />
    </el-form-item>
  </el-form>
</template>

<script setup lang="ts">
import departmentApi from '@/api/department-api';
import jobApi, { type Job } from '@/api/job-api';
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
  title: '',
  isEnabled: true,
});

departmentApi.getOptions().then((x) => (departments.value = x.data));

if (props.action === 'edit') {
  jobApi
    .get(props.id)
    .then((x) => Object.assign(form, x.data))
    .finally(() => (loading.value = false));
}

const save = () => {
  loading.value = true;
  const post = props.action === 'create' ? jobApi.create : jobApi.update;
  post(form as Job).then(() => {
    loading.value = false;
    success(props.action);
  });
};
</script>

<style scoped lang="scss">
.el-form {
  max-width: 1000px;
}
</style>
