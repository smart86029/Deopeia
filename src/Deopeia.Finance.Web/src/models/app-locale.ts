type Key = 'en-US' | 'zh-TW';

export interface AppLocale {
  name: string;
  key: Key;
  languageCode: string;
  dayjsCode: string;
}
