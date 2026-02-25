FROM node:20-alpine AS build
WORKDIR /app

COPY package*.json ./
RUN npm install

COPY . .

RUN npm run build -- --configuration production

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

COPY --from=build /app/dist/crud_angular/browser ./

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]

