# 员工管理 Web API（Employee Management API）

基于 .NET 8 + ASP.NET Core 的员工管理后端服务，采用三层架构，使用 EF Core 操作 SQL Server。

## 技术栈
- .NET 8 / ASP.NET Core Web API
- Entity Framework Core 8 + LINQ
- SQL Server (LocalDB)
- Swagger / OpenAPI

## 功能
- 员工分页查询、按姓名模糊搜索
- 查询单个员工（含部门名）
- 新增 / 修改 / 删除员工
- 部门与员工一对多关联
- DTO 参数校验、统一返回格式、全局异常处理中间件

## 架构
Controller（表现层）→ Service（业务层）→ Repository（数据层）→ EF Core → 数据库，层与层通过接口 + 依赖注入解耦。

## 本地运行
```bash
dotnet restore
dotnet ef database update
dotnet run