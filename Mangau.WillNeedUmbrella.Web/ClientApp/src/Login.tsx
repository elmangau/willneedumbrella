import './Login.css';
import * as React from 'react';
import { useState } from 'react';

import * as Utils from './Utils';
import { Form, Button, FormGroup, FormControl, FormLabel } from "react-bootstrap";
import { Redirect, RouteComponentProps } from 'react-router';

export interface LoginProps extends RouteComponentProps<any> {
    onLogin: (event: any) => void;
}

const Login = ({ onLogin }: LoginProps) => {
    const [username, setUsername] = useState<string>("");
    const [password, setPassword] = useState<string>("");
    //const [success, setSuccess] = useState<boolean>(Utils.hasJwtToken());

    function validateForm(): boolean {
        return username.length > 0 && password.length > 0;
    }

    function onLoginClick(event: React.FormEvent) {
        event.preventDefault();
        Utils.userLoginFetch({ 'username': username, 'password': password })
            .then((data: any) => {
                onLogin({});
                //setSuccess(true);
            });
    }

    return (Utils.hasJwtToken() ? <Redirect to="/" /> :
        <div className="Login">
            <Form onSubmit={onLoginClick}>
                <FormGroup controlId="username">
                    <FormLabel>User Name</FormLabel>
                    <FormControl
                        autoFocus
                        type="text"
                        value={username}
                        onChange={(e: any) => setUsername(e.target.value)}
                    />
                </FormGroup>
                <FormGroup controlId="password">
                    <FormLabel>Password</FormLabel>
                    <FormControl
                        value={password}
                        onChange={(e: any) => setPassword(e.target.value)}
                        type="password"
                    />
                </FormGroup>
                <Button block disabled={!validateForm()} type="submit">
                    Login
                </Button>
            </Form>
        </div>
    );
}

export default Login;
