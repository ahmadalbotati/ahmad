# Installs the core EF Core SQL Server provider
Install-Package Microsoft.EntityFrameworkCore.SqlServer

# Installs the tools needed to run the 'Scaffold' command
Install-Package Microsoft.EntityFrameworkCore.Tools

# Required for the scaffolding process to work with SQL Server
Install-Package Microsoft.EntityFrameworkCore.Design

Scaffold-DbContext "Server=YOUR_SERVER_NAME;Database=CompanyDB;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutDir Models

using CompanyEFCore.Models;

using (var context = new CompanyContext())
{
    var employees = context.Employees.ToList();
    Console.WriteLine($"Found {employees.Count} employees in the database.");
}