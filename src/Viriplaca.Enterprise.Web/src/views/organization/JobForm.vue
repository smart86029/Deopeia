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
      <ButtonSave />
    </el-form-item>
  </el-form>
</template>

<script setup lang="ts">
import departmentApi from '@/api/department-api';
import jobApi from '@/api/job-api';
import { Guid } from '@/models/guid';
import type { OptionResult } from '@/models/option-result';

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
