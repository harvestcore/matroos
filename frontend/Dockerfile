FROM node:16-alpine as build
ENV NODE_ENV production

WORKDIR /app

COPY package.json ./

RUN npm install

COPY public ./public
COPY src ./src
COPY tsconfig.json ./

RUN npm run build

FROM nginx:stable-alpine

COPY --from=build /app/build /usr/share/nginx/html
COPY nginx/nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]