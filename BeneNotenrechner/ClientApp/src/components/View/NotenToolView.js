import React, { Component } from 'react';
import { Link } from "react-router-dom";
import { NavMenu } from '../NavMenu';

export class NotenToolView extends Component {
    static displayName = NotenToolView.name;

    constructor(props) {
        super(props);
        this.state = {};
    }

    render() {

        const menuStyle = {
            width: "900px",
            marginTop: "100px",
            marginBottom: "100px",
        };

        return (
            <div>
                <NavMenu></NavMenu>

                <div style={menuStyle} className="container-sm">
                    <div className="vstack gap-3">

                        <div className="card">
                            <div className="card-header"> Allgemeine  Fächer </div>

                            <ul className="list-group list-group-flush">
                                <li className="d-flex gap-3">
                                    <div className="p-2 w-100 d-flex gap-3">
                                        <div style={{ width: 70 }} class="p-2">
                                            <div className="row p-1"><Link to="/AbsenzenTool" className="btn btn-secondary">ABU</Link></div>
                                            <div className="row p-1"><b className="text-center border rounded p-1">5.5</b></div>
                                        </div>
                                        <div style={{ width: 70 }} class="p-2">
                                            <div className="row p-1"><Link to="/AbsenzenTool" className="btn btn-secondary">MAT</Link></div>
                                            <div className="row p-1"><b className="text-center border rounded p-1">4.5</b></div>
                                        </div>
                                        <div style={{ width: 70 }} class="p-2">
                                            <div className="row p-1"><Link to="/AbsenzenTool" className="btn btn-secondary">ENG</Link></div>
                                            <div className="row p-1"><b className="text-center border rounded p-1">5</b></div>
                                        </div>
                                    </div>
                                    <div className="p-2 flex-shrink-1 d-flex gap-3 border-start">
                                        <div style={{ width: 70 }} class="p-2">
                                            <div className="row p-1"><div className="btn btn-white" disabled>Schnit</div></div>
                                            <div className="row p-1"><b className="text-center border rounded p-1">5.5</b></div>
                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </div>


                        <div className="card">
                            <div className="card-header"> Informatik Fächer </div>

                            <ul className="list-group list-group-flush">
                                <li className="d-flex gap-3">
                                    <div className="p-2 w-100 d-flex gap-3">
                                        <div style={{ width: 70 }} class="p-2">
                                            <div className="row p-1"><Link to="/AbsenzenTool" className="btn btn-secondary">M122</Link></div>
                                            <div className="row p-1"><b className="text-center border rounded p-1">5.5</b></div>
                                        </div>
                                        <div style={{ width: 70 }} class="p-2">
                                            <div className="row p-1"><Link to="/AbsenzenTool" className="btn btn-secondary">M117</Link></div>
                                            <div className="row p-1"><b className="text-center border rounded p-1">4.5</b></div>
                                        </div>
                                        <div style={{ width: 70 }} class="p-2">
                                            <div className="row p-1"><Link to="/AbsenzenTool" className="btn btn-secondary">M293</Link></div>
                                            <div className="row p-1"><b className="text-center border rounded p-1">5</b></div>
                                        </div>
                                        <div style={{ width: 70 }} class="p-2">
                                            <div className="row p-1"><Link to="/AbsenzenTool" className="btn btn-secondary">M390</Link></div>
                                            <div className="row p-1"><b className="text-center border rounded p-1">5</b></div>
                                        </div>
                                        <div style={{ width: 70 }} class="p-2">
                                            <div className="row p-1"><Link to="/AbsenzenTool" className="btn btn-secondary">M164</Link></div>
                                            <div className="row p-1"><b className="text-center border rounded p-1">5</b></div>
                                        </div>
                                        <div style={{ width: 70 }} class="p-2">
                                            <div className="row p-1"><Link to="/AbsenzenTool" className="btn btn-secondary">M329</Link></div>
                                            <div className="row p-1"><b className="text-center border rounded p-1">5</b></div>
                                        </div>
                                        <div style={{ width: 70 }} class="p-2">
                                            <div className="row p-1"><Link to="/AbsenzenTool" className="btn btn-secondary">M122</Link></div>
                                            <div className="row p-1"><b className="text-center border rounded p-1">5</b></div>
                                        </div>                                     
                                    </div>
                                    <div className="p-2 flex-shrink-1 d-flex gap-3 border-start">
                                        <div style={{ width: 70 }} class="p-2">
                                            <div className="row p-1"><div className="btn btn-white" disabled>Schnit</div></div>
                                            <div className="row p-1"><b className="text-center border rounded p-1">5.5</b></div>
                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </div>

                    </div>
                </div>
            </div>
        );
    }
}
