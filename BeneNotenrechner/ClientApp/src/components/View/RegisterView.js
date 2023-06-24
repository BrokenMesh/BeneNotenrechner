import React, { Component } from 'react';
import { MContext } from '../StateProvider';
import { sha256 } from 'js-sha256'

export class RegisterView extends Component {
    static displayName = RegisterView.name;

    constructor(props) {
        super(props);
        this.state = { formindex: 0, username: '', password: '', usermail: '', error: '', validationcode: '' };
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

    handleValidationcodeChange(event) {
        this.setState({ validationcode: event.target.value });
    }

    handleRegiserteSubmit(event, context) {
        const submitRegisterData = async () => {
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

            console.log(jsonResult);

            this.setState({ error: jsonResult.error });

            if (jsonResult.error !== '') {
                return;
            }

            this.setState({ formindex: 1 });
            this.setState({ validationcode: '' });
        }

        event.preventDefault();

        if (this.state.password.length < 8) {
            this.setState({ error: "Das passwort muss min. 8 Charaktere enthalten!" });
            return;
        }

        submitRegisterData();
    }

    handleValidationcodeSubmit(event, context) {
        const submitRegisterData = async () => {
            const data = {
                Validationcode: this.state.validationcode,
            }

            console.log(data);

            const result = await fetch('validationcode', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })

            const jsonResult = await result.json();

            console.log(jsonResult);

            this.setState({ error: jsonResult.error });

            if (jsonResult.error !== '') {
                return;
            }

            this.setState({ formindex: 2 });
        }

        submitRegisterData();

        event.preventDefault();
    }

    renderRegistrationForm(context) {
        return (
            <div style={{ width: 500 }} className="container-sm position-absolute top-50 start-50 translate-middle">
                <form className="form-control needs-validation" onSubmit={(evt) => { this.handleRegiserteSubmit(evt, context) }}>
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
        )
    }

    renderTokenMailForm(context) {
        return (
            <div style={{ width: 500 }} className="container-sm position-absolute top-50 start-50 translate-middle">
                <form className="form-control needs-validation" onSubmit={(evt) => { this.handleValidationcodeSubmit(evt, context) }}>
                    <h1>E-Mail Authentifizieren</h1>
                    <br></br>
                    <div className="mb-3">
                        <label htmlFor="AuthoCode-input" className="form-label">Authentifizierungs  Code</label>
                        <input type="text" className="form-control" id="AuthoCode-input" required
                            value={this.state.validationcode} onChange={(evt) => { this.handleValidationcodeChange(evt) }}></input>
                    </div>
                    <p style={{ color: 'red' }}>{this.state.error}</p>
                    <br></br>
                    <div class="d-flex justify-content-between">
                        <div className="mb-3">
                            <button type="submit" className="btn btn-primary">Registrieren</button>
                        </div>
                        <div className="mb-3">
                            <div className="btn btn-secondary" onClick={() => { this.setState({ formindex: 0 }); }} >Züruck</div>
                        </div>
                    </div>
                </form>
            </div>
        )
    }

    renderSuccessForm(context) {
        return (
            <div style={{ width: 500 }} className="container-sm position-absolute top-50 start-50 translate-middle">
                <form className="form-control needs-validation" onSubmit={(evt) => { }}>
                    <h1>Konto Erstellt</h1>
                    <br></br>
                    <p><b>Konto wurde erfolgreich erstellt</b></p>
                    <p>Sie könne sich jetzt  anmelden</p>
                    <br></br>
                    <div class="d-flex justify-content-between">
                        <div className="mb-3">
                            <div className="btn btn-primary" onClick={() => { this.props.parent.SetIsRegister(false); }} >Zum Login</div>
                        </div>
                    </div>
                </form>
            </div>
        )
    }

    render() {
        return (
            <MContext.Consumer>
                {(context) => {
                    let content;
                    if (this.state.formindex === 0) content = this.renderRegistrationForm(context);
                    if (this.state.formindex === 1) content = this.renderTokenMailForm(context);
                    if (this.state.formindex === 2) content = this.renderSuccessForm(context);

                    return content;
                }    
                }
            </MContext.Consumer>
        );
    }
}
