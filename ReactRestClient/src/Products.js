import React, { Component } from 'react';
import WebApiReactClient from './WebApiReactClient';

class Products extends Component {
    constructor(props) {
        super(props);
        this.state = { products: [] }
        this.client = new WebApiReactClient();
    }

    async componentDidMount() {
        const json = await this.client.getProducts();
        this.setState({ products: json });
    };

    render() {
        return (
            <div>
            <h3>Products</h3>
                {this.state.products.map(product =>
                    (
                        <p>ProductUd = {product.id}, ProductName = {product.name}, SupplierName = {product.supplierName}</p>
                    ))}
            </div>
        );
    }
}

export default Products;
