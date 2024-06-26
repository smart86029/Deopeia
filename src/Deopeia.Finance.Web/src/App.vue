<template>
  <el-config-provider :locale="elLocale">
    <RouterView />
  </el-config-provider>
</template>

<script setup lang="ts">
import i18n from '@/plugins/i18n';
import { en as enUS, zhTw as zhTW } from 'element-plus/es/locales';

type ElLocale = typeof enUS | typeof zhTW;
const map = new Map<string, ElLocale>([
  ['en-US', enUS],
  ['zh-TW', zhTW],
]);

const elLocale = computed(() => map.get(i18n.global.locale.value));
</script>
