import React, { Component } from 'react';

export const MContext = React.createContext(); 

export class StateProvider extends Component {
    state = { token: "", isAuthenticated: false }

    render() {
        return (
            <MContext.Provider value={
                {
                    state: this.state,
                    setToken: (value) => this.setState({
                        token: value
                    }),
                    setAuthenticatedState: (value) => this.setState({
                        isAuthenticated: value
                    }),
                }}>
                {this.props.children}
            </MContext.Provider>)
    }
}