<template>
  <el-config-provider :locale="elLocale" :empty-values="emtpyValues">
    <RouterView :key="locale.key" />
  </el-config-provider>
</template>

<script setup lang="ts">
import { usePreferencesStore } from '@/stores/preferences';
import { en as enUS, zhTw as zhTW } from 'element-plus/es/locales';
import { emptyGuid } from './models/guid';

type ElLocale = typeof enUS | typeof zhTW;
const map = new Map<string, ElLocale>([
  ['en-US', enUS],
  ['zh-TW', zhTW],
]);

const { locale } = storeToRefs(usePreferencesStore());
const elLocale = computed(() => map.get(locale.value.key));

const emtpyValues = ['', null, undefined, emptyGuid];
</script>
