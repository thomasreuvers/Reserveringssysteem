import React from "react";
import { observer } from "mobx-react";
import UserStore from "./stores/UserStore";
import LoginForm from "./components/LoginForm";
import SubmitButton from "./components/SubmitButton";
import Table from "./components/Table";
import "bootstrap/dist/css/bootstrap.min.css";
import "./App.css";

class App extends React.Component {
  // Logout function
  async doLogout() {
    try {
      let res = await fetch(
        `https://localhost:44381/api/user/logout?userCode=${UserStore.userCode}`,
        {
          method: "post",
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
          },
        }
      );

      let result = await res.json();

      if (result != null && res.ok) {
        UserStore.loggedIn = false;
        UserStore.userCode = "";
        UserStore.emailAddress = "";
        UserStore.userRole = "";
      }

      window.location.reload();
    } catch (e) {
      console.log(e);
    }
  }

  render() {
    if (UserStore.loading) {
      return (
        <div className="app">
          <div className="container">Loading, please wait..</div>
        </div>
      );
    } else {
      if (UserStore.isLoggedIn) {
        return (
          <div className="app">
            <div className="container">
              <div className="row">
                <div className="upper-body">
                  <div className="col-6">
                    <h3>Welcome {UserStore.emailAddress}</h3>
                    <SubmitButton
                      text={"Log out"}
                      disabled={false}
                      onClick={() => this.doLogout()}
                    />
                  </div>
                </div>
                <div className="col-12">
                  <Table />
                </div>
              </div>
            </div>
          </div>
        );
      }

      return (
        <div className="app">
          <div className="container">
            <LoginForm />
          </div>
        </div>
      );
    }
  }
}

export default observer(App);
