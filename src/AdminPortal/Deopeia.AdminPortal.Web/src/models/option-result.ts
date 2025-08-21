/**
 * Represents an option item in dropdown selections and choice lists
 */
export interface OptionResult<TValue = unknown> {
  name: string;
  value: TValue;
  isEnabled: boolean;
}
