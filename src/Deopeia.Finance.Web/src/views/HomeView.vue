<template>
  <el-container>
    <el-header>
      <MainHeader />
    </el-header>
    <el-container>
      <el-aside class="menu-aside" :class="{ close: asideSwitch }">
        <el-scrollbar>
          <AsideMenu />
        </el-scrollbar>
      </el-aside>
      <el-main>
        <el-scrollbar view-class="scrollbar-view">
          <RouterView />
        </el-scrollbar>
      </el-main>
    </el-container>
  </el-container>
</template>

<script setup lang="ts">
// import commonApi from '@/api/common-api';
// import { useAuthStore } from '@/stores/auth';
// import { useGlobal } from '@/stores/global';

const asideSwitch = ref(false);

// commonApi.getGlobal().then((res) => {
//   imageDomain.value = res.data.imageDomain;
//   navigationUrl.value = res.data.navigationUrl;
//   userLevel.value = res.data.userLevel;
// });
// const authStore = useAuthStore();
// // 監聽切換分頁回來, 會主動打refresh
// document.addEventListener('visibilitychange', function () {
//   if (document.visibilityState === 'visible') {
//     authStore.refresh();
//   }
// });
</script>

<style scoped lang="scss">
.menu-aside {
  width: var(--aside-menu-width);
  transition: all 0.3s cubic-bezier(0, 0, 0.5, 1);
  &.close {
    width: calc(
      var(--el-menu-icon-width) + var(--el-menu-base-level-padding) * 2
    );
  }
}
</style>
