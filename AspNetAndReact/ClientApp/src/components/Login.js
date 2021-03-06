import React, { Component } from 'react';

export class Login extends Component {
    state = {
        email: "",
        password: "",
        loading: false
    };
 
    onFieldChange = (evt) => {
        this.setState({ [evt.target.id]: evt.target.value });
    };

    login = (evt) => {
        evt.preventDefault();

        this.setState({ loading: true });

        fetch("/api/account/login", {
            "method": "POST",
            "headers": {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
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
            <form onSubmit={this.login}>
                <h1>Login</h1>
                <div className="form-group">
                    <label htmlFor="email">Email/Username:</label>
                    <input type="email" id="email" className="form-control"
                        value={this.state.email} onChange={this.onFieldChange} />
                </div>

                <div className="form-group">
                    <label htmlFor="password">Password:</label>
                    <input type="password" id="password" className="form-control"
                        value={this.state.password} onChange={this.onFieldChange} />
                </div>

                <button className="btn btn-primary" onClick={this.login} disabled={this.state.loading}>
                    login
                </button>
            </form>
        );
    }
}
