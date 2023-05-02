import React, { Component } from 'react';
import { MContext } from '../StateProvider';
import { sha256 } from 'js-sha256'

export class LoginView extends Component {
    static displayName = LoginView.name;

    constructor(props) {
        super(props);
        this.state = { username: '', password: '', error: '' };

        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handlePasswordChange(event) {
        this.setState({ password: event.target.value });
    }

    handleUsernameChange(event) {
        this.setState({ username: event.target.value });
    }

    handleSubmit(event, context) {
        const submitUserData = async () => {
            const password_hash = sha256(this.state.password);


            const data = {
                username: this.state.username,
                password: password_hash
            } 

            console.log(data);

            const result = await fetch('logincall', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })

            const jsonResult = await result.json();

            this.setState({ error: jsonResult.error });

            console.log(jsonResult);

            if (jsonResult.token != "") {
                context.setToken(jsonResult.token);
                context.setAuthenticatedState(true);
            }
        }

        event.preventDefault();
        submitUserData();
    }

    render() {
        return (
            <MContext.Consumer>
                {(context) => (
                    <div style={{ width: 500 }} className="container-sm position-absolute top-50 start-50 translate-middle">
                        <form className="form-control needs-validation" onSubmit={(evt) => { this.handleSubmit(evt, context) }}>
                            <h1>Login</h1>
                            <br></br>
                            <div className="mb-3">
                                <label htmlFor="username-input" className="form-label">Username</label>
                                <input type="text" className="form-control" id="username-input" required
                                    value={this.state.username} onChange={(evt) => { this.handleUsernameChange(evt) }}></input>
                                <div className="invalid-feedback">
                                    Please choose a username.
                                </div>
                            </div>
                            <div className="mb-3">
                                <label htmlFor="password-input" className="form-label">Password</label>
                                <input type="password" className="form-control" id="password-input" aria-describedby="passwordHelp" required
                                    value={this.state.password} onChange={(evt) => { this.handlePasswordChange(evt) }}></input>
                                <div id="passwordHelp" className="form-text">The password will not be encrypted, never use a password the is used in other services. </div>
                            </div>
                            <p style={{ color: 'red' }}>{this.state.error}</p>
                            <br></br>
                            <button type="submit" className="btn btn-primary">Submit</button>
                        </form>
                    </div>
                )}
            </MContext.Consumer>
        );
    }
}
