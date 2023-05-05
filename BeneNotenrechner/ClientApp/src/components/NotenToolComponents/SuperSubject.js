import React, { Component } from 'react';
import { Link } from "react-router-dom";

export class SuperSubject extends Component {
    static displayName = SuperSubject.name;

    constructor(props) {
        super(props);
        this.state = {};
    }

    render() {
        return (
            <div>
                <div className="card">
                    <div className="card-header"> {this.props.name} </div>

                    <ul className="list-group list-group-flush">
                        <li className="d-flex gap-3">
                            <div className="p-2 w-100 d-flex gap-3">
                                <div style={{ width: 70 }} className="p-2">
                                    <div className="row p-1"><Link to="/AbsenzenTool" className="btn btn-secondary">ABU</Link></div>
                                    <div className="row p-1"><b className="text-center border rounded p-1">5.5</b></div>
                                </div>
                                <div style={{ width: 70 }} className="p-2">
                                    <div className="row p-1"><Link to="/AbsenzenTool" className="btn btn-secondary">MAT</Link></div>
                                    <div className="row p-1"><b className="text-center border rounded p-1">4.5</b></div>
                                </div>
                                <div style={{ width: 70 }} className="p-2">
                                    <div className="row p-1"><Link to="/AbsenzenTool" className="btn btn-secondary">ENG</Link></div>
                                    <div className="row p-1"><b className="text-center border rounded p-1">5</b></div>
                                </div>
                            </div>
                            <div className="p-2 flex-shrink-1 d-flex gap-3 border-start">
                                <div style={{ width: 70 }} className="p-2">
                                    <div className="row p-1"><div className="btn btn-white" disabled>Schnit</div></div>
                                    <div className="row p-1"><b className="text-center border rounded p-1">5.5</b></div>
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
        );
    }
}
