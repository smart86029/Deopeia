<template>
  <el-form :model="form" label-width="200" @submit.prevent="save">
    <el-form-item :label="$t('identity.userName')">
      <el-input v-model="form.userName" />
    </el-form-item>
    <el-form-item :label="$t('identity.password')">
      <el-input v-model="form.password" type="password" />
    </el-form-item>
    <el-form-item :label="$t('status.isEnabled.name')">
      <el-switch v-model="form.isEnabled" />
    </el-form-item>
    <el-form-item :label="$t('identity.role')">
      <el-checkbox-group v-model="form.roleIds">
        <el-checkbox
          v-for="role in roles"
          :key="role.value"
          :label="role.name"
          :value="role.value"
          :disabled="!role.isEnabled"
        />
      </el-checkbox-group>
    </el-form-item>
    <el-form-item>
      <ButtonBack />
      <ButtonSave :loading="loading" />
    </el-form-item>
  </el-form>
</template>

<script setup lang="ts">
import roleApi from '@/api/identity/role-api';
import userApi, { type User } from '@/api/identity/user-api';
import { Guid } from '@/models/guid';
import type { OptionResult } from '@/models/option-result';
import { success } from '@/plugins/element';

const props = defineProps<{
  action: 'create' | 'edit';
  id: Guid;
}>();
const loading = ref(false);
const roles = ref([] as OptionResult<Guid>[]);
const form = reactive({
  id: Guid.empty,
  userName: '',
  password: '',
  isEnabled: true,
} as User);

roleApi.getOptions().then((x) => (roles.value = x.data));

if (props.action === 'edit') {
  userApi
    .get(props.id)
    .then((x) => {
      Object.assign(form, x.data);
    })
    .finally(() => (loading.value = false));
}

const save = () => {
  loading.value = true;
  const post = props.action === 'create' ? userApi.create : userApi.update;
  post(form)
    .then(() => success(props.action))
    .finally(() => (loading.value = false));
};
</script>

<style scoped lang="scss">
.el-form {
  max-width: 1000px;
}
</style>
