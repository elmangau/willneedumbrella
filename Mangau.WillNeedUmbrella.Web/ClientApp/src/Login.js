"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
require("./Login.css");
var React = require("react");
var react_1 = require("react");
var Utils = require("./Utils");
var react_bootstrap_1 = require("react-bootstrap");
var react_router_1 = require("react-router");
var Login = function (_a) {
    var onLogin = _a.onLogin;
    var _b = react_1.useState(""), username = _b[0], setUsername = _b[1];
    var _c = react_1.useState(""), password = _c[0], setPassword = _c[1];
    //const [success, setSuccess] = useState<boolean>(Utils.hasJwtToken());
    function validateForm() {
        return username.length > 0 && password.length > 0;
    }
    function onLoginClick(event) {
        event.preventDefault();
        Utils.userLoginFetch({ 'username': username, 'password': password })
            .then(function (data) {
            onLogin({});
            //setSuccess(true);
        });
    }
    return (Utils.hasJwtToken() ? React.createElement(react_router_1.Redirect, { to: "/" }) :
        React.createElement("div", { className: "Login" },
            React.createElement(react_bootstrap_1.Form, { onSubmit: onLoginClick },
                React.createElement(react_bootstrap_1.FormGroup, { controlId: "username" },
                    React.createElement(react_bootstrap_1.FormLabel, null, "User Name"),
                    React.createElement(react_bootstrap_1.FormControl, { autoFocus: true, type: "text", value: username, onChange: function (e) { return setUsername(e.target.value); } })),
                React.createElement(react_bootstrap_1.FormGroup, { controlId: "password" },
                    React.createElement(react_bootstrap_1.FormLabel, null, "Password"),
                    React.createElement(react_bootstrap_1.FormControl, { value: password, onChange: function (e) { return setPassword(e.target.value); }, type: "password" })),
                React.createElement(react_bootstrap_1.Button, { block: true, disabled: !validateForm(), type: "submit" }, "Login"))));
};
exports.default = Login;
//# sourceMappingURL=Login.js.map