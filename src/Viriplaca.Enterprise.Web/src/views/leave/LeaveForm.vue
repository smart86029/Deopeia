<template>
  <el-form label-width="200px" :model="form" @submit.prevent="onSubmit">
    <el-form-item :label="$t('common.type')">
      <SelectOption :options="types" v-model="form.leaveTypeId" />
    </el-form-item>
    <el-form-item :label="$t('common.time')">
      <DateTimeRangePicker v-model="form.range" />
    </el-form-item>
    <el-form-item :label="$t('common.reason')">
      <el-input
        type="textarea"
        :autosize="{ minRows: 2, maxRows: 4 }"
        v-model="form.reason"
      />
    </el-form-item>
    <el-form-item>
      <ButtonBack />
      <ButtonSave />
    </el-form-item>
  </el-form>
</template>

<script setup lang="ts">
import type { Leave } from '@/api/leave-api';
import leaveApi from '@/api/leave-api';
import { Guid } from '@/models/guid';
import type { OptionResult } from '@/models/option-result';
import { success } from '@/plugins/element';

const loading = ref(true);
const types = ref([] as OptionResult<Guid>[]);
const form = reactive({
  leaveTypeId: Guid.empty,
  range: undefined as Date[] | undefined,
  reason: '',
});

leaveApi.getTypes().then((x) => (types.value = x.data));

const onSubmit = () => {
  if (!form.range) {
    return;
  }
  loading.value = true;
  leaveApi
    .apply({
      leaveTypeId: form.leaveTypeId,
      startedAt: form.range[0],
      endedAt: form.range[1],
      reason: form.reason,
    } as Leave)
    .then(() => {
      loading.value = false;
      success('create');
    });
};
</script>
