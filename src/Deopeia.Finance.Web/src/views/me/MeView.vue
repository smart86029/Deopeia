<template>
  <div class="router-view">
    <div class="flex">
      <el-menu :default-active="activeIndex" router>
        <el-menu-item
          v-for="menu of menus"
          :key="menu"
          :index="menu"
          :route="{ name: menu }"
        >
          {{ $t(`route.${menu}`) }}
        </el-menu-item>
      </el-menu>
      <RouterView />
    </div>
  </div>
</template>

<script setup lang="ts">
const menus = ['me.twoFactorAuthentication', 'asset.position'];
const activeIndex = ref(menus[0] as string | undefined);
</script>

<style lang="scss" scoped>
.router-view {
  display: flex;
  justify-content: center;

  > * {
    width: 1400px;
  }
}

.flex {
  display: flex;
  gap: 16px;
  margin-top: 16px;
}

.el-menu {
  width: 256px;
}
</style>
