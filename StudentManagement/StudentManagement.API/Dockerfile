# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy các file .csproj vào đúng cấu trúc thư mục để restore (tận dụng cache)
COPY ["StudentManagement.API/StudentManagement.API.csproj", "StudentManagement.API/"]
COPY ["StudentManagement.BLL/StudentManagement.BLL.csproj", "StudentManagement.BLL/"]
COPY ["StudentManagement.DAL/StudentManagement.DAL.csproj", "StudentManagement.DAL/"]
RUN dotnet restore "StudentManagement.API/StudentManagement.API.csproj"

# Copy toàn bộ phần code còn lại của cả 3 project
COPY . .

# Di chuyển con trỏ vào project API và tiến hành Publish
WORKDIR "/app/StudentManagement.API"
RUN dotnet publish "StudentManagement.API.csproj" -c Release -o /app/out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 8080
ENTRYPOINT ["dotnet", "StudentManagement.API.dll"]
