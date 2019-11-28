import Koa from "koa"
import Router from "koa-router"
import json from "koa-json"
import logger from "koa-logger"
import {getProductById} from "./api/endpoints/product";

const app = new Koa();
const router = new Router();

router.get("/", async (ctx, next) => {
    ctx.body = { message: "Test"};
    await next();
});

const rout = getProductById(id => Promise.resolve({ id: id }));

app.use(json());
app.use(logger());
app.use(router.routes()).use(router.allowedMethods());

app.listen(3000, () => {
    console.log(`Server listening on port ${3000}`);
})