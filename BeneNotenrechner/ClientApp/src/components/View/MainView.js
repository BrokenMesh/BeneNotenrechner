import React, { Component } from 'react';
import { Link } from "react-router-dom";
import { NavMenu } from '../NavMenu';

export class MainView extends Component {
    static displayName = MainView.name;

    constructor(props) {
        super(props);
        this.state = { };
    }

    render() {
        return (
            <div>
               <NavMenu></NavMenu>
                <div style={{ width: 800 }} className="container-sm position-absolute top-50 start-50 translate-middle">
                    <div className="row">
                        <div className="col-sm-6 mb-3 mb-sm-0">
                            <div className="card">
                                <div className="card-body">
                                    <h5 className="card-title">NotenTool</h5>
                                    <p className="card-text">Erlaubt das eintragen von Noten und des Berechnen vom Notenschnit.</p>
                                    <Link to="/NotenTool" className="btn btn-primary">Öffnen</Link>
                                </div>
                            </div>
                        </div>
                        <div className="col-sm-6">
                            <div className="card">
                                <div className="card-body">
                                    <h5 className="card-title">AbsenzenTool</h5>
                                    <p className="card-text">Elaubt das eintragen von den Absentzen und das Visualisiren der besuchten Schulzeit.</p>
                                    <Link to="/AbsenzenTool" className="btn btn-primary">Öffnen</Link>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}
