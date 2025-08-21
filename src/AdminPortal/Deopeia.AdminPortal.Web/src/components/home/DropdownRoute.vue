<template>
  <el-dropdown @command="memberRoute">
    <IconPerson class="icon" />
    <template #dropdown>
      <el-dropdown-menu>
        <el-dropdown-item command="settings">
          {{ $t('common.settings') }}
        </el-dropdown-item>
        <el-dropdown-item command="signOut">
          {{ $t('auth.signOut') }}
        </el-dropdown-item>
      </el-dropdown-menu>
    </template>
  </el-dropdown>
</template>

<script setup lang="ts">
import router from '@/router';
import { useAuthStore } from '@/stores/auth';

const authStore = useAuthStore();

const memberRoute = (command: string) => {
  switch (command) {
    case 'settings':
      router.push({ name: 'me.view' });
      break;

    case 'signOut':
      authStore.signOut();
      break;
  }
};
</script>

<style lang="scss" scoped>
.icon {
  cursor: pointer;
}
</style>
