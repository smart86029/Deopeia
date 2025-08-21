<template>
  <el-text v-if="value" type="success">
    {{ $t(`${localeKey}.true`) }}
  </el-text>
  <el-text v-else type="danger">
    {{ $t(`${localeKey}.false`) }}
  </el-text>
</template>

<script setup lang="ts">
defineProps<{
  value: boolean;
  localeKey: string;
}>();
</script>
