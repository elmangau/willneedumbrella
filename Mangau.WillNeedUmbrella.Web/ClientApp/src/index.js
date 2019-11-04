"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var ReactDOM = require("react-dom");
var App_1 = require("./App");
require("bootstrap/dist/css/bootstrap.min.css");
require("./index.css");
var registerServiceWorker_1 = require("./registerServiceWorker");
ReactDOM.render(React.createElement(App_1.default, null), document.getElementById('root'));
registerServiceWorker_1.default();
//# sourceMappingURL=index.js.map