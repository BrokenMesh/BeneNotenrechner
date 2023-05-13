import React, { Component } from 'react';
import { Link } from "react-router-dom";
import { MContext } from '../StateProvider';
import { Subject } from './Subject';

export class SuperSubject extends Component {
    static displayName = SuperSubject.name;

    constructor(props) {
        super(props);
        this.state = {
            Subjects: [],
            Loading: true,
            NewSubject: {
                name: "",   
            }
        };
        this.reload = this.reload.bind(this);
    }

    populateData(context, SuperSubjectId) {
        const fetchdata = async () => {

            const data = {
                token: context.state.token,
                SuperSubjectID: SuperSubjectId + ""
            }

            const result = await fetch('nt/nt_subject', {
                method: 'Post',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })

            const subjectData = await result.json();

            console.log(subjectData);

            if (subjectData.Error == null) {
                this.setState({ Subjects: subjectData, Loading: false });
            }
        }

        fetchdata();
    }

    createSubject(context, SuperSubjectID, NewSubject) {
        const asCreateSubject = async () => {

            const data = {
                Token: context.state.token,
                SuperSubjectID: SuperSubjectID + "",
                Name: NewSubject.name
            }

            const result = await fetch('nt/', {
                method: 'Post',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })

            this.reload(context);
        }

        asCreateSubject();
    }

    updateNewSubjectName(event) {
        const newSubject = this.state.NewSubject;
        newSubject.name = event.target.value;
        this.setState({ NewSubject: newSubject });
    }

    resetNewSubject() {
        this.setState({
            NewSubject: {
                name: "",
            }
        });
    }

    renderSubjects(subjects, SuperSubjectId) {
        return (
            <div className="p-2 w-100 d-flex gap-3">
                {
                    subjects.map((subject) => {
                        return (
                            <Subject name={subject.Name} id={subject.Id} supersubject_id={SuperSubjectId} key={subject.Id} parent={this} />
                        )
                    })
                    
                }

                <div className="p-2">
                    <div className="row p-1">
                        <button type="button" className="btn btn-outline-primary" data-bs-toggle="modal" data-bs-target={"#supersubjectcreate" + this.props.id}> + </button>
                    </div>
                </div>

            </div>
        );
    }

    reload(context) {
        this.populateData(context, this.props.id);
    }

    render() {

        let contents = this.state.Loading
            ? <p><em>Loading...</em></p>
            : this.renderSubjects(this.state.Subjects, this.props.id);

        return (
            <div>
                <MContext.Consumer>
                    {(context) => {
                        if (this.state.Loading == true)
                            this.populateData(context, this.props.id);
                        return (
                            <div className="modal fade" id={"supersubjectcreate" + this.props.id} tabIndex="-1" aria-labelledby="createmodalLabel" aria-hidden="true">
                                <div className="modal-dialog modal-dialog-centered ">
                                    <div className="modal-content">
                                        <div className="modal-header">
                                            <h1 className="modal-title fs-5" id="createmodalLabel"> Neues Fach Hinzufügen </h1>
                                            <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                        </div>
                                        <div className="modal-body">
                                            <form>
                                                <div className="mb-2">
                                                    <label htmlFor="subject-name" className="col-form-label">Name:</label>
                                                    <input type="text" className="form-control" id="subject-name"
                                                        onChange={(evt) => { this.updateNewSubjectName(evt) }} value={this.state.NewSubject.name} required />
                                                </div>
                                            </form>
                                        </div>
                                        <div className="modal-footer">
                                            <button type="button" className="btn btn-primary" data-bs-dismiss="modal" onClick={() => this.createSubject(context, this.props.id, this.state.NewSubject)}>Hinzufügen</button>
                                            <button type="button" className="btn btn-secondary" data-bs-dismiss="modal" onClick={() => this.resetNewSubject() }>Abbrechen</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        )
                    }}
                </MContext.Consumer>

                <div className="card">
                    <div className="card-header"> {this.props.name} </div>
                    
                    <ul className="list-group list-group-flush">
                        <li className="d-flex gap-3">

                            {contents}

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
