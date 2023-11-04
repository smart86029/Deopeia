<template>
  <el-container>
    <el-header>
      <h1>{{ $t('common.title') }}</h1>
      <NavBreadcrumb />
      <span class="divider"></span>
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
        <el-icon size="24">
          <User />
        </el-icon>
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
        <el-menu
          :default-active="
            $route.matched.length > 2 ? $route.matched[2] : $route.path
          "
          router
        >
          <template v-for="menu in menus" :key="menu.name">
            <el-sub-menu v-if="menu.children" :index="menu.name">
              <template #title>
                {{ $t(menu.name) }}
              </template>
              <el-menu-item
                v-for="child in menu.children"
                :key="child.name"
                :index="child.path"
              >
                {{ $t(child.name) }}
              </el-menu-item>
            </el-sub-menu>
            <el-menu-item v-else :index="menu.path">
              {{ $t(menu.name) }}
            </el-menu-item>
          </template>
        </el-menu>
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
import { usePreferencesStore } from '@/stores/preferences';
import { type AppLocale } from '@/models/app-locale';

const isDark = useDark();
// const authStore = useAuthStore();
const { t } = useI18n();
const { locales, locale } = storeToRefs(usePreferencesStore());
const dialogVisible = ref(false);

const menus = [] as Menu[];

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

<style lang="scss">
h1 {
  width: 240px;
}

.el-header {
  display: flex;
  align-items: center;
  gap: 16px;
  border-bottom: 1px solid var(--el-border-color);

  .divider {
    flex: 1;
  }
}

.el-main {
  height: calc(100vh - 60px);

  > .el-scrollbar {
    margin: -20px;
  }
}

.el-main > *:not(:first-child) {
  background-color: var(--el-bg-color);
  margin-top: 10px;
}

.scrollbar-view {
  padding: 20px;
}
</style>
