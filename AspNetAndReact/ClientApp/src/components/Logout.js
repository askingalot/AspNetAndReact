import React, { Component } from 'react';

export class Logout extends Component {
    componentDidMount() {
        fetch("/api/account/logout").then(() => {
            sessionStorage.removeItem("jwt_token");
            this.props.history.push("/");
        });
    }

    render () {
        return null;
    }
}
