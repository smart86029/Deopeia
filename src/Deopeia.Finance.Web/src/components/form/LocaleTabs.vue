<template>
  <el-tabs editable @tab-add="openDialog">
    <slot></slot>

    <el-dialog
      v-model="dialogVisible"
      width="500"
      :title="$t('common.message.selectLocale')"
    >
      <el-form label-width="120">
        <el-form-item :label="$t('common.locale')">
          <SelectOption v-model="culture" :options="cultures" />
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button type="primary" @click="add">
          {{ $t('operation.add') }}
        </el-button>
      </template>
    </el-dialog>
  </el-tabs>
</template>

<script setup lang="ts">
import * as localeCodes from 'locale-codes';

const emits = defineEmits<{
  update: [locale: string];
}>();

const dialogVisible = ref(false);
const cultures = localeCodes.all.map((x) => ({
  name: `${x.name} (${x.tag})`,
  value: x.tag,
  isEnabled: true,
}));
const culture = ref('');

const openDialog = () => (dialogVisible.value = true);

const add = () => {
  emits('update', culture.value);
  dialogVisible.value = false;
};
</script>

<style scoped lang="scss">
:deep(.el-tabs__header) {
  padding-left: 200px;
}
</style>
