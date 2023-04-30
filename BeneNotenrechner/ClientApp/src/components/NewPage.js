import React, { Component } from 'react';

export class NewPage extends Component {
    static displayName = NewPage.name;

    constructor(props) {
        super(props);
        this.state = { };
    }

    render() {
        return (
            <div>
                <h1>This is a new Page</h1>

                <p>Cool</p>
            </div>
        );
    }
}
