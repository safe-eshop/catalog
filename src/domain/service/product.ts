import {Product, ProductId} from "../model/product";
import {IProductRepository} from "../repository/product";
import {Option} from "fp-ts/lib/Option";

export interface IProductService {
    getById(id: string) : Promise<Option<Product>>
}

export class ProductService implements IProductService {

    /**
     *
     */
    constructor(private repo: IProductRepository) {

    }

    async getById(id: ProductId): Promise<Option<Product>> {
        return this.repo.get(id) ;
    }

}
