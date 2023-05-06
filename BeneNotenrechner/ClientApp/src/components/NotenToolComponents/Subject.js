import React, { Component } from 'react';
import { Link } from "react-router-dom";
import { MContext } from '../StateProvider';
import { Grade } from './Grade';

export class Subject extends Component {
    static displayName = Subject.name;

    constructor(props) {
        super(props);
        this.state = { Grades: [], Loading: true };
    }

    populateData(context, SubjectId, SuperSubjectID) {
        const fetchdata = async () => {

            const data = {
                token: context.state.token,
                SuperSubjectID: SuperSubjectID + "",
                SubjectID: SubjectId + ""
            }

            const result = await fetch('nt/nt_grade', {
                method: 'Post',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })

            const gradeData = await result.json();

            console.log(gradeData);

            this.setState({ Grades: gradeData, Loading: false });
        }

        fetchdata();
    }

    updateSubject(context, SubjectId, name) {
        const asUpdateSubject = async () => {

            const data = {
                token: context.state.token,
                SubjectID: SubjectId + "",
                SubjectID: name
            }

            const result = await fetch('nt/nt_grade', {
                method: 'Post',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })
        }

        asUpdateSubject();
    }

    renderGrades(grades) {
        return (
            <table className="table table-striped">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Bewertung</th>
                        <th>Datum</th>
                        <th></th>
                    </tr>
                </thead>
                 <tbody className="">
                    {
                        grades.map((grade) => {
                            return (
                                <Grade name={grade.name} grade_value={grade.Grade} date={grade.Date} id={grade.Id} key={grade.Id} />
                            )
                        })
                    }
                </tbody>
            </table>
        );
    }

    render() {

        let contents = this.state.Loading
            ? <p><em>Loading...</em></p>
            : this.renderGrades(this.state.Grades);

        return (
            <div>
                <MContext.Consumer>
                    {(context) => {
                        if (this.state.Loading == true)
                            this.populateData(context, this.props.id, this.props.supersubject_id);
                        return (<div></div>)
                    }}
                </MContext.Consumer>

                <div style={{ width: 70 }} className="p-2">
                    <div className="row p-1">
                        <button type="button" data-bs-toggle="modal" data-bs-target={"#subject" + this.props.id} className="btn btn-primary"> {this.props.name} </button>
                    </div>
                    <div className="row p-1">
                        <b className="text-center border rounded p-1">5.5</b>
                    </div>
                </div>

                <div className="modal fade" id={"subject" + this.props.id} tabIndex="-1" aria-labelledby="subjectmodalLabel" aria-hidden="true">
                    <div className="modal-dialog modal-dialog-centered ">
                        <div className="modal-content">
                            <div className="modal-header">
                                <h1 className="modal-title fs-5" id="^subjectmodalLabel"> {this.props.name}</h1>
                                <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div style={{minHeight: 400}} className="modal-body">
                                <h4>Prüfungen:</h4>
                                <br/>
                                {contents}

                            </div>
                            <div className="modal-footer">
                                <button type="button" data-bs-toggle="modal" data-bs-target={"#subjectedit" + this.props.id} className="btn btn-secondary">Fach Bearbeiten</button>
                            </div>
                        </div>
                    </div>
                </div>

                <div className="modal fade" id={"subjectedit" + this.props.id} tabIndex="-1" aria-labelledby="editmodalLabel" aria-hidden="true">
                    <div className="modal-dialog modal-dialog-centered ">
                        <div className="modal-content">
                            <div className="modal-header">
                                <h1 className="modal-title fs-5" id="editmodalLabel"> {this.props.name} Bearbeiten</h1>
                                <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div className="modal-body"> 
                                <form>
                                    <div className="mb-3">
                                        <label for="subject-name" className="col-form-label">Name:</label>
                                        <input type="text" className="form-control" id="subject-name" value={this.props.name} />
                                    </div>
                                    <br/>
                                    <div className="mb-3">
                                        <button type="button" className="btn btn-danger btn-sm">Fach Löschen</button>
                                    </div>
                                </form>
                            </div>
                            <div className="modal-footer">
                                <button type="button" className="btn btn-primary" data-bs-dismiss="modal" >Anwenden</button>
                                <button type="button" className="btn btn-secondary" data-bs-dismiss="modal" >Abbrechen</button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        );
    }
}
