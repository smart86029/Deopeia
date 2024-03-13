<template>
  <el-button @click="() => $router.go(-1)">
    {{ $t('operation.back') }}
  </el-button>
</template>
