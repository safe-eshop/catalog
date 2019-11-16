import {IProductService} from "../../domain/service/product";


export default function (service: IProductService) {
    return (id: string) => {
        return service.getById(id);
    }
}