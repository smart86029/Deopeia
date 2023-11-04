<template>
  <el-config-provider :locale="elLocale">
    <RouterView />
  </el-config-provider>
</template>

<script setup lang="ts">
import { usePreferencesStore } from '@/stores/preferences';
import { en as enUS, zhTw as zhTW } from 'element-plus/es/locales';

const { locale } = storeToRefs(usePreferencesStore());
type ElLocale = typeof enUS | typeof zhTW;
const map = new Map<string, ElLocale>([
  ['en-US', enUS],
  ['zh-TW', zhTW],
]);

const elLocale = computed(() => map.get(locale.value.key));
</script>
