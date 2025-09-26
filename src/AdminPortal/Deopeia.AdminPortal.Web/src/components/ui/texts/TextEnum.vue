<template>
  <el-text :type="type">
    {{ $t(`${localeKey}.${value}`) }}
  </el-text>
</template>

<script setup lang="ts" generic="TValue extends string | number">
const props = defineProps<{
  value: TValue;
  localeKey: string;
  success?: TValue | TValue[];
  danger?: TValue | TValue[];
}>();

const type = computed(() => {
  const value = props.value;
  const danger = props.danger;
  if (danger !== undefined) {
    if (danger === value || (Array.isArray(danger) && danger.includes(value))) {
      return 'danger';
    }
  }

  const success = props.success;
  if (success !== undefined) {
    if (success === value || (Array.isArray(success) && success.includes(value))) {
      return 'success';
    }
  }

  return '';
});
</script>
