<template>
  <div>
    <h2>{{ $t('route.me.twoFactorAuthentication') }}</h2>
    <el-form label-position="top">
      <el-form-item class="item-image">
        <el-image :src="authenticator.imageUrl" />
      </el-form-item>
      <el-form-item>
        <InputCopyable v-model="authenticator.manualEntryKey" />
      </el-form-item>
      <el-form-item :label="$t('auth.authenticationCode')">
        <AuthenticationCode v-model="authenticationCode" autofocus />
      </el-form-item>
      <el-form-item>
        <ButtonSave class="button-save" text="operation.link" />
      </el-form-item>
    </el-form>
  </div>
</template>

<script setup lang="ts">
import {
  authenticatorApi,
  type Authenticator,
} from '@/api/user/authenticator-api';

const loading = ref(false);
const authenticator: Authenticator = reactive({
  isBound: false,
  imageUrl: '',
  manualEntryKey: '',
});
const authenticationCode = ref('');

authenticatorApi
  .get()
  .then((x) => Object.assign(authenticator, x.data))
  .finally(() => (loading.value = false));
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
