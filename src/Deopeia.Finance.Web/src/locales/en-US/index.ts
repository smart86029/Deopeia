import auth from './auth';
import common from './common';
import finance from './finance';
import operation from './operation';
import route from './route';
import status from './status';

export default {
  ...auth,
  ...common,
  ...finance,
  ...operation,
  ...route,
  ...status,
};
