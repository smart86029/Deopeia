<template>
  <el-container v-if="isOperator">
    <el-header>
      <h1>{{ $t('common.title') }}</h1>
      <NavBreadcrumb />
      <FlexDivider />
      <el-switch
        v-model="isDark"
        inline-prompt
        active-icon="moon"
        inactive-icon="sunny"
      />
      <DropdownLocale />
      <DropdownRoute />
    </el-header>
    <el-container>
      <el-aside width="256px">
        <el-scrollbar view-class="scrollbar-view">
          <NavOperator />
        </el-scrollbar>
      </el-aside>
      <el-main>
        <el-scrollbar view-class="scrollbar-view">
          <RouterView />
        </el-scrollbar>
      </el-main>
    </el-container>
  </el-container>
  <el-container v-else>
    <el-header>
      <h1>{{ $t('common.title') }}</h1>
      <NavTrader />
      <FlexDivider />
      <el-switch
        v-model="isDark"
        inline-prompt
        active-icon="moon"
        inactive-icon="sunny"
      />
      <DropdownLocale />
      <DropdownRoute />
    </el-header>
    <el-main>
      <el-scrollbar view-class="scrollbar-view">
        <RouterView />
      </el-scrollbar>
    </el-main>
  </el-container>
</template>

<script setup lang="ts">
const isDark = useDark();
const isOperator = false;
</script>

<style scoped lang="scss">
h1 {
  width: 240px;
}

.el-header {
  display: flex;
  align-items: center;
  gap: 16px;
  border-bottom: 1px solid var(--el-border-color);
}

.el-aside {
  height: calc(100vh - 60px);
}

.el-main {
  height: calc(100vh - 60px);
  padding: 0;

  > .el-scrollbar {
    height: 100%;
  }

  :deep(.scrollbar-view) {
    padding: var(--el-main-padding);
  }
}
</style>
