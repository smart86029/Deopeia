<template>
  <el-dropdown @command="memberRoute">
    <IconPerson />
    <template #dropdown>
      <el-dropdown-menu>
        <el-dropdown-item command="changePassword">
          {{ $t('auth.changePassword') }}
        </el-dropdown-item>
        <el-dropdown-item command="signOut">
          {{ $t('auth.signOut') }}
        </el-dropdown-item>
      </el-dropdown-menu>
    </template>
  </el-dropdown>
  <DialogChangePassword v-model="dialogVisible" />
</template>

<script setup lang="ts">
import { useAuthStore } from '@/stores/auth';

const authStore = useAuthStore();
const dialogVisible = ref(false);

const memberRoute = (command: string) => {
  switch (command) {
    case 'changePassword':
      dialogVisible.value = true;
      break;

    case 'signOut':
      authStore.signIn();
      break;
  }
};
</script>
