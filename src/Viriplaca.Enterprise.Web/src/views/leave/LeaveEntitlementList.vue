<template>
  <el-card v-for="entitlement of entitlements" :key="entitlement.id.value">
    <template #header>
      {{ entitlement.leaveType.name }}
    </template>
    {{ entitlement.startedOn }} ~
    {{ entitlement.endedOn }}
    <br />
    {{ $t('leave.lengthGranted') }}
    {{ formatWorkingTime(entitlement.grantedTime) }}
    <br />
    {{ entitlement.leaveType.description }}
  </el-card>
</template>

<script setup lang="ts">
import leaveEntitlementApi, {
  type LeaveEntitlement,
} from '@/api/leave-entitlement-api';

import { formatWorkingTime } from '@/models/leave/working-time';

const entitlements = ref([] as LeaveEntitlement[]);

leaveEntitlementApi.getList().then((x) => (entitlements.value = x.data));
</script>

<style scoped lang="scss">
.el-card {
  max-width: 480px;
}
</style>
