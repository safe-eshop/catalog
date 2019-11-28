FROM node:latest AS base
WORKDIR /app

FROM node:latest AS build
WORKDIR /app
COPY . .
RUN npm set progress=false && npm config set depth 0
RUN npm install --only=production 
RUN npm run build

FROM base AS final
WORKDIR /app
COPY --from=build /app/node_modules ./node_modules
COPY --from=build /app/lib ./

EXPOSE 3000
ENTRYPOINT node index.js