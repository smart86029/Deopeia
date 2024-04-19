<template>
  <el-form :model="form" label-width="200px" @submit.prevent="save">
    <el-form-item :label="$t('organization.firstName')">
      <el-input v-model="form.firstName" />
    </el-form-item>
    <el-form-item :label="$t('organization.lastName')">
      <el-input v-model="form.lastName" />
    </el-form-item>
    <el-form-item :label="$t('organization.birthDate')">
      <el-date-picker type="date" v-model="form.birthDate" />
    </el-form-item>
    <el-form-item :label="$t('organization.sex.name')">
      <RadioEnum :enum="Sex" v-model="form.sex" localeKey="organization.sex" />
    </el-form-item>
    <el-form-item :label="$t('organization.maritalStatus.name')">
      <RadioEnum
        :enum="MaritalStatus"
        v-model="form.maritalStatus"
        localeKey="organization.maritalStatus"
      />
    </el-form-item>
    <el-form-item :label="$t('organization.avatar')">
      <UploadImage
        v-model:imageId="form.avatarId"
        v-model:imageUrl="avatarUrl"
      />
    </el-form-item>

    <el-form-item :label="$t('organization.department')">
      <SelectOption :options="departments" v-model="form.departmentId" />
    </el-form-item>
    <el-form-item :label="$t('organization.jobTitle')">
      <SelectOption :options="jobs" v-model="form.jobId" />
    </el-form-item>
    <el-form-item :label="$t('organization.hiredOn')">
      <el-date-picker type="date" v-model="form.hiredOn" />
    </el-form-item>

    <el-form-item>
      <ButtonBack />
      <ButtonSave :loading="loading" />
    </el-form-item>
  </el-form>
</template>

<script setup lang="ts">
import departmentApi from '@/api/department-api';
import employeeApi, { type Employee } from '@/api/employee-api';
import jobApi from '@/api/job-api';
import { Guid } from '@/models/guid';
import type { OptionResult } from '@/models/option-result';
import { MaritalStatus } from '@/models/organization/marital-status';
import { Sex } from '@/models/organization/sex';
import { success } from '@/plugins/element';

const props = defineProps<{
  action: 'create' | 'edit';
  id: Guid;
}>();
const loading = ref(true);
const departments: Ref<OptionResult<Guid>[]> = ref([]);
const jobs: Ref<OptionResult<Guid>[]> = ref([]);
const form = reactive({
  id: Guid.empty,
  firstName: '',
  lastName: undefined as string | undefined,
  birthDate: new Date(),
  sex: Sex.NotKnown,
  maritalStatus: MaritalStatus.Unknown,
  avatarId: Guid.empty,
  departmentId: Guid.empty,
  jobId: Guid.empty,
  hiredOn: undefined as Date | undefined,
});
const avatarUrl = ref('');

departmentApi.getOptions().then((x) => (departments.value = x.data));
jobApi.getOptions().then((x) => (jobs.value = x.data));

if (props.action === 'edit') {
  employeeApi
    .get(props.id)
    .then((x) => {
      Object.assign(form, x.data);
      avatarUrl.value = x.data.avatarUrl || '';
    })
    .finally(() => (loading.value = false));
}

const save = () => {
  loading.value = true;
  const post =
    props.action === 'create' ? employeeApi.create : employeeApi.update;
  post(form as Employee).then(() => {
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
