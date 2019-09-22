import { IProductService } from "../../domain/service/product"
import { Product } from "../../domain/model/product"
import { IProductRepository } from "../../domain/repository/product";


export class ProductService implements IProductService {

    /**
     *
     */
    constructor(private repo: IProductRepository) {
        
    }

    getAll(): Product[] {
        const res = this.repo.getAll();
        if(res === null) {
            return [];
        }
        return [];
    }

}