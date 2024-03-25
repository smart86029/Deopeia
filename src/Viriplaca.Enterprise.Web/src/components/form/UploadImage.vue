<template>
  <el-upload ref="upload" :show-file-list="false" :http-request="post">
    <el-image v-if="imageUrl" :src="imageUrl" class="image" />
    <div v-else class="icon">
      <IconAdd />
    </div>
  </el-upload>
</template>

<script setup lang="ts">
import imageApi, { type UploadImageCommand } from '@/api/image-api';
import type { Guid } from '@/models/guid';
import type { UploadInstance } from 'element-plus';

const props = defineProps<{
  imageId: Guid;
  imageUrl: string;
}>();

const emits = defineEmits<{
  'update:imageId': [imageId: Guid];
  'update:imageUrl': [imageUrl: string];
}>();

const upload = ref<UploadInstance>();
const imageUrl = computed({
  get: () => props.imageUrl,
  set: (value) => emits('update:imageUrl', value),
});

const post = (option: any) => {
  const command: UploadImageCommand = {
    file: option.file,
  };

  imageApi.upload(command).then((x) => {
    emits('update:imageId', x.data.id);
    imageUrl.value = x.data.url;
    upload.value?.clearFiles();
  });
};
</script>

<style scoped lang="scss">
$width: 178px;
$height: 178px;

.image {
  width: $width;
  height: $height;
}

:deep(.el-upload) {
  border: 1px dashed var(--el-border-color);
  border-radius: 6px;
  transition: var(--el-transition-duration-fast);
}

:deep(.el-upload:hover) {
  border-color: var(--el-color-primary);
}

.icon {
  color: var(--el-text-color-regular);
  width: $width;
  height: $height;
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>
