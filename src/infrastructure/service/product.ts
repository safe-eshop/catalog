import { IProductService } from "../../domain/service/product"
import { Product } from "../../domain/model/product"
import { IProductRepository } from "../../domain/repository/product";


export class ProductService implements IProductService {

    /**
     *
     */
    constructor(private repo: IProductRepository) {
        
    }

    async getAll(): Promise<Product[]> {
        const res = await this.repo.getAll() || [];
        return res;
    }

}