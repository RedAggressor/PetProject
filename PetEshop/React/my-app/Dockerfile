# Етап збірки
FROM node as build
WORKDIR /app
COPY . .
RUN npm instal
#COPY . .
RUN npm run build
CMD ["npm", "start"]

#FROM node as build
#COPY . /usr/app
#WORKDIR /usr/app
#RUN yarn
#RUN npm run build

# Стадія продакшну
#FROM nginx:alpine
#WORKDIR /usr/share/nginx_react/html
#RUN rm -rf ./*
#COPY --from=build /usr/app/build .
#CMD ["nginx", "-g", "daemon off;"]