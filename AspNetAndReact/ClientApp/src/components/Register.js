import React, { Component } from 'react';

export class Register extends Component {
    state = {
        firstName: "",
        lastName: "",
        email: "",
        password: ""
    };
 
    onFieldChange = (evt) => {
        this.setState({
            [evt.target.id]: evt.target.value
        });
    };

    register = () => {
        fetch("/api/account/register", {
            "method": "POST",
            "headers": {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                firstName: this.state.firstName,
                lastName: this.state.lastName,
                email: this.state.email,
                password: this.state.password
            })
        })
        .then(resp => resp.text())
        .then(token => sessionStorage.setItem("jwt_token", token))
        .then(() => this.props.history.push("/"));
    };

    render () {
        return (
            <React.Fragment>
                <h1>Register</h1>
                <div className="form-group">
                    <label for="firstName">First Name:</label>
                    <input id="firstName" className="form-control"
                        value={this.state.firstName} onChange={this.onFieldChange} />
                </div>

                <div className="form-group">
                    <label for="lastName">Last Name:</label>
                    <input id="lastName" className="form-control"
                        value={this.state.lastName} onChange={this.onFieldChange} />
                </div>

                <div className="form-group">
                    <label for="email">Email/Username:</label>
                    <input type="email" id="email" className="form-control"
                        value={this.state.email} onChange={this.onFieldChange} />
                </div>

                <div className="form-group">
                    <label for="password">Password:</label>
                    <input type="password" id="password" className="form-control"
                        value={this.state.password} onChange={this.onFieldChange} />
                </div>

                <button className="btn btn-primary" onClick={this.register}>
                    Register
                </button>
            </React.Fragment>
        );
    }
}
