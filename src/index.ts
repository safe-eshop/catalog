import Koa from "koa"
import Router from "koa-router"
import json from "koa-json"
import logger from "koa-logger"
import {getProductById} from "./api/endpoints/product";
import {MongoProductRepository} from "./infrastructure/repository/product";
import {productModel} from "./infrastructure/model/product";
import {seedDatabaseUseCase} from "./application/usecase/seed";
import getProductByIdUseCase from "./application/usecase/getProductByIdUseCase";
import {ProductService} from "./domain/service/product";
import mongoose from "mongoose";

mongoose.connect('mongodb://mongo:27017/catalog?authSource=admin', {useNewUrlParser: true}).then((_: any) => {
    return seedDatabaseUseCase(new MongoProductRepository(productModel))
}, rejected => {
    console.error(rejected)
    process.exit(-1);
});

const app = new Koa();
const router = new Router({ prefix: process.env.PATH_BASE });

router.get("/", async (ctx, next) => {
    ctx.body = { message: "Test"};
    await next();
});

getProductById(getProductByIdUseCase(new ProductService(new MongoProductRepository(productModel))))(router);
//app.use(rout);
app.use(json());
app.use(logger());
app.use(router.routes()).use(router.allowedMethods());

app.listen(3000, () => {
    console.log(`Server listening on port ${3000}`);
})