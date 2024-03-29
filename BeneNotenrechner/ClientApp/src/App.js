import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import { Layout } from './components/Layout';
import './custom.css';
import { Authenticat } from './components/Authenticat';
import { StateProvider } from './components/StateProvider';

export default class App extends Component {
  static displayName = App.name;

    render() {
        return (
        <div>
            <StateProvider>          
                <Authenticat>
                    <Routes>
                        {AppRoutes.map((route, index) => {
                        const { element, ...rest } = route;
                        return <Route key={index} {...rest} element={element} />;
                        })}
                    </Routes>
                </Authenticat>
            </StateProvider>
        </div>
    );
    }
}
