import React, { Component } from 'react';

export class Demo extends Component {
    static displayName = Demo.name;

    constructor(props) {
        super(props);
        this.state = { currentText: 'FUCKKKK' };
        this.incrementCounter = this.incrementCounter.bind(this);
    }

    incrementCounter(evt) {
        const val = evt.target.value;
        this.setState({
            currentText: val
        });
    }

    render() {
        return (
            <div>
                <h1>Demo</h1>

                <p>Im trying my best</p>

                <p aria-live="polite">Current Text: <strong>{this.state.currentText}</strong></p>

                <input type="input" class="form-control" onChange={evt => this.incrementCounter(evt)} value={this.state.currentText}/>
            </div>
        );
    }
}
