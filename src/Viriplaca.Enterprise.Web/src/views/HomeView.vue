<template>
  <el-container>
    <el-header>
      <h1>{{ $t('common.title') }}</h1>
      <NavBreadcrumb />
      <FlexDivider />
      <el-switch
        v-model="isDark"
        inline-prompt
        active-icon="moon"
        inactive-icon="sunny"
      />
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
      <el-dropdown @command="memberRoute">
        <IconPerson />
        <template #dropdown>
          <el-dropdown-menu>
            <el-dropdown-item command="changePassword">
              {{ $t('route.auth.changePassword') }}
            </el-dropdown-item>
            <el-dropdown-item command="signOut">
              {{ $t('auth.signOut') }}
            </el-dropdown-item>
          </el-dropdown-menu>
        </template>
      </el-dropdown>
    </el-header>
    <el-container>
      <el-aside width="256px">
        <el-scrollbar view-class="scrollbar-view">
          <NavAside />
        </el-scrollbar>
      </el-aside>
      <el-main>
        <el-scrollbar view-class="scrollbar-view">
          <RouterView />
        </el-scrollbar>
      </el-main>
    </el-container>
  </el-container>
  <ChangePasswordDialog v-model="dialogVisible" />
</template>

<script setup lang="ts">
// import { useAuthStore } from '@/stores/auth';
import { type AppLocale } from '@/models/app-locale';
import { usePreferencesStore } from '@/stores/preferences';

const isDark = useDark();
// const authStore = useAuthStore();
const { t } = useI18n();
const { locales, locale } = storeToRefs(usePreferencesStore());
const dialogVisible = ref(false);

const changeLocale = (command: AppLocale) => {
  locale.value = command;
};

const memberRoute = (command: string) => {
  switch (command) {
    case 'changePassword':
      dialogVisible.value = true;
      break;

    case 'signOut':
      // ElMessageBox.confirm(t('auth.signOutConfirm')).then(() =>
      //    authStore.signOut().then(() => router.push({ name: 'auth.signIn' })),
      // );
      break;
  }
};
</script>

<style scoped lang="scss">
h1 {
  width: 240px;
}

.el-header {
  display: flex;
  align-items: center;
  gap: 16px;
  border-bottom: 1px solid var(--el-border-color);
}

.el-aside {
  height: calc(100vh - 60px);
}

.el-main {
  height: calc(100vh - 60px);
  padding: 0;

  > .el-scrollbar {
    height: 100%;
  }

  > *:not(:first-child) {
    background-color: var(--el-bg-color);
    margin-top: 10px;
  }

  :deep(.scrollbar-view) {
    padding: var(--el-main-padding);
  }
}
</style>
