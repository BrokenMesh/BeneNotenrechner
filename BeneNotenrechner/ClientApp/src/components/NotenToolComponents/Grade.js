import React, { Component } from 'react';
import { Link } from "react-router-dom";
import { MContext } from '../StateProvider';

export class Grade extends Component {
    static displayName = Grade.name;

    constructor(props) {
        super(props);
        this.state = { Grades: [], Loading: true };
    }

    render() {

        return (
            <tr>
                <td>{this.props.name}</td>
                <td>{this.props.grade_value}</td>
                <td>{this.props.date}</td>
                <td>
                    <button type="button" className="btn btn-secondary btn-sm m-1">Edit</button>
                </td>
            </tr>
        );
    }
}
