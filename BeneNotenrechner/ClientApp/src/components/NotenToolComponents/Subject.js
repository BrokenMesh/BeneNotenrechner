import React, { Component } from 'react';
import { Link } from "react-router-dom";
import { MContext } from '../StateProvider';

export class Subject extends Component {
    static displayName = Subject.name;

    constructor(props) {
        super(props);
        this.state = { Subjects: [], Loading: true };
    }

    render() {

        return (
            <div>
                <div style={{ width: 70 }} className="p-2">
                    <div className="row p-1">
                        <button type="button" data-bs-toggle="modal" data-bs-target={"#subject" + this.props.id} className="btn btn-secondary"> {this.props.name} </button>
                    </div>
                    <div className="row p-1">
                        <b className="text-center border rounded p-1">5.5</b>
                    </div>
                </div>

                <div className="modal fade" id={"subject" + this.props.id} tabIndex="-1" aria-labelledby="modalLabel" aria-hidden="true">
                    <div className="modal-dialog modal-dialog-centered ">
                        <div className="modal-content">
                            <div className="modal-header">
                                <h1 className="modal-title fs-5" id="^modalLabel"> {this.props.name}</h1>
                                <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div style={{minHeight: 400}} className="modal-body">
                                
                            </div>
                            <div className="modal-footer">
                                <button type="button" className="btn btn-primary" data-bs-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        );
    }
}
