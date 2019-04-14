class WebApiReactClient{

    constructor(){
        this.baseUri = 'https://localhost:44336/api';
    };

    getCategories() {
        let uri = this.baseUri + '/category';
        return this.getData(uri);
    };

    getProducts() {
        let uri = this.baseUri + '/product';   
        return this.getData(uri);
    };

    getData(uri){
        return fetch(uri).then(res => res.json());
    }
}

export default WebApiReactClient;
