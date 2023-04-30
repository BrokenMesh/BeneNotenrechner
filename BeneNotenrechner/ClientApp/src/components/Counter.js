import React, { Component } from 'react';
import { MContext } from './StateProvider';

export class Counter extends Component {
  static displayName = Counter.name;

  constructor(props) {
    super(props);
  }

  render() {
    return (
      <div>
        <h1>Counter</h1>

        <p>This is a simple example of a React component.</p>

        <MContext.Consumer>
            {(context) => (
                <div>
                    <p aria-live="polite">Current count: <strong>{context.state.count}</strong></p>
                        <button className="btn btn-primary" onClick={() => { context.setCount(context.state.count +1) }}>Increment</button>
                </div>
            )}
            </MContext.Consumer>
      </div>
    );
  }
}
