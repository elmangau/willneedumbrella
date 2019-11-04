import * as React from 'react';
import { BrowserRouter, Switch, Redirect } from "react-router-dom";
import './App.css';
import logo from './logo.svg';

import * as Utils from './Utils';
import { ExtendedRoute } from './ExtendedRoute';

import { Button } from 'react-bootstrap';

import Login from './Login';

class App extends React.Component {
    state = {
        reloadApp: false,
        isAuthenticated: Utils.hasJwtToken(),
    }

    constructor(props: any) {
        super(props);

        this.onLogoutClick = this.onLogoutClick.bind(this);
    }

    public onLogoutClick(event: React.FormEvent): void {
        event.preventDefault();
        Utils.userLogoutFetch()
            .then(() => {
                this.setState(() => ({
                    reloadApp: true,
                    isAuthenticated: false,
                }));
            });
    }

    public render() {
        return (
            <BrowserRouter>
                <div>
                    <div className="App">
                        <header className="App-header">
                            <img src={logo} className="App-logo" alt="logo" />
                            <h1 className="App-title">Welcome to Mangau Will Need Umbrella</h1>
                            {this.state.isAuthenticated ? <Button size="sm" onClick={this.onLogoutClick}>Logout</Button> : <span/>}
                            {this.state.reloadApp ? <Redirect to='/' /> : <span/>}
                        </header>
                    </div>
                    <Switch>
                        <ExtendedRoute exact path="/" component={Home} useAlternatePath={!this.state.isAuthenticated} alternatePath="/login" />
                        <ExtendedRoute exact path="/foo" component={Foo} useAlternatePath={!this.state.isAuthenticated} alternatePath="/login" />
                        <ExtendedRoute exact path="/login" component={Login} withProps={{
                            onLogin: (event: any) => {
                                this.setState(() => ({
                                    reloadApp: false,
                                    isAuthenticated: true,
                                }));
                            }
                        }} />
                    </Switch>
                </div>
            </BrowserRouter>
        );
    }
}

export default App;

export const Home = () => <h1>Home Page</h1>;
export const Foo = () => <h1>Foo Page</h1>;
