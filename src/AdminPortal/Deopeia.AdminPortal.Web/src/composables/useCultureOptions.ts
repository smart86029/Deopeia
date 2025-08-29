import { optionApi } from '@/api/option-api';

export function useCultureOptions() {
  const { data, isFetching } = useQuery({
    queryKey: ['optionApi.getCultures'],
    queryFn: () => optionApi.getCultures(),
    staleTime: 5 * 60 * 1000,
  });

  return {
    data,
    isFetching,
  };
}
