<template>
  <el-dropdown @command="changeLocale">
    <IconTranslate />
    <template #dropdown>
      <el-dropdown-menu>
        <el-dropdown-item
          v-for="locale in locales"
          :key="locale.key"
          :command="locale"
        >
          {{ locale.name }}
        </el-dropdown-item>
      </el-dropdown-menu>
    </template>
  </el-dropdown>
</template>

<script setup lang="ts">
import type { AppLocale } from '@/models/app-locale';
import { usePreferencesStore } from '@/stores/preferences';

const { locales, locale } = storeToRefs(usePreferencesStore());

const changeLocale = (command: AppLocale) => {
  locale.value = command;
};
</script>
