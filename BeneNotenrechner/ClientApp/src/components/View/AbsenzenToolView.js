import React, { Component } from 'react';
import { NavMenu } from '../NavMenu';

export class AbsenzenToolView extends Component {
    static displayName = AbsenzenToolView.name;

    constructor(props) {
        super(props);
        this.state = {};
    }

    render() {
        return (
            <div>
                <NavMenu></NavMenu>
                <h1>AbsenzenToolView</h1>
            </div>
        );
    }
}
