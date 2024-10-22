<template>
  <el-select v-model="model" clearable>
    <el-option
      v-for="value in values"
      :key="value"
      :label="$t(`${localeKey}.${value}`)"
      :value="value"
    />
  </el-select>
</template>

<script setup lang="ts" generic="TEnum extends object">
const model = defineModel<number>();

const props = defineProps<{
  enum: TEnum;
  localeKey: string;
}>();

const keys = Object.keys(props.enum);
const values = keys.slice(0, keys.length / 2).map((x) => +x);
</script>

<style scoped lang="scss">
.el-select {
  min-width: var(--el-form-inline-content-width);
}
</style>
