<template>
  <el-upload
    ref="upload"
    :show-file-list="false"
    :auto-upload="false"
    :limit="1"
    :on-change="handleChange"
    accept="image/*"
  >
    <el-image v-if="imageUrl" :src="imageUrl" class="image" />
    <div v-else class="icon">
      <IconAdd />
    </div>
  </el-upload>
</template>

<script setup lang="ts">
import type { UploadFile, UploadInstance } from 'element-plus';

const model = defineModel<File>();

defineProps<{
  imageUrl: string;
}>();

const upload = ref<UploadInstance>();

const handleChange = (uploadFile: UploadFile) => (model.value = uploadFile.raw);
</script>

<style lang="scss" scoped>
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
