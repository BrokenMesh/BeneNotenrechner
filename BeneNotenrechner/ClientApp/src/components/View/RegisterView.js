import React, { Component } from 'react';
import { MContext } from '../StateProvider';
import { sha256 } from 'js-sha256'

export class RegisterView extends Component {
    static displayName = RegisterView.name;

    constructor(props) {
        super(props);
        this.state = { username: '', password: '', usermail: '', error: '' };
    }


    handlePasswordChange(event) {
        this.setState({ password: event.target.value });
    }

    handleUsernameChange(event) {
        this.setState({ username: event.target.value });
    }

    handleUsermailChange(event) {
        this.setState({ usermail: event.target.value });
    }

    handleSubmit(event, context) {
        const submitUserData = async () => {
            const password_hash = sha256(this.state.password);

            const data = {
                username: this.state.username,
                usermail: this.state.usermail,
                password: password_hash,
            }

            console.log(data);

            const result = await fetch('registercall', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })

            const jsonResult = await result.json();

            if (jsonResult.error != null) {
                this.setState({ error: jsonResult.error });
            }

            console.log(jsonResult);
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
                            <h1>Registrieren</h1>
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
                                <label htmlFor="usermail-input" className="form-label">E-Mail</label>
                                <input type="text" className="form-control" id="usermail-input" required
                                    value={this.state.usermail} onChange={(evt) => { this.handleUsermailChange(evt) }}></input>
                                <div className="invalid-feedback">
                                    Please choose a email.
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
                            <div class="d-flex justify-content-between">
                                <div className="mb-3">
                                    <button type="submit" className="btn btn-primary">Registrieren</button>
                                </div>
                                <div className="mb-3">
                                    <div className="btn btn-secondary" onClick={() => { this.props.parent.SetIsRegister(false); }} >Login</div>
                                </div>
                            </div>
                        </form>
                    </div>
                )}
            </MContext.Consumer>
        );
    }
}
