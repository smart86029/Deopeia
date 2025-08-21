/**
 * Supported locale keys for the application
 */
type LocaleKey = 'en' | 'zh-Hant';

/**
 * Application locale configuration
 */
export interface AppLocale {
  name: string;
  key: LocaleKey;
  dayjsCode: string;
}
