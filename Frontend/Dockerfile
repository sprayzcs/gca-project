# Build
FROM node:18-alpine AS build

WORKDIR /app

COPY Frontend/package-lock.json .
COPY Frontend/package.json .
RUN npm ci -f

COPY Frontend .
RUN npm run build

# Run web server
FROM nginx:latest AS run

COPY --from=build /app/dist/frontend /usr/share/nginx/html

EXPOSE 80
