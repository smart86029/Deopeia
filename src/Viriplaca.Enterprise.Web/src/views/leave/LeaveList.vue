<template>
  <div class="toolbar">
    <el-form :inline="true" :model="form">
      <el-form-item :label="$t('common.account')">
        <el-input v-model="form.accounts" />
      </el-form-item>
    </el-form>
    <FlexDivider />
    <ButtonCreate route="leave.create" />
  </div>
  <el-table v-loading="loading" :data="leaves">
    <el-table-column prop="type" :label="$t('common.type')" />
    <el-table-column prop="startedAt" :label="$t('common.nickname')" />
    <el-table-column prop="endedAt" :label="$t('common.status')" />
    <el-table-column prop="status" :label="$t('operator.role')" />
    <el-table-column :label="$t('common.operations')">
      <template #default="{ row }">
        <TextLink
          :to="{ name: 'operator.edit', params: { id: row.id } }"
          :text="$t('operation.edit')"
        />
      </template>
    </el-table-column>
  </el-table>
</template>

<script setup lang="ts">
import { type Leave } from '@/api/leave-api';
import { type Option } from '@/models/option';

const loading = ref(false);
const leaves: Ref<Leave[]> = ref([]);
const types: Ref<Option<number>[]> = ref([]);
const form = reactive({
  accounts: '  ',
});

// watch(form, async (form) => {
//   loading.value = true;
//   leaves.value = await leaveApi
//     .getList('')
//     .finally(() => (loading.value = false));
// });

form.accounts = '';
</script>
