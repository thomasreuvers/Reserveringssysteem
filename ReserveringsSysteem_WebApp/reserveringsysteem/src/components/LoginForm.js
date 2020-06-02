import React from "react";
import InputField from "./InputField";
import SubmitButton from "./SubmitButton";
import UserStore from "../stores/UserStore";

class LoginForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      emailAddress: "",
      password: "",
      buttonDisabled: false,
    };
  }

  setInputValue(property, val) {
    val = val.trim();
    if (val.length > 12) {
      return;
    }

    this.setState({
      [property]: val,
    });
  }

  resetForm() {
    this.setState({
      emailAddress: "",
      password: "",
      buttonDisabled: false,
    });
  }

  async doLogin() {
    if (!this.state.emailAddress) {
      return;
    }
    if (!this.state.password) {
      return;
    }

    this.setState({
      buttonDisabled: true,
    });

    try {
      let res = await fetch("https://localhost:44381/api/user/login", {
        method: "post",
        headers: {
          Accept: "Application/json",
          "Content-Type": "Application/json",
        },
        body: JSON.stringify({
          Email: this.state.emailAddress,
          Password: this.state.password,
        }),
      });

      let result = await res.json();

      if (result != null && res.ok) {
        UserStore.isLoggedIn = true;
        UserStore.userCode = result.userCode;
        UserStore.emailAddress = result.emailAddress;
        UserStore.userRole = result.role;
      } else if (!res.ok || result == null) {
        this.resetForm();
        alert(result);
      }
    } catch (e) {
      console.log(e);
      this.resetForm();
    }
  }

  render() {
    return (
      <div className="login-form-parent">
        <div className="loginForm">
          <h2>Login</h2>
          <InputField
            type="text"
            placeholder="Email"
            value={this.state.emailAddress ? this.state.emailAddress : ""}
            onChange={(val) => this.setInputValue("emailAddress", val)}
          />
          <InputField
            type="password"
            placeholder="Password"
            value={this.state.password ? this.state.password : ""}
            onChange={(val) => this.setInputValue("password", val)}
          />
          <SubmitButton
            text="Login"
            disabled={this.state.buttonDisabled}
            onClick={() => this.doLogin()}
            className="loginform-btn"
          />
        </div>
      </div>
    );
  }
}

export default LoginForm;
