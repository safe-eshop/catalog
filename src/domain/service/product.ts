import { Product, zero } from "../model/product";
import {IProductRepository} from "../repository/product";

export interface IProductService {
    getAll(): Promise<Product[]>
    getById(id: string) : Promise<Product>
}

export class ProductService implements IProductService {

    /**
     *
     */
    constructor(private repo: IProductRepository) {

    }

    async getAll(): Promise<Product[]> {
        const res = await this.repo.getAll();
        return res  || [];
    }

    async getById(id: string): Promise<Product> {
        const res = await this.repo.get(id) ;
        return res || zero(id);
    }

}
