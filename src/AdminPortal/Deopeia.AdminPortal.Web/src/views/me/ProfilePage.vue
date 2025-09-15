<template>
  <h2>{{ $t('route.me.profile') }}</h2>

  <UploadImage v-model="avatar" :image-url="profile.avatarUrl" />

  <p><strong>Name:</strong> {{ profile.name }}</p>
  <p><strong>Email:</strong> {{ userEmail }}</p>
</template>

<script setup lang="ts">
import { meApi, type Profile } from '@/api/me/me-api';

const userEmail = 'john.doe@example.com';
const avatar: Ref<File | undefined> = ref(undefined);

const profile: Profile = reactive({
  name: '',
  avatarUrl: '',
});

watch(avatar, (avatar) => {
  if (!avatar) {
    return;
  }
  mutate();
});

const queryClient = useQueryClient();
useQuery({
  queryKey: ['meApi.getProfile'],
  queryFn: () => meApi.getProfile().then((x) => Object.assign(profile, x)),
});

const { mutate } = useMutation({
  mutationFn: () => meApi.uploadAvatar(avatar.value!),
  onSuccess: () => queryClient.invalidateQueries({ queryKey: ['meApi.getProfile'] }),
});
</script>

<style lang="scss" scoped></style>
