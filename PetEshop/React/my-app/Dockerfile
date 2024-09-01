# Етап збірки
#FROM node as build
#WORKDIR /app
#COPY . .
#RUN npm instal
#COPY . .
#RUN npm run build
#CMD ["npm", "start"]

FROM node as build
WORKDIR /usr/src/app
COPY . /usr/app
COPY package*.json .
RUN npm install
COPY . .
RUN npm run build

# Стадія продакшну
FROM nginx:alpine
#WORKDIR /usr/share/nginx_react/html
#RUN rm -rf ./*
COPY --from=build /usr/src/app/build /usr/share/nginx/html
COPY nginx_react.conf /etc/nginx/conf.d/default.conf
CMD ["nginx", "-g", "daemon off;"]