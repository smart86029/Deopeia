<template>
  <h2>{{ $t('auth.changePassword') }}</h2>
  <el-form label-position="top" @submit.prevent="mutate">
    <el-form-item :label="$t('auth.currentPassword')">
      <el-input v-model="form.currentPassword" type="password" show-password />
    </el-form-item>
    <el-form-item :label="$t('auth.newPassword')">
      <el-input v-model="form.newPassword" type="password" show-password />
    </el-form-item>
    <el-form-item>
      <ButtonSave class="button-save" :loading="isPending" />
    </el-form-item>
  </el-form>
</template>

<script setup lang="ts">
import { meApi, type ChangePasswordCommand } from '@/api/me/me-api';

const form: ChangePasswordCommand = reactive({
  currentPassword: '',
  newPassword: '',
});

const { isPending, mutate } = useMutation({
  mutationFn: () => meApi.changePassword(form),
  onSuccess: () => {
    form.currentPassword = '';
    form.newPassword = '';
  },
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
