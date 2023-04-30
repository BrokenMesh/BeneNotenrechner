import React, { Component } from 'react';
import { MContext } from './StateProvider';

export class Demo extends Component {
    static displayName = Demo.name;

    constructor(props) {
        super(props);
        this.state = { currentText: 'FUCKKKK' };
    }

    render() {
        return (
            <div>
                <h1>Demo</h1>

                <p>Im trying my best</p>
                <MContext.Consumer>
                {(context) => (
                    <div>
                        <p aria-live="polite">Current Text: <strong>{context.state.message}</strong></p>
                        <input type="input" class="form-control" onChange={(evt) => { context.setMessage(evt.target.value) }} value={context.state.message} ></input>
                    </div>
                )}
                </MContext.Consumer>

            </div> 
        );
    }
}
