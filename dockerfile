# MSSQL Container

FROM mcr.microsoft.com/mssql/server:2019-latest

ENV MSSQL_SA_PASSWORD=\#maslO!23
ENV ACCEPT_EULA=Y
ENV MSSQL_RPC_PORT=135
ENV MSSQL_PID=Developer
ENV MSSQL_TCP_PORT=2137


EXPOSE 2137

# DOCKER RUN -d -p 2137:2137 name

CMD ["/opt/mssql/bin/sqlservr"]