<template>Processing...</template>

<script setup lang="ts">
import { useAuthStore } from '@/stores/auth';
import { ElLoading } from 'element-plus';

const authStore = useAuthStore();
const route = useRoute();
const router = useRouter();

if (route.params.error) {
  console.log(route.params.error);
  router.replace('/');
} else {
  const loading = ElLoading.service({
    lock: true,
    text: 'Loading',
    background: 'rgba(0, 0, 0, 0.7)',
  });
  authStore.signInCallback().finally(() => {
    loading.close();
    router.replace('/');
  });
}
</script>
