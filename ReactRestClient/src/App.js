import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';
import WebApiReactClient from './WebApiReactClient.js';

class App extends Component {
  constructor(props){
    super(props);
    this.client = new WebApiReactClient(props);
    this.getProducts = this.getProducts.bind(this);
    this.getCategories = this.getCategories.bind(this);
  }

  getCategories(){
    return this.client.getCategories();
  }

    getProducts(event, context){
    return this.client.getProducts();
  }

  render() {
    return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <p>
            Edit <code>src/App.js</code> and save to reload.
          </p>
          <a className="App-link" href="https://reactjs.org" target="_blank" rel="noopener noreferrer">Learn React</a>
          <a className="App-link" href="#" onClick={this.getCategories} target="_blank" rel="noopener noreferrer">Get categories</a>
          <a className="App-link" href="#" onClick={this.getProducts} target="_blank" rel="noopener noreferrer">Get products</a>
        </header>
       
      </div>
    );
  }
}

export default App;
