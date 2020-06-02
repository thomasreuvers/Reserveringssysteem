import { extendObservable } from "mobx";

/**
 * UserStore
 */

class UserStore {
  constructor() {
    extendObservable(this, {
      loading: false,
      isLoggedIn: false,
      userCode: "",
      emailAddress: "",
      userRole: "",
    });
  }
}

export default new UserStore();
