<template>
  <el-breadcrumb>
    <el-breadcrumb-item
      v-for="location in locations"
      :key="location.name"
      :to="location.to"
    >
      {{ location.name }}
    </el-breadcrumb-item>
  </el-breadcrumb>
</template>

<script setup lang="ts">
const { t } = useI18n();
const route = useRoute();
const locations = computed(() =>
  route.matched
    .filter((x) => x.name && !x.name?.toString().endsWith('default'))
    .map((x, index, array) => ({
      name: t(`route.${x.name?.toString()}`, { ...route.params }),
      to:
        x.name?.toString().endsWith('module') || index === array.length - 1
          ? undefined
          : {
              name: x.name,
              params: route.params,
            },
    })),
);
</script>
