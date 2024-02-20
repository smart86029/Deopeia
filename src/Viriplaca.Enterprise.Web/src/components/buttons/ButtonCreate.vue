<template>
  <router-link custom :to="{ name: route }" v-slot="{ navigate }">
    <el-button type="primary" plain @click="navigate">
      {{ $t('operation.create') }}
    </el-button>
  </router-link>
</template>

<script setup lang="ts">
defineProps<{
  route: string;
}>();
</script>
