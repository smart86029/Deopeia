<template>
  <el-button type="primary" native-type="submit">
    {{ $t(text) }}
  </el-button>
</template>

<script setup lang="ts">
withDefaults(
  defineProps<{
    text?: string;
  }>(),
  { text: 'action.save' },
);
</script>
