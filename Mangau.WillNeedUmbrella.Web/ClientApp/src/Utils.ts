export const getRequestHeaders = () => {
    const jwtToken = localStorage.jwtToken;

    return {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Authorization': jwtToken ? `Bearer ${jwtToken}` : ''
    }
}

export const hasJwtToken = () => {
    let jwtToken = localStorage.getItem("jwtToken");

    return jwtToken !== undefined && jwtToken !== null && jwtToken.length > 0;
}

export const userLoginFetch = (user: any) => {
    return fetch("api/users/login", {
        method: "POST",
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
        },
        body: JSON.stringify(user)
    })
        .then(resp => resp.json())
        .then(data => {
            if (data.message) {
                // Here you should have logic to handle invalid creation of a user.
                // This assumes your Rails API will return a JSON object with a key of
                // 'message' if there is an error with creating the user, i.e. invalid username
            } else {
                localStorage.setItem("jwtToken", data.token)
                localStorage.setItem("currentUser", data)
            }
            return data;
        })
}

export const userLogoutFetch = () => {
    return fetch("api/users/logout", {
        method: "DELETE",
        headers: getRequestHeaders()
    })
        .then(() => {
            localStorage.removeItem("jwtToken")
            localStorage.removeItem("currentUser")
        })
}

