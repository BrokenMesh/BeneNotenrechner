import React, { Component } from 'react';
import { NavMenu } from '../NavMenu';
import { SuperSubject } from '../NotenToolComponents/SuperSubject';
import { MContext } from '../StateProvider';

export class NotenToolView extends Component {
    static displayName = NotenToolView.name;

    constructor(props) {
        super(props);
        this.state = {
            SuperSubjects: [],
            ProfilesLoading: true,
            Profiles: [],
            CurrentProfileIndex: 0,
            SuperSubjectLoading: true,
            NewSuperSubject: {
                name: ""
            }
        };

        this.reload = this.reload.bind(this);
    }

    populateData(context) {
        const fetchSuperSubject = async () => {

            const data = {
                token: context.state.token,
            }

            const result = await fetch('nt/nt_supersubject', {
                method: 'Post',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })

            const superSubjectData = await result.json();

            if (superSubjectData.Error == null) {
                this.setState({ SuperSubjects: superSubjectData, SuperSubjectsLoading: false });
            } else {
                this.setState({ SuperSubjectsLoading: true });
                console.log(superSubjectData.Error);
            }
        }

        const fetchProfil = async () => {

            const data = {
                token: context.state.token,
            }

            const result = await fetch('nt/nt_profile', {
                method: 'Post',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })

            const profileData = await result.json();

            if (profileData.Error == null) {
                this.setState({ Profiles: profileData, ProfilesLoading: false });
            } else {
                this.setState({ ProfilesLoading: true });
                console.log(profileData.Error);
            }
        } 

        fetchSuperSubject();
        fetchProfil();
    }

    createSuperSubject(context, NewSuperSubject) {
        const asCreateSuperSubject = async () => {
            const data = {
                Token: context.state.token,
                Name: NewSuperSubject.name
            }

            const result = await fetch('nt/nt_createsupersubject', {
                method: 'Post',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })

            this.reload(context);
        }

        asCreateSuperSubject();
    }

    updateNewSuperSubjectName(event) {
        let superSubject = this.state.NewSuperSubject;
        superSubject.name = event.target.value;
        this.setState({ NewSuperSubject: superSubject });
    }

    resetNewSuperSubject() {
        this.setState({
            NewSuperSubject: {
                name: "",
            }
        });   
    }

    setProfile(profile, index, context) {
        const asSetProfile = async () => {
            const data = {
                Token: context.state.token,
                Index: profile.Index + "",
            }

            const result = await fetch('nt/nt_profileset', {
                method: 'Post',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })

            this.setState({
                CurrentProfileIndex: index
            });

            this.reload(context);
        }

        asSetProfile();
    }

    reload(contex) {
        this.populateData(contex);
    }

    renderSuperSubjects(superSubjects) {
        return (
            <div className="vstack gap-3">
                {
                    superSubjects.map((superSubject) => {
                        return (
                            <SuperSubject name={superSubject.Name} average={superSubject.Average} id={superSubject.Id} key={superSubject.Id} parent={this} />
                        )
                    })
                }
            </div>
        );
    }

    renderProfiles(profiles, currentProfile , dropdownStyle, context) {

        let _current = '_';

        if (profiles.length > currentProfile) {
            _current = profiles[currentProfile].Index;
        }

        return (
            <div style={dropdownStyle} className="dropdown">
                <button className="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false"> {_current} </button>
                <ul className="dropdown-menu">
                    {
                        profiles.map((_profile, _index) => {
                            return (
                                <li key={_profile.Index}><div onClick={(evt) => { this.setProfile(_profile, _index, context); }} className="dropdown-item">{_profile.Index}</div></li>
                            )
                        })
                    }
                </ul>
            </div> 
        )
    }

    render() {

        const menuStyle = {
            width: "900px",
            marginTop: "100px",
            marginBottom: "100px",
        };

        const dropdownStyle = {
            position: 'relative',
            left: -70,
            top: -10
        };

        return (
            <div>
                <NavMenu />
                <MContext.Consumer>
                    {(context) => {
                        if (this.state.SuperSubjectsLoading === true || this.state.ProfilesLoading === true)
                            this.populateData(context);

                        let superSubjects = this.state.SuperSubjectsLoading
                            ? <p><em>Loading...</em></p>
                            : this.renderSuperSubjects(this.state.SuperSubjects);

                        let profiles = this.state.ProfilesLoading
                            ? <p><em>Loading...</em></p>
                            : this.renderProfiles(this.state.Profiles, this.state.CurrentProfileIndex, dropdownStyle, context);

                        return (
                            <div>
                                <div className="modal fade" id={"notentoolcreate"} tabIndex="-1" aria-labelledby="createmodalLabel" aria-hidden="true">
                                    <div className="modal-dialog modal-dialog-centered ">
                                        <div className="modal-content">
                                            <div className="modal-header">
                                                <h1 className="modal-title fs-5" id="createmodalLabel"> Neues Überfach Hinzufügen </h1>
                                                <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                            </div>
                                            <div className="modal-body">
                                                <form>
                                                    <div className="mb-2">
                                                        <label htmlFor="subject-name" className="col-form-label">Name:</label>
                                                        <input type="text" className="form-control" id="subject-name"
                                                            onChange={(evt) => { this.updateNewSuperSubjectName(evt) }} value={this.state.NewSuperSubject.name} required />
                                                    </div>
                                                </form>
                                            </div>
                                            <div className="modal-footer">
                                                <button type="button" className="btn btn-primary" data-bs-dismiss="modal" onClick={() => this.createSuperSubject(context, this.state.NewSuperSubject)}>Hinzufügen</button>
                                                <button type="button" className="btn btn-secondary" data-bs-dismiss="modal" onClick={() => this.resetNewSuperSubject()}>Abbrechen</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style={menuStyle} className="container-sm ">

                                    {profiles}

                                    {superSubjects}

                                    <div className="text-center p-3">
                                        <button type="button" className=" btn btn-outline-primary" data-bs-toggle="modal" data-bs-target={"#notentoolcreate"}>Hinzufügen</button>
                                    </div>
                                </div>
                            </div>
                        )
                    }}
                </MContext.Consumer>
            </div>
        );
    }
}

