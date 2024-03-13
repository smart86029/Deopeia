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
import departmentApi from '@/api/department-api';
import type { Guid } from '@/models/guid';
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
  id: 0,
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
  max-width: 700px;
}
</style>
