import React, { Component } from 'react';
import { Link } from 'react-router-dom';

export class MovieList extends Component {
    state = {
        movies: []
    };

    componentDidMount() {
        fetch("/api/movies", {
            "method": "GET",
            "headers": {
                "Authorization": `Bearer ${sessionStorage.getItem("jwt_token")}`
            }
        })
        .then(resp => resp.json())
        .then(movies => this.setState({ movies }));
    }

    toggleFavorite = (movieId) => {
        const movie = this.state.movies.find(m => m.id === movieId);
        if (movie) {
            movie.isUserFavorite = !movie.isUserFavorite;
            fetch(`/api/movies/${movieId}/`, {
                "method": "PUT",
                "headers": {
                    "Authorization": `Bearer ${sessionStorage.getItem("jwt_token")}`,
                    "Content-Type": "application/json"
                },
                "body": JSON.stringify(movie)
            })

            this.setState({ movies: this.state.movies });
        }
    }

    render () {
        if (!this.state.movies.length) {
            return <p><em>Loading...</em></p>;
        }

        return (
            <div className="container">
                <h1>Movies</h1>
                <Link className="btn btn-primary mb-3 ml-1" to="/movies/new">Add Movie</Link>
                <ul className="list-group">
                    {this.state.movies.map(movie =>
                        <li style={{ "cursor": "pointer" }}
                            key={movie.id} className="list-group-item"
                            onClick={() => this.toggleFavorite(movie.id)}>
                            <span className="float-left">{movie.title} ({movie.year})</span>
                            {movie.isUserFavorite &&
                                <span className="float-right border border-info px-1">&#10003;</span>
                            }
                        </li>
                    )}
                </ul>
            </div>
        );
    }
}
