<template>
  <el-dropdown @command="changeLocale">
    <IconTranslate class="icon" />
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
const route = useRoute();
const router = useRouter();

const changeLocale = (command: AppLocale) => {
  locale.value = command;
  router.replace({
    params: { ...route.params, locale: command.key },
    query: { ...route.query },
  });
};
</script>

<style lang="scss" scoped>
.icon {
  cursor: pointer;
}
</style>
