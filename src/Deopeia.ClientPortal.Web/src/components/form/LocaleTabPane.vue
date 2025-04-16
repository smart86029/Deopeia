<template>
  <el-tab-pane
    :label="cultures.find((x) => x.value === locale.culture)?.name"
    :name="locale.culture"
    :closable="locale.culture !== 'en'"
  >
    <slot></slot>
  </el-tab-pane>
</template>

<script setup lang="ts">
import type { Locale } from '@/models/localization';
import { useOptionStore } from '@/stores/option';

defineProps<{ locale: Locale }>();

const { cultures } = storeToRefs(useOptionStore());
</script>
