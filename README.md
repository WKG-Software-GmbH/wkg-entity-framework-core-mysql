# WKG Entity Framework Core - MySQL Bindings

[![NuGet](https://img.shields.io/badge/NuGet-555555?style=for-the-badge&logo=nuget)![NuGet version (Wkg.EntityFrameworkCore.MySql)](https://img.shields.io/nuget/v/Wkg.EntityFrameworkCore.MySql.svg?style=for-the-badge&label=Wkg.EntityFrameworkCore.MySql)![NuGet downloads (Wkg.EntityFrameworkCore.MySql)](https://img.shields.io/nuget/dt/Wkg.EntityFrameworkCore.MySql?style=for-the-badge)](https://www.nuget.org/packages/Wkg.EntityFrameworkCore.MySql/)

---

`Wkg.EntityFrameworkCore.MySql` is a company-internal library that provides MySQL bindings for the Reflective Entity Configuration and Procedure mapping framework (RECAP) provided by the [WKG Entity Framework Core](https://github.com/WKG-Software-GmbH/wkg-entity-framework-core) package. This library is required to use the procedure mapping features of the RECAP framework with MySQL databases.

As part of our commitment to open-source software, we are making this library [available to the public](https://github.com/WKG-Software-GmbH/wkg-entity-framework-core-mysql) under the GNU General Public License v3.0. We hope that it will be useful to other developers and that the community will contribute to its further development.

> :warning: **Warning**
> This package requires the [WKG Entity Framework Core](https://github.com/WKG-Software-GmbH/wkg-entity-framework-core) package to be installed in your project.

## Installation

Install the `Wkg.EntityFrameworkCore.MySql` package by adding the following package references to your project file:

```xml
<ItemGroup>
    <PackageReference Include="Wkg.EntityFrameworkCore" Version="X.Y.ZA" />
    <PackageReference Include="Wkg.EntityFrameworkCore.MySql" Version="X.Y.ZB" />
</ItemGroup>
```

> :warning: **Warning**
> Replace `X.Y.Z[AB]` with the latest stable version available on the [nuget feed](https://www.nuget.org/packages/Wkg.EntityFrameworkCore.MySql/), where **the major version must match the major version of your targeted .NET runtime**.

You can find the latest stable version of `Wkg.EntityFrameworkCore` below:

[![GitHub (Wkg.EntityFrameworkCore)](https://img.shields.io/badge/GitHub-WKG_Entity_Framework_Core-blue?style=flat-square&logo=github)](https://github.com/WKG-Software-GmbH/wkg-entity-framework-core)[![NuGet version (Wkg.EntityFrameworkCore)](https://img.shields.io/nuget/v/Wkg.EntityFrameworkCore.svg?style=flat-square&logo=nuget&label=&color=555555)](https://www.nuget.org/packages/Wkg.EntityFrameworkCore/)

> :warning: **Warning**
> Ensure that the major and minor versions of the `Wkg.EntityFrameworkCore` and `Wkg.EntityFrameworkCore.MySql` packages match.

## Getting Started

The *WKG Entity Framework Core - MySQL Bindings* library provides the RECAP bindings for MySQL. For more information on how to use the RECAP framework, please refer to the [WKG Entity Framework Core](https://github.com/WKG-Software-GmbH/wkg-entity-framework-core/blob/main/docs/documentation.md) documentation.

For more information on features specific to the MySQL bindings, please refer to the [WKG Entity Framework Core - MySQL Bindings](https://github.com/WKG-Software-GmbH/wkg-entity-framework-core-mysql/blob/main/docs/documentation.md) documentation.
