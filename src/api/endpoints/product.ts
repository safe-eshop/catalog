import {IProductService} from "../../domain/service/product";
import Router from "koa-router";
import {ProductDto} from "../../application/usecase/getProductByIdUseCase";


export function getProductById(getProductById: (id: string) => Promise<ProductDto> ) {
    return (router: Router<any, {}>) => {
        return router.get("/products", async (context, next) => {
            const result = await getProductById("")
        })
    }
}