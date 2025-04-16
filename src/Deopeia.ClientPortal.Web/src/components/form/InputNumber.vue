<template>
  <el-input v-model="model" :formatter="formatter" :parser="parser" />
</template>

<script setup lang="ts">
const model = defineModel<number>();

const formatter = (value: string | number): string =>
  value
    .toString()
    .replace(/^(0+)(?=\d)/g, '')
    .replace(
      /(\d)(?=(?:\d{3})+(?:\.|$))|(\.\d*)$/g,
      (_, integer, fractional) => fractional || integer + ',',
    );

const parser = (value: string): string => value.replace(/(,*)/g, '');
</script>
