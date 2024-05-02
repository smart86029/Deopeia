<template>
  <el-form label-width="200px" :model="form" @submit.prevent="onSubmit">
    <el-form-item :label="$t('organization.employee')">
      {{ leave?.employee?.firstName }} {{ leave?.employee?.lastName }}
    </el-form-item>
    <el-form-item :label="$t('common.type')">
      {{ types.get(leave.type) }}
    </el-form-item>
    <el-form-item :label="$t('common.time')">
      {{ formatDateTime(leave.startedAt) }} ~
      {{ formatDateTime(leave.endedAt) }}
    </el-form-item>
    <el-form-item :label="$t('common.duration')">
      {{ formatDuration(leave.startedAt, leave.endedAt) }}
    </el-form-item>
    <el-form-item :label="$t('common.reason')">
      {{ leave.reason }}
    </el-form-item>
    <el-form-item :label="$t('status.approval.name')">
      <el-radio-group v-model="form.approvalStatus">
        <el-radio
          :label="$t(`status.approval.${ApprovalStatus.Approved}`)"
          :value="ApprovalStatus.Approved"
        />
        <el-radio
          :label="$t(`status.approval.${ApprovalStatus.Rejected}`)"
          :value="ApprovalStatus.Rejected"
        />
      </el-radio-group>
    </el-form-item>
    <el-form-item>
      <ButtonBack />
      <ButtonSave />
    </el-form-item>
  </el-form>
</template>

<script setup lang="ts">
import leaveApi, { type Leave } from '@/api/leave-api';
import { ApprovalStatus } from '@/models/approval-status';
import type { Guid } from '@/models/guid';
import { formatDateTime, formatDuration } from '@/plugins/dayjs';
import { success } from '@/plugins/element';

const props = defineProps<{
  action: 'create' | 'edit';
  id: Guid;
}>();
const loading = ref(true);
const types = new Map<number, string>();
const leave = ref({} as Leave);
const form = reactive({
  approvalStatus: ApprovalStatus.Approved,
});

leaveApi
  .getTypes()
  .then((x) => x.data.forEach((y) => types.set(y.value, y.name)));

leaveApi
  .get(props.id)
  .then((x) => (leave.value = x.data))
  .finally(() => (loading.value = false));

const onSubmit = () => {
  loading.value = true;
  leaveApi.updateApprovalStatus(props.id, form.approvalStatus).then(() => {
    loading.value = false;
    success(props.action);
  });
};
</script>
