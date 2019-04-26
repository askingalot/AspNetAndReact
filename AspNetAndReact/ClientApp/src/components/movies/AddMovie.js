import React, { Component } from 'react';

export class AddMovie extends Component {
    state = {
        title: "",
        year: ""
    };
 
    onFieldChange = (evt) => {
        this.setState({ [evt.target.id]: evt.target.value });
    };

    addMovie = (evt) => {
        evt.preventDefault();

        const movie = {
            title: this.state.title,
            year: parseInt(this.state.year, 10)
        };

        fetch("/api/movies", {
            "method": "POST",
            "headers": {
                "Authorization": `Bearer ${sessionStorage.getItem("jwt_token")}`,
                "Content-Type": "application/json"
            },
            "body": JSON.stringify(movie)
        })
        .then(() => this.props.history.push("/movies"));
    }

    render () {
        return (
            <form onSubmit={this.addMovie}>
                <h1>Add Movie</h1>
                <div className="form-group">
                    <label htmlFor="title">Title:</label>
                    <input id="title" className="form-control"
                        value={this.state.title} onChange={this.onFieldChange} />
                </div>

                <div className="form-group">
                    <label htmlFor="year">Year:</label>
                    <input type="number" id="year" className="form-control"
                        value={this.state.year} onChange={this.onFieldChange} />
                </div>

                <button className="btn btn-primary" onClick={this.addMovie}>
                    Add
                </button>
            </form>
        );
    }
}
