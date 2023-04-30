import React, { Component } from 'react';

export const MContext = React.createContext(); 

export class StateProvider extends Component {
    state = { message: "", count: 1, isAuthenticated: false }

    render() {
        return (
            <MContext.Provider value={
                {
                    state: this.state,
                    setMessage: (value) => this.setState({
                        message: value
                    }),
                    setCount: (value) => this.setState({
                        count: value
                    })
                }}>
                {this.props.children}
            </MContext.Provider>)
    }
}