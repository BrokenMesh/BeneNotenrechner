import React, { Component } from 'react';
import { MContext } from './StateProvider';
import { LoginView } from './View/LoginView';
import { RegisterView } from './View/RegisterView';
import { useEffect } from 'react';

export class Authenticat extends Component {
    static displayName = Authenticat.name;

    constructor(props) {
        super(props);

        this.state = { isRegister: false };

        this.SetisRegister = this.SetIsRegister.bind(this);
    }

    Reauthenticate(context) {
        const submitReauthenticate = async() => {
            const data = {
                token: context.state.token
            }

            const result = await fetch('reauthenticate', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })

            console.log("Send Request");
        }

        if (context.state.isAuthenticated) {
            submitReauthenticate();
        }
    }

    SetIsRegister(_value) {
        this.setState({ isRegister: _value});
    }

    render() {
        let page = this.props.children;

        return (
            <div>

                <MContext.Consumer>
                    {(context) => {

                        setInterval(() => { this.Reauthenticate(context) }, 1000*60*5);

                        if (context.state.isAuthenticated) {
                            return page;
                        } else {

                            let loginview = this.state.isRegister ? <RegisterView parent={this}></RegisterView> : <LoginView parent={this}></LoginView>;

                            return (loginview)
                        }
                    }}
                </MContext.Consumer>
            </div>
        );
    }
}
