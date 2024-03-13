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
    <el-form-item>
      <ButtonBack />
      <ButtonSave />
    </el-form-item>
  </el-form>
</template>

<script setup lang="ts">
import departmentApi from '@/api/department-api';
import employeeApi from '@/api/employee-api';
import { Guid } from '@/models/guid';
import type { OptionResult } from '@/models/option-result';
import { MaritalStatus } from '@/models/organization/marital-status';
import { Sex } from '@/models/organization/sex';
import { dayjs } from 'element-plus';

const props = defineProps<{
  action: 'create' | 'edit';
  id: Guid;
}>();
const loading = ref(true);
const { t } = useI18n();
const router = useRouter();
const departments = ref([] as OptionResult<Guid>[]);
const form = reactive({
  id: Guid.empty,
  firstName: '',
  lastName: undefined as string | undefined,
  birthDate: dayjs.Dayjs,
  sex: Sex.NotKnown,
  maritalStatus: MaritalStatus.Unknown,
  parentId: undefined as Guid | undefined,
});

departmentApi.getOptions().then((x) => (departments.value = x.data));

if (props.action === 'edit') {
  employeeApi
    .get(props.id)
    .then((x) => Object.assign(form, x.data))
    .finally(() => (loading.value = false));
}

const save = () => {
  // const post =
  //   props.action === 'create' ? operatorApi.create : operatorApi.update;
  // post(form as Operator).then(() => {
  //   ElNotification.success({
  //     message: t(`message.${props.action}Success`),
  //     position: 'bottom-left',
  //   });
  //   router.go(-1);
  // });
};
</script>

<style scoped lang="scss">
.el-form {
  max-width: 1000px;
}
</style>
