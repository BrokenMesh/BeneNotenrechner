﻿import React, { Component } from 'react';
import { Link } from "react-router-dom";
import { MContext } from '../StateProvider';
import DatePicker from 'react-datepicker';
import { format } from "date-fns";

export class Grade extends Component {
    static displayName = Grade.name;

    constructor(props) {
        super(props);
        this.state = {
            EditGrade: {
                name: props.name,
                date: new Date(),
                grade: props.grade_value,
                evaluation: props.evaluation,
            }
        };
    }

 
    updateGrade(context, SuperSubjectID, SubjectId, GradeID, Grade) {
        const asUpdateGrade = async () => {

            const _date = format(Grade.date, 'yyyy-MM-dd', { awareOfUnicodeTokens: true })

            const data = {
                Token: context.state.token,
                SubjectID: SubjectId + "",
                SuperSubjectID: SuperSubjectID + "",
                GradeID: GradeID + "",
                Grade_Name: Grade.name + "",
                Grade_Grade: Grade.grade + "",
                Grade_Evaluation: Grade.evaluation + "",
                Grade_Date: _date + ""
            }

            const result = await fetch('nt/nt_updategrade', {
                method: 'Post',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })

            this.props.parent.reload(context);
        }

        asUpdateGrade();
    }

    removeGrade(context, SuperSubjectID, SubjectId, GradeID,) {
        const asRemoveGrade = async () => {

            const data = {
                Token: context.state.token,
                SubjectID: SubjectId + "",
                SuperSubjectID: SuperSubjectID + "",
                GradeID: GradeID + "",
            }

            const result = await fetch('nt/nt_deletegrade', {
                method: 'Post',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })

            this.props.parent.reload(context);
        }

        asRemoveGrade();
    }

    updateEditGradeName(event) {
        const grade = this.state.EditGrade;
        grade.name = event.target.value;
        this.setState({ EditGrade: grade })
    }

    updateEditGradeGrade(event) {
        const value = (isFinite(event.target.value)) ? event.target.value : this.state.EditGrade.grade;
        const grade = this.state.EditGrade;
        grade.grade = value;
        this.setState({ EditGrade: grade })
    }

    updateEditGradeEvaluation(event) {
        const value = (isFinite(event.target.value)) ? event.target.value : this.state.EditGrade.evaluation;
        const grade = this.state.EditGrade;
        grade.evaluation = value;
        this.setState({ EditGrade: grade })
    }

    updateEditGradeDate(value) {
        const grade = this.state.EditGrade;
        grade.date = value;
        this.setState({ EditGrade: grade })
    }

    resetValues() {
        this.setState({
            EditGrade: {
                name: this.props.name,
                date: new Date(),
                grade: this.props.grade_value,
                evaluation: this.props.evaluation
            }
        });
    }

    render() {

        return (

            <MContext.Consumer>
            {(context) => (
                    <div className="modal fade" id={"gradeedit" + this.props.id} tabIndex="-1" aria-labelledby="editgrademodalLabel" aria-hidden="true">
                        <div className="modal-dialog modal-dialog-centered ">
                            <div className="modal-content">
                                <div className="modal-header">
                                    <h1 className="modal-title fs-5" id="editgrademodalLabel"> {this.props.name} Bearbeiten </h1>
                                    <button type="button" className="btn-close" data-bs-dismiss="modal" onClick={() => this.resetValues()} aria-label="Close"></button>
                                </div>
                                <div className="modal-body">
                                    <form>
                                        <div className="mb-2">
                                            <label htmlFor="grade-name" className="col-form-label">Name:</label>
                                            <input type="text" className="form-control" id="grade-name"
                                                onChange={(evt) => { this.updateEditGradeName(evt) }} value={this.state.EditGrade.name} required />
                                        </div>
                                        <div className="mb-2">
                                            <label htmlFor="grade-grade" className="col-form-label">Note:</label>
                                            <input type="text" className="form-control" id="grade-grade"
                                                onChange={(evt) => { this.updateEditGradeGrade(evt) }} value={this.state.EditGrade.grade} required />
                                        </div>
                                        <div className="mb-2">
                                            <label htmlFor="grade-grade" className="col-form-label">Wertung:</label>
                                            <input type="text" className="form-control" id="grade-grade"
                                                onChange={(evt) => { this.updateEditGradeEvaluation(evt) }} value={this.state.EditGrade.evaluation} required />
                                        </div>
                                        <div id="date-picker" className="mb-2 input-with-post-icon datepicker">
                                            <label htmlFor="grade-date" className="col-form-label">Datum:</label>
                                            <DatePicker id="grade-date" selected={this.state.EditGrade.date} onChange={(date) => this.updateEditGradeDate(date)} />
                                        </div>
                                        <br/>
                                        <div className="mb-2">
                                            <button type="button" className="btn btn-danger btn-sm" data-bs-dismiss="modal"
                                                onClick={() => this.removeGrade(context, this.props.supersubjectid, this.props.subjectid, this.props.id)}>
                                                Löschen
                                            </button>
                                        </div>
                                    </form>
                                </div>
                                <div className="modal-footer">
                                    <button type="button" className="btn btn-primary" data-bs-dismiss="modal"
                                        onClick={() => this.updateGrade(context, this.props.supersubjectid, this.props.subjectid, this.props.id, this.state.EditGrade)}>
                                        Bearbeiten
                                    </button>
                                    <button type="button" className="btn btn-secondary" data-bs-dismiss="modal" onClick={() => this.resetValues()}>Abbrechen</button>
                                </div>
                            </div>
                        </div>
                    </div>
            )}
            </MContext.Consumer>
        );
    }
}
