import {IProductService} from "../../domain/service/product";
import Router from "koa-router";
import {ProductDto} from "../../application/usecase/getProductByIdUseCase";
import {isSome, Option, toNullable} from "fp-ts/lib/Option";


export function getProductById(getProductById: (id: string) => Promise<Option<ProductDto>> ) {
    return (router: Router<any, {}>) => {
        return router.get("/products/:id", async (context, next) => {
            const result = await  getProductById(context.params.id);
            if (isSome(result)) {
                context.body = toNullable(result);
                context.status = 200;
            } else {
                context.status = 404;
            }
            
        })
    }
}