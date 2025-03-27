<template>
  <div class="view">
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
      <div>
        <RouterView />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
const menus = ['me.password', 'me.twoFactorAuthentication'];
const activeIndex = ref(menus[0] as string | undefined);
</script>

<style lang="scss" scoped>
.view {
  display: flex;
  justify-content: center;
}

.flex {
  display: flex;
  gap: 16px;
  margin-top: 16px;
  width: 1400px;
}

.el-menu {
  width: 256px;
}
</style>
