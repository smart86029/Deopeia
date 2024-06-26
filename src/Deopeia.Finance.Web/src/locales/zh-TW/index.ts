import auth from './auth';
import common from './common';
import operation from './operation';
import route from './route';
import status from './status';

export default {
  ...auth,
  ...common,
  ...operation,
  ...route,
  ...status,
};
