import Router from "koa-router";
import {isSome, Option, toNullable} from "fp-ts/lib/Option";
import {ProductDto} from "../../application/dto/product";
import {ProductsFilter} from "../../infrastructure/repository/product";


export function getProductByIdEndpoint(getById: (id: string) => Promise<Option<ProductDto>> ) {
    return (router: Router<any, {}>) => {
        return router.get("/products/:id", async (context, next) => {
            const result = await getById(context.params.id);
            if (isSome(result)) {
                context.body = toNullable(result);
                context.status = 200;
            } else {
                context.status = 404;
            }
        })
    }
}

export function getProductsEndpoint(get: (filter: ProductsFilter) => Promise<Option<ProductDto[]>> ) {
    return (router: Router<any, {}>) => {
        return router.get("/products", async (context, next) => {
            const result = await get({});
            if (isSome(result)) {
                context.body = toNullable(result);
                context.status = 200;
            } else {
                context.status = 201;
                context.body = [];
            }
        })
    }
}