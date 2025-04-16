type Key = 'en' | 'zh-Hant';

export interface AppLocale {
  name: string;
  key: Key;
  dayjsCode: string;
}
