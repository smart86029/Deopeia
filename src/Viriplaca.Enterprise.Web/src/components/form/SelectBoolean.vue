<template>
  <el-select clearable>
    <el-option :label="$t(`${localeKey}.true`)" :value="true" />
    <el-option :label="$t(`${localeKey}.false`)" :value="false" />
  </el-select>
</template>

<script setup lang="ts">
defineProps<{
  localeKey: string;
}>();
</script>

<style scoped lang="scss">
.el-select {
  min-width: var(--el-form-inline-content-width);
}
</style>
