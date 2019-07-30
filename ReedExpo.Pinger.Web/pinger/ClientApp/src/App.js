import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { UrlList } from './components/UrlList';


export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
        <Layout>
            <Route exact path='/' component={UrlList} />
      </Layout>
    );
  }
}
