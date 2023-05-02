import React, { Component } from 'react';
import { MContext } from './StateProvider';
import { LoginView } from './View/LoginView';
import { useEffect } from 'react';

export class Authenticat extends Component {
    static displayName = Authenticat.name;

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

    render() {
        let page = this.props.children;

        return (
            <div>

                <MContext.Consumer>
                    {(context) => {

                        setInterval(() => { this.Reauthenticate(context) }, 600_000);

                        if (context.state.isAuthenticated) {
                            return page;
                        } else {
                            return (<LoginView></LoginView>)
                        }
                    }}
                </MContext.Consumer>
            </div>
        );
    }
}
