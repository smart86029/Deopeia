/**
 * Represents a locale with culture identifier
 * Used for content localization in forms and data models
 */
export interface Locale {
  /** BCP 47 culture code (e.g., 'en-US', 'zh-TW') */
  culture: string;
}

/**
 * Interface for entities that support multiple locales
 * Provides a standard way to handle localized content
 */
export interface Localizable {
  /** Array of localized versions of the content */
  locales: Locale[];
}

/**
 * Utility type for creating locale-specific content
 * Extends base Locale with additional content properties
 */
export type LocalizedContent<T = Record<string, unknown>> = Locale & T;
