<template>
  <el-dialog :title="$t('auth.changePassword')">
    <template #footer>
      <el-button>{{ $t('operation.cancel') }}</el-button>
      <el-button type="primary">
        {{ $t('operation.save') }}
      </el-button>
    </template>
  </el-dialog>
</template>

<script setup lang="ts"></script>
