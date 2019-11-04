"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var react_router_dom_1 = require("react-router-dom");
require("./App.css");
var logo_svg_1 = require("./logo.svg");
var Utils = require("./Utils");
var ExtendedRoute_1 = require("./ExtendedRoute");
var react_bootstrap_1 = require("react-bootstrap");
var Login_1 = require("./Login");
var App = /** @class */ (function (_super) {
    __extends(App, _super);
    function App(props) {
        var _this = _super.call(this, props) || this;
        _this.state = {
            reloadApp: false,
            isAuthenticated: Utils.hasJwtToken(),
        };
        _this.onLogoutClick = _this.onLogoutClick.bind(_this);
        return _this;
    }
    App.prototype.onLogoutClick = function (event) {
        var _this = this;
        event.preventDefault();
        Utils.userLogoutFetch()
            .then(function () {
            _this.setState(function () { return ({
                reloadApp: true,
                isAuthenticated: false,
            }); });
        });
    };
    App.prototype.render = function () {
        var _this = this;
        return (React.createElement(react_router_dom_1.BrowserRouter, null,
            React.createElement("div", null,
                React.createElement("div", { className: "App" },
                    React.createElement("header", { className: "App-header" },
                        React.createElement("img", { src: logo_svg_1.default, className: "App-logo", alt: "logo" }),
                        React.createElement("h1", { className: "App-title" }, "Welcome to Mangau Will Need Umbrella"),
                        this.state.isAuthenticated ? React.createElement(react_bootstrap_1.Button, { size: "sm", onClick: this.onLogoutClick }, "Logout") : React.createElement("span", null),
                        this.state.reloadApp ? React.createElement(react_router_dom_1.Redirect, { to: '/' }) : React.createElement("span", null))),
                React.createElement(react_router_dom_1.Switch, null,
                    React.createElement(ExtendedRoute_1.ExtendedRoute, { exact: true, path: "/", component: exports.Home, useAlternatePath: !this.state.isAuthenticated, alternatePath: "/login" }),
                    React.createElement(ExtendedRoute_1.ExtendedRoute, { exact: true, path: "/foo", component: exports.Foo, useAlternatePath: !this.state.isAuthenticated, alternatePath: "/login" }),
                    React.createElement(ExtendedRoute_1.ExtendedRoute, { exact: true, path: "/login", component: Login_1.default, withProps: {
                            onLogin: function (event) {
                                _this.setState(function () { return ({
                                    reloadApp: false,
                                    isAuthenticated: true,
                                }); });
                            }
                        } })))));
    };
    return App;
}(React.Component));
exports.default = App;
exports.Home = function () { return React.createElement("h1", null, "Home Page"); };
exports.Foo = function () { return React.createElement("h1", null, "Foo Page"); };
//# sourceMappingURL=App.js.map