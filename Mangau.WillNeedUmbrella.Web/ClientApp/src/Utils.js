"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.getRequestHeaders = function () {
    var jwtToken = localStorage.jwtToken;
    return {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Authorization': jwtToken ? "Bearer " + jwtToken : ''
    };
};
exports.hasJwtToken = function () {
    var jwtToken = localStorage.getItem("jwtToken");
    return jwtToken !== undefined && jwtToken !== null && jwtToken.length > 0;
};
exports.userLoginFetch = function (user) {
    return fetch("api/users/login", {
        method: "POST",
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
        },
        body: JSON.stringify(user)
    })
        .then(function (resp) { return resp.json(); })
        .then(function (data) {
        if (data.message) {
            // Here you should have logic to handle invalid creation of a user.
            // This assumes your Rails API will return a JSON object with a key of
            // 'message' if there is an error with creating the user, i.e. invalid username
        }
        else {
            localStorage.setItem("jwtToken", data.token);
            localStorage.setItem("currentUser", data);
        }
        return data;
    });
};
exports.userLogoutFetch = function () {
    return fetch("api/users/logout", {
        method: "DELETE",
        headers: exports.getRequestHeaders()
    })
        .then(function () {
        localStorage.removeItem("jwtToken");
        localStorage.removeItem("currentUser");
    });
};
//# sourceMappingURL=Utils.js.map