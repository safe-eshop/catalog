import { Product } from "../model/product";

export interface IProductService {
    getAll(): Product[]
}