import auth from './auth';
import common from './common';
import finance from './finance';
import fund from './fund';
import identity from './identity';
import operation from './operation';
import route from './route';
import status from './status';
import trading from './trading';

export default {
  ...auth,
  ...common,
  ...finance,
  ...fund,
  ...identity,
  ...operation,
  ...route,
  ...status,
  ...trading,
};
