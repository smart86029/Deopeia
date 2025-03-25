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
  flex-direction: column;
  align-items: center;

  > * {
    width: 1400px;
  }
}

.overview {
  display: flex;
  gap: 16px;
}

.flex {
  display: flex;
  gap: 16px;
  margin-top: 16px;
}

.el-statistic {
  --el-statistic-content-font-size: 28px;
}

.statistic-card {
  height: 100%;
  padding: 20px;
  border-radius: 4px;
  background-color: var(--el-bg-color-overlay);
  text-align: center;
  flex: 1;
}

.balance {
  flex: 2;
}

.el-menu {
  width: 256px;
}
</style>
