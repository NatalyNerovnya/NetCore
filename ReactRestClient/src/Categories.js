import React, { Component } from 'react';
import WebApiReactClient from './WebApiReactClient';

class Categories extends Component {
    constructor(props) {
        super(props);
        this.state = { categories: [] }
        this.client = new WebApiReactClient();
    }

    async componentDidMount() {
        const json = await this.client.getCategories();
        this.setState({ categories: json });
    };

    render() {
        return (
            <div>
            <h3>Categories</h3>
                {this.state.categories.map(category =>
                    (
                        <p>CategoryId = {category.id}, CategoryName = {category.name}</p>
                    ))}
            </div>
        );
    }
}

export default Categories;
