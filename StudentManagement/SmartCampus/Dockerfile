# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy các file .csproj vào đúng cấu trúc thư mục để restore (tận dụng cache)
COPY ["SmartCampus/SmartCampus.csproj", "SmartCampus/"]
COPY ["SmartCampus.BLL/SmartCampus.BLL.csproj", "SmartCampus.BLL/"]
COPY ["SmartCampus.DAL/SmartCampus.DAL.csproj", "SmartCampus.DAL/"]
RUN dotnet restore "SmartCampus/SmartCampus.csproj"

# Copy toàn bộ phần code còn lại của cả 3 project
COPY . .

# Di chuyển con trỏ vào project API và tiến hành Publish
WORKDIR "/app/SmartCampus"
RUN dotnet publish "SmartCampus.csproj" -c Release -o /app/out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 8080
ENTRYPOINT ["dotnet", "SmartCampus.dll"]