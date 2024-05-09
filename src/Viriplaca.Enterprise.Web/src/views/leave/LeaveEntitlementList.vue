<template>
  <div v-for="entitlement of entitlements" :key="entitlement.id.value">
    {{ $t('leave.period') }} {{ entitlement.startedOn }} ~
    {{ entitlement.endedOn }}
    {{ entitlement.leaveType.name }}
    {{ entitlement.leaveType.description }}
    {{ entitlement.grantedTime.amount }}
    <br />
  </div>
</template>

<script setup lang="ts">
import leaveEntitlementApi, {
  type LeaveEntitlement,
} from '@/api/leave-entitlement-api';

const entitlements = ref([] as LeaveEntitlement[]);

leaveEntitlementApi.getList().then((x) => (entitlements.value = x.data));
</script>
