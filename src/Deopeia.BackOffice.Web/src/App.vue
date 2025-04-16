<template>
  <el-config-provider :locale="elLocale" :empty-values="emtpyValues">
    <RouterView :key="locale.key" />
  </el-config-provider>
</template>

<script setup lang="ts">
import { usePreferencesStore } from '@/stores/preferences';
import { en, zhTw } from 'element-plus/es/locales';
import { emptyGuid } from './models/guid';

type ElLocale = typeof en | typeof zhTw;
const map = new Map<string, ElLocale>([
  ['en', en],
  ['zh-Hant', zhTw],
]);

const { locale } = storeToRefs(usePreferencesStore());
const elLocale = computed(() => map.get(locale.value.key));

const emtpyValues = ['', null, undefined, emptyGuid];
</script>
