# YEET https://github.com/GaetanoPiazzolla/env-frontend/blob/master/angular-env/Dockerfile
FROM node:18-alpine as build
WORKDIR /app

COPY Frontend/package-lock.json .
COPY Frontend/package.json .
RUN npm ci --legacy-peer-deps -f

COPY Frontend .
RUN npm run build

FROM nginx:1.15
COPY --from=build /app/dist/build/ /usr/share/nginx/html
# COPY --from=build /nginx.conf /etc/nginx/conf.d/default.conf keine ahnung was das soll, macht den build kaputt und funktioniert auch ohne
CMD ["/bin/sh",  "-c",  "envsubst < /usr/share/nginx/html/assets/env.template.js > /usr/share/nginx/html/assets/env.js && exec nginx -g 'daemon off;'"]