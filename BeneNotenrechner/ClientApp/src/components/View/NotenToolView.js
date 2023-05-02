import React, { Component } from 'react';
import { NavMenu } from '../NavMenu';

export class NotenToolView extends Component {
    static displayName = NotenToolView.name;

    constructor(props) {
        super(props);
        this.state = {};
    }

    render() {
        return (
            <div>
                <NavMenu></NavMenu>
                <h1>NotenTool</h1>
            </div>
        );
    }
}
