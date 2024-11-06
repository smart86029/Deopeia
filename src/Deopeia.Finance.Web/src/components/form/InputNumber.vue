<template>
  <el-input v-model="model" :formatter="formatter" :parser="parser" />
</template>

<script setup lang="ts">
const model = defineModel<number>();

const formatter = (value: string | number): string =>
  value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');

const parser = (value: string): string => value.replace(/(,*)/g, '');
</script>
