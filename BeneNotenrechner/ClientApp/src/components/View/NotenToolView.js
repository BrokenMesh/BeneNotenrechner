import React, { Component } from 'react';
import { NavMenu } from '../NavMenu';
import { SuperSubject } from '../NotenToolComponents/SuperSubject';
import { MContext } from '../StateProvider';

export class NotenToolView extends Component {
    static displayName = NotenToolView.name;

    constructor(props) {
        super(props);
        this.state = { SuperSubjects: [], Loading: true };
    }

    populateData(context) {
        const fetchdata = async () => {

            const data = {
                token: context.state.token,
                profile: "0"
            }

            const result = await fetch('nt/nt_supersubject', {
                method: 'Post',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })

            const superSubjectData = await result.json();

            console.log(superSubjectData);

            this.setState({ SuperSubjects: superSubjectData, Loading: false });
        }

        fetchdata();
    }

    renderSuperSubjects(superSubjects) {
        return (
            <div className="vstack gap-3">
                {
                    superSubjects.map((superSubject) => {
                        return (
                            <SuperSubject name={superSubject.Name} semester={superSubject.Semester} id={superSubject.Id} key={superSubject.Id} />
                        )
                    })
                }
            </div>
        );
    }


    render() {

        const menuStyle = {
            width: "900px",
            marginTop: "100px",
            marginBottom: "100px",
        };

        let contents = this.state.Loading
            ? <p><em>Loading...</em></p>
            : this.renderSuperSubjects(this.state.SuperSubjects);

        return (
            <div>
                <MContext.Consumer>
                    {(context) => {
                        if(this.state.Loading == true)
                            this.populateData(context);
                        return (<div></div>)
                    }}
                </MContext.Consumer>
                <NavMenu />

                <div style={menuStyle} className="container-sm">
                    {contents}
                </div>
            </div>
        );
    }
}

