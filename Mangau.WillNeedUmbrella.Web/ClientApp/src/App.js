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
var App = /** @class */ (function (_super) {
    __extends(App, _super);
    function App() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    App.prototype.render = function () {
        return (React.createElement(react_router_dom_1.BrowserRouter, null,
            React.createElement("div", null,
                React.createElement("div", { className: "App" },
                    React.createElement("header", { className: "App-header" },
                        React.createElement("img", { src: logo_svg_1.default, className: "App-logo", alt: "logo" }),
                        React.createElement("h1", { className: "App-title" }, "Welcome to Mangau Will Need Umbrella"))),
                React.createElement("nav", null,
                    React.createElement(react_router_dom_1.Link, { to: "/" }, "Home"),
                    React.createElement(react_router_dom_1.Link, { to: "/foo" }, "Foo"),
                    React.createElement(react_router_dom_1.Link, { to: "/bar" }, "Bar")),
                React.createElement(react_router_dom_1.Switch, null,
                    React.createElement(react_router_dom_1.Route, { exact: true, path: "/", component: exports.Home }),
                    React.createElement(react_router_dom_1.Route, { exact: true, path: "/foo", component: exports.Foo }),
                    React.createElement(react_router_dom_1.Route, { exact: true, path: "/bar", component: exports.Bar })))));
    };
    return App;
}(React.Component));
exports.default = App;
exports.Home = function () { return React.createElement("h1", null, "Home Page"); };
exports.Foo = function () { return React.createElement("h1", null, "Foo Page"); };
exports.Bar = function () { return React.createElement("h1", null, "Bar Page"); };
//# sourceMappingURL=App.js.map