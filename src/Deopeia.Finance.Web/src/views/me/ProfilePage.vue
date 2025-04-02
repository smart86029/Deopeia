<template>
  <h2>{{ $t('common.profile') }}</h2>

  <UploadImage v-model="avatar" :image-url="imageUrl" />

  <p><strong>Name:</strong> {{ userName }}</p>
  <p><strong>Email:</strong> {{ userEmail }}</p>
</template>

<script setup lang="ts">
import { meApi } from '@/api/me/me-api';

const userName = 'John Doe';
const userEmail = 'john.doe@example.com';
const avatar: Ref<File | undefined> = ref(undefined);
const imageUrl = ref('');

watch(avatar, (avatar) => {
  if (!avatar) {
    return;
  }

  meApi
    .uploadAvatar(avatar)
    .then((response) => {
      console.log('Avatar uploaded successfully:', response);
      imageUrl.value = response.data; // Assuming the API returns the image URL
    })
    .catch((error) => {
      console.error('Error uploading avatar:', error);
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
