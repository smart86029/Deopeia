<template>
  <el-space :spacer="spacer">
    <slot></slot>
  </el-space>
</template>

<script setup lang="ts">
import { ElDivider } from 'element-plus';

const spacer = h(ElDivider, { direction: 'vertical' });
</script>

<style scoped lang="scss">
:deep(.el-divider--vertical) {
  margin: 0;
}
</style>
