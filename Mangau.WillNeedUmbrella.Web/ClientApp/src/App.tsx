import * as React from 'react';
import { BrowserRouter as Router, Route, Link, Switch } from "react-router-dom";
import './App.css';
import logo from './logo.svg';

class App extends React.Component {
    public render() {
        return (
            <Router>
                <div>
                    <div className="App">
                        <header className="App-header">
                            <img src={logo} className="App-logo" alt="logo" />
                            <h1 className="App-title">Welcome to Mangau Will Need Umbrella</h1>
                        </header>
                    </div>
                    <nav>
                        <Link to="/">Home</Link>
                        <Link to="/foo">Foo</Link>
                        <Link to="/bar">Bar</Link>
                    </nav>
                    <Switch>
                        <Route exact path="/" component={Home} />
                        <Route exact path="/foo" component={Foo} />
                        <Route exact path="/bar" component={Bar} />
                    </Switch>
                </div>
            </Router>
        );
    }
}

export default App;

export const Home = () => <h1>Home Page</h1>;
export const Foo = () => <h1>Foo Page</h1>;
export const Bar = () => <h1>Bar Page</h1>;
