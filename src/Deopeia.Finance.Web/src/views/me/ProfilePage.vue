<template>
  <h2>{{ $t('route.me.profile') }}</h2>

  <UploadImage v-model="avatar" :image-url="imageUrl" />

  <p><strong>Name:</strong> {{ userName }}</p>
  <p><strong>Email:</strong> {{ userEmail }}</p>
</template>

<script setup lang="ts">
import { meApi } from '@/api/me/me-api';

const userName = 'John Doe';
const userEmail = 'john.doe@example.com';
const avatar: Ref<File | undefined> = ref(undefined);
const imageUrl = ref('/api/Me/Avatar');

watch(avatar, (avatar) => {
  if (!avatar) {
    return;
  }
  meApi.uploadAvatar(avatar).then((response) => {
    imageUrl.value = response.data;
  });
});
</script>

<style lang="scss" scoped>
.avatar-container {
  position: relative;
  display: inline-block;
  cursor: pointer;
}

.user-avatar {
  transition: opacity 0.3s;
}

.avatar-container:hover .user-avatar {
  opacity: 0.7;
}

.upload-icon {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  font-size: 24px;
  color: #fff;
  pointer-events: none;
}
</style>
