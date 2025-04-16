<template>
  <el-text>{{ value === undefined ? '-' : $n(value, 'number') }}</el-text>
</template>

<script setup lang="ts">
defineProps<{
  value: number | undefined;
}>();
</script>
