<template>
  <el-radio-group v-model="model">
    <el-radio
      v-for="value in values"
      :key="value"
      :label="$t(`${localeKey}.${value}`)"
      :value="value"
    />
  </el-radio-group>
</template>

<script setup lang="ts" generic="TEnum extends object">
const model = defineModel<TEnum>();

const props = defineProps<{
  enum: TEnum;
  localeKey: string;
}>();

const keys = Object.keys(props.enum);
const values = keys.slice(0, keys.length / 2).map((x) => +x);
</script>
