import React, { Component } from 'react';

class WebApiReactClient extends Component{

    constructor(props){
        super(props);
        this.state = { resultData: [] };
        this.baseUri = '';
    };

    getCategories() {
        let uri = this.baseUri + 'categories';
        this.componentDidMount(uri);
        return this.state;
    };

    getProducts() {
        debugger;
        let uri = 'https://baconipsum.com/api/?type=meat-and-filler';
        this.componentDidMount(uri);
        return this.state;
    };

    componentDidMount(){
        fetch('https://baconipsum.com/api/?type=meat-and-filler')
            .then(res => res.json())
            .then(json => this.setState({resultData : json}));
    }
}

export default WebApiReactClient;
