import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { StateProvider } from './StateProvider';

export class Layout extends Component {
  static displayName = Layout.name;

  render() {
    return (
        <div class=".container" data-bs-theme="dark">
          <Container>
            <StateProvider>
              {this.props.children}
            </StateProvider>
          </Container>
      </div>
    );
  }
}
