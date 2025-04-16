<template>
  <h2>{{ $t('auth.changePassword') }}</h2>
  <el-form label-position="top" @submit.prevent="save">
    <el-form-item :label="$t('auth.currentPassword')">
      <el-input v-model="form.currentPassword" type="password" show-password />
    </el-form-item>
    <el-form-item :label="$t('auth.newPassword')">
      <el-input v-model="form.newPassword" type="password" show-password />
    </el-form-item>
    <el-form-item>
      <ButtonSave class="button-save" />
    </el-form-item>
  </el-form>
</template>

<script setup lang="ts">
import { meApi, type ChangePasswordCommand } from '@/api/me/me-api';

const form: ChangePasswordCommand = reactive({
  currentPassword: '',
  newPassword: '',
});

const save = () =>
  meApi.changePassword(form).then(() => {
    form.currentPassword = '';
    form.newPassword = '';
  });
</script>

<style lang="scss" scoped>
.el-form {
  width: 400px;
}

.button-save {
  width: 100%;
}
</style>
