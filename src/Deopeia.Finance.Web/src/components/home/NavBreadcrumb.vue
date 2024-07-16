<template>
  <el-breadcrumb>
    <el-breadcrumb-item v-for="location in locations" :key="location.name">
      <router-link :to="location.to">
        {{ location.name }}
      </router-link>
    </el-breadcrumb-item>
  </el-breadcrumb>
</template>

<script setup lang="ts">
const { t } = useI18n();
const route = useRoute();
const locations = computed(() =>
  route.matched
    .filter((x) => !x.name?.toString().endsWith('default'))
    .map((x) => ({
      name: t(`route.${x.name?.toString()}`, {
        id: route.params.id,
        symbol: route.params.symbol,
      }),
      to: x,
    })),
);
</script>

<style scoped lang="scss">
.el-breadcrumb-item:last-child {
  pointer-events: none;
}
</style>
