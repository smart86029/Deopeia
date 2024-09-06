import auth from './auth';
import common from './common';
import finance from './finance';
import identity from './identity';
import operation from './operation';
import route from './route';
import status from './status';

export default {
  ...auth,
  ...common,
  ...finance,
  ...identity,
  ...operation,
  ...route,
  ...status,
};
