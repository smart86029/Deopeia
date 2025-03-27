<template>
  <h2>{{ $t('route.me.twoFactorAuthentication') }}</h2>
  <el-form label-position="top" @submit.prevent="enable">
    <el-form-item class="item-image">
      <el-image :src="authenticator.imageUrl" />
    </el-form-item>
    <el-form-item>
      <InputCopyable v-model="authenticator.manualEntryKey" />
    </el-form-item>
    <el-form-item :label="$t('auth.verificationCode')">
      <VerificationCode v-model="verificationCode" />
    </el-form-item>
    <el-form-item>
      <ButtonSave class="button-save" text="operation.enable" />
    </el-form-item>
  </el-form>
</template>

<script setup lang="ts">
import { meApi, type Authenticator } from '@/api/me/me-api';

const loading = ref(false);
const authenticator: Authenticator = reactive({
  isEnabled: false,
  imageUrl: '',
  manualEntryKey: '',
});
const verificationCode = ref('');

meApi
  .getAuthenticator()
  .then((x) => Object.assign(authenticator, x.data))
  .finally(() => (loading.value = false));

const enable = () =>
  meApi.enableAuthenticator(verificationCode.value).catch(() => {
    verificationCode.value = '';
  });
</script>

<style lang="scss" scoped>
.el-form {
  width: 400px;
}

.item-image {
  justify-items: center;
}

.el-image {
  border-radius: var(--el-border-radius-base);
}

.button-save {
  width: 100%;
}
</style>
