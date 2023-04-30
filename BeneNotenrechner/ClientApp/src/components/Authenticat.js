import React, { Component } from 'react';
import { MContext } from './StateProvider';
import { LoginView } from './View/LoginView';

export class Authenticat extends Component {
    static displayName = Authenticat.name;

    render() {

        let page = this.props.children;


        return (
            <div>
                <MContext.Consumer>
                    {(context) => {
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
