import Koa from "koa"
import json from "koa-json"
import logger from "koa-logger"
import setup from "./api/infrastructure/setup";


const app = new Koa();

setup(app).then(router => {
    //app.use(rout);
    app.use(json());
    app.use(logger());
    app.use(router.routes()).use(router.allowedMethods());

    app.listen(3000, () => {
        console.log(`Server listening on port ${3000}`);
    })
})
