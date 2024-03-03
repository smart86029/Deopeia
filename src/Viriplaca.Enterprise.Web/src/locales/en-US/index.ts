import auth from './auth';
import common from './common';
import department from './department';
import leave from './leave';
import route from './route';
import status from './status';

export default {
  ...auth,
  ...common,
  ...department,
  ...leave,
  ...route,
  ...status,
};
