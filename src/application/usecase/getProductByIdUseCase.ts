import {IProductService} from "../../domain/service/product";


export interface ProductDto {
    readonly id: string
}

export default function (service: IProductService) {
    return (id: string) : Promise<ProductDto> => {
        return service.getById(id);
    }
}