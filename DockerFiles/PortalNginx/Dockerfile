FROM current-alpine3.12
WORKDIR /rl-store/portal
COPY ../../Portal/ .
RUN npm install && npm build dev
WORKDIR /app
COPY ../rl-store/portal/build .

FROM httpd:latest
COPY --from=build /app /usr/local/apache2/htdocs