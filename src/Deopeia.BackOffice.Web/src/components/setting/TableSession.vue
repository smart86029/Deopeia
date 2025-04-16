<template>
  <el-table :data="model">
    <el-table-column :label="$t('common.startTime')">
      <template #default="{ row }">
        {{ weekday(row.openDay) }}
      </template>
    </el-table-column>
    <el-table-column prop="openTime" />
    <el-table-column :label="$t('common.endTime')">
      <template #default="{ row }">
        {{ row.openDay === row.closeDay ? '' : weekday(row.closeDay) }}
      </template>
    </el-table-column>
    <el-table-column prop="closeTime" />
    <el-table-column width="auto" align="right">
      <template #default="{ $index }">
        <el-button type="danger" plain @click="removeSession($index)">
          {{ $t('operation.remove') }}
        </el-button>
      </template>
    </el-table-column>

    <template #append>
      <div class="new-session">
        <SelectDayOfWeek v-model="openDay" class="day" />
        <div class="cell">
          <TimePicker v-model="openTime" />
        </div>
        <SelectDayOfWeek v-model="closeDay" class="day" disabled />
        <div class="cell">
          <TimePicker v-model="closeTime" />
        </div>
        <div class="cell add">
          <el-button type="primary" plain @click="addSession">
            {{ $t('operation.add') }}
          </el-button>
        </div>
      </div>
    </template>
  </el-table>
</template>

<script setup lang="ts">
import { DayOfWeek } from '@/models/day-of-week';
import type { Session } from '@/models/trading/session';
import { weekday } from '@/plugins/dayjs';
import { dayjs } from 'element-plus';

const model = defineModel<Session[]>({ required: true });

const openDay: Ref<DayOfWeek | undefined> = ref(undefined);
const openTime = ref('00:00:00');
const closeTime = ref('00:00:00');

const closeDay = computed(() => {
  if (openDay.value === undefined) {
    return undefined;
  }
  if (openTime.value < closeTime.value) {
    return openDay.value;
  }
  return (openDay.value + 1) % 7;
});

const addSession = () => {
  if (openDay.value === undefined) {
    return;
  }
  const index = model.value.findIndex(
    (x) =>
      x.openDay > openDay.value! ||
      (x.openDay === openDay.value &&
        dayjs(x.openTime, 'HH:mm:ss').isAfter(
          dayjs(openTime.value, 'HH:mm:ss'),
        )),
  );
  model.value.splice(index, 0, {
    openDay: openDay.value,
    openTime: openTime.value,
    closeDay: closeDay.value!,
    closeTime: closeTime.value,
  });
};

const removeSession = (index: number) => model.value.splice(index, 1);
</script>

<style scoped lang="scss">
.sessions {
  display: flex;
  flex-direction: column;

  .el-text {
    align-self: flex-start;
  }
}

.new-session {
  display: flex;
  width: 100%;

  .day {
    flex: 0 1 20%;
    padding: 8px 0;
    min-width: 0;
  }

  .cell {
    flex: 0 1 20%;
    padding: 8px 12px;

    :deep(.el-date-editor.el-input) {
      width: unset;
    }

    &.add {
      text-align: right;
    }
  }

  .new-session-add {
    text-align: right;
  }
}
</style>
