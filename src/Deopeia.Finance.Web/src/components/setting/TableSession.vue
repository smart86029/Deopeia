<template>
  <el-table :data="model">
    <el-table-column :label="$t('common.dayOfTheWeek')">
      <template #default="{ row }">
        {{ weekday(row.openDay) }}
      </template>
    </el-table-column>
    <el-table-column prop="openTime" :label="$t('common.time')" />
    <el-table-column :label="$t('common.dayOfTheWeek')">
      <template #default="{ row }">
        {{ row.openDay === row.closeDay ? '' : weekday(row.closeDay) }}
      </template>
    </el-table-column>
    <el-table-column prop="closeTime" :label="$t('common.time')" />
    <el-table-column width="auto" align="right">
      <template #default="{ $index }">
        <el-button type="danger" plain @click="removeSession($index)">
          {{ $t('operation.remove') }}
        </el-button>
      </template>
    </el-table-column>

    <template #append>
      <tr class="new-session">
        <td class="el-table__cell">
          <div class="cell">
            <SelectDayOfWeek v-model="openDay" />
          </div>
        </td>
        <td class="el-table__cell">
          <div class="cell">
            <TimePicker v-model="openTime" />
          </div>
        </td>
        <td class="el-table__cell new-session-add">
          <div class="cell">
            <el-button type="primary" plain @click="addSession">
              {{ $t('operation.add') }}
            </el-button>
          </div>
        </td>
      </tr>
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
const times: [Date, Date] = reactive([
  dayjs('00:00:00', 'HH:mm:ss').toDate(),
  dayjs('01:00:00', 'HH:mm:ss').toDate(),
]);
const openTime = ref('00:00:00');
const closeTime = ref('00:00:00');

const addSession = () => {
  if (openDay.value === undefined) {
    return;
  }
  model.value.push({
    openDay: openDay.value,
    openTime: dayjs(times[0]).format('HH:mm:ss'),
    closeDay: openDay.value,
    closeTime: dayjs(times[1]).format('HH:mm:ss'),
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

  .new-session-add {
    flex: 1;
    text-align: right;
  }
}
</style>
