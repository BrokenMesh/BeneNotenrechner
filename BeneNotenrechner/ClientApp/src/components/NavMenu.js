import React, { Component } from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';

export class NavMenu extends Component {

    render() {
        return (
            <header style={{ backgroundColor: "#e3f2fd" }}>
                <Navbar className="navbar navbar-expand-lg bg-body-tertiary" container light>
                    <NavbarBrand tag={Link} to="/">Benedict SchoolTool</NavbarBrand>
                    <div className="navbar-nav">
                        <NavItem>
                            <NavLink tag={Link} className="text-black" to="/NotenTool">NotenTool</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink tag={Link} className="text-secondary" to="/" reloadDocument>Abmelden</NavLink>
                        </NavItem>
                    </div>
                </Navbar>
            </header>
        )
    }
}