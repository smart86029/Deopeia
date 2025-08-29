<template>
  <el-tab-pane :label="label" :name="locale.culture" :closable="closable">
    <slot></slot>
  </el-tab-pane>
</template>

<script setup lang="ts">
import { useCultureOptions } from '@/composables/useCultureOptions';

const props = defineProps<{ locale: Locale }>();

const { data } = useCultureOptions();

const label = computed(
  () => data.value?.find((x) => x.value === props.locale.culture)?.name ?? props.locale.culture,
);

const closable = computed(() => props.locale.culture !== 'en');
</script>
