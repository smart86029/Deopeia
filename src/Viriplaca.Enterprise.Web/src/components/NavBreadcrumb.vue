<template>
  <el-breadcrumb>
    <el-breadcrumb-item v-for="location in locations" :key="location.path">
      <router-link :to="location.path">
        {{ $t(`route.${location.name?.toString()}`, { id }) }}
      </router-link>
    </el-breadcrumb-item>
  </el-breadcrumb>
</template>

<script setup lang="ts">
const route = useRoute();
const locations = computed(() => route.matched.filter((x) => !!x.name));
const id = computed(() => route.params.id);
</script>

<style scoped lang="scss">
.el-breadcrumb-item:last-child {
  pointer-events: none;
}
</style>
