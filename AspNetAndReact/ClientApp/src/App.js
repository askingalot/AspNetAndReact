import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { MovieList } from './components/movies/MovieList';
import { AddMovie } from './components/movies/AddMovie';
import { Counter } from './components/Counter';
import { FetchData } from './components/FetchData';
import { Login } from './components/Login';
import { Register } from './components/Register';
import { Logout } from './components/Logout';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/login' component={Login} />
        <Route path='/register' component={Register} />
        <Route exact path='/movies' component={MovieList} />
        <Route exact path='/movies/new' component={AddMovie} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetch-data' component={FetchData} />
        <Route path='/logout' component={Logout} />
      </Layout>
    );
  }
}
