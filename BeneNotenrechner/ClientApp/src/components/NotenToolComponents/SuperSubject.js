import React, { Component } from 'react';
import { Link } from "react-router-dom";
import { MContext } from '../StateProvider';
import { Subject } from './Subject';

export class SuperSubject extends Component {
    static displayName = SuperSubject.name;

    constructor(props) {
        super(props);
        this.state = { Subjects: [], Loading: true };
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

            this.setState({ Subjects: subjectData, Loading: false });
        }

        fetchdata();
    }

    renderSubjects(subjects) {
        return (
            <div className="p-2 w-100 d-flex gap-3">
                {
                    subjects.map((subject) => {
                        return (
                            <Subject name={subject.Name} id={subject.Id} key={subject.Id} />
                        )
                    })
                }
            </div>
        );
    }


    render() {

        let contents = this.state.Loading
            ? <p><em>Loading...</em></p>
            : this.renderSubjects(this.state.Subjects);

        return (
            <div>
                <MContext.Consumer>
                    {(context) => {
                        if (this.state.Loading == true)
                            this.populateData(context, this.props.id);
                        return (<div></div>)
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
