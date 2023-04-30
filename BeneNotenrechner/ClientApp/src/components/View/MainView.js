import React, { Component } from 'react';
import { MContext } from '../StateProvider';

export class MainView extends Component {
    static displayName = MainView.name;

    constructor(props) {
        super(props);
        this.state = { };
    }

    render() {
        return (
            <div>
                <h1>This is the Main Page</h1>
                <MContext.Consumer>
                    {(context) => (
                        <p>{context.state.token}</p>
                    )}
                </MContext.Consumer>
            </div>
        );
    }
}
