<template>Processing...</template>

<script setup lang="ts">
import { useAuthStore } from '@/stores/auth';
import { ElLoading, ElMessageBox } from 'element-plus';

const authStore = useAuthStore();
const route = useRoute();
const router = useRouter();

const error = route.params.error;
if (error) {
  ElMessageBox.close();
  ElMessageBox.alert(error.toString());
  router.replace('/');
} else {
  const loading = ElLoading.service({
    lock: true,
    text: 'Loading',
    background: 'rgba(0, 0, 0, 0.7)',
  });
  authStore.signInCallback().finally(() => {
    loading.close();
    router.push('/');
  });
}
</script>
