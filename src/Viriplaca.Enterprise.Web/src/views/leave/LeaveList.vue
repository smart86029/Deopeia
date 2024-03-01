<template>
  <div class="toolbar">
    <el-form :inline="true" :model="form">
      <el-form-item :label="$t('common.account')">
        <el-input v-model="form.accounts" />
      </el-form-item>
    </el-form>
    <FlexDivider />
    <ButtonCreate route="leave.apply" :text="$t('operation.apply')" />
  </div>
  <el-table v-loading="loading" :data="leaves">
    <el-table-column prop="type" :label="$t('common.type')" />
    <el-table-column prop="startedAt" :label="$t('common.startTime')" />
    <el-table-column prop="endedAt" :label="$t('common.endTime')" />
    <el-table-column prop="duration" :label="$t('common.duration')" />
    <el-table-column prop="status" :label="$t('common.status')" />
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
import leaveApi, { type Leave } from '@/api/leave-api';
import { type Option } from '@/models/option';

const loading = ref(false);
const leaves: Ref<Leave[]> = ref([]);
const types: Ref<Option<number>[]> = ref([]);
const form = reactive({
  accounts: '  ',
});

watch(form, (form) => {
  loading.value = true;
  leaveApi
    .getList('')
    .then((x) => {
      leaves.value = x.data.items;
    })
    .finally(() => (loading.value = false));
});

form.accounts = '';
</script>
