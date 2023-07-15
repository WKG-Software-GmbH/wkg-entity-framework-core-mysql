# WKG Entity Framework Core - MySQL Bindings

![](https://git.wkg.lan/WKG/components/wkg-entity-framework-core-mysql/badges/main/pipeline.svg)

---

`Wkg.EntityFrameworkCore.MySql` is a company-internal library that provides MySQL bindings for the Reflective Entity Configuration and Procedure mapping framework (RECAP) provided by the [WKG Entity Framework Core](https://git.wkg.lan/WKG/components/wkg-entity-framework-core) package.

> :warning: **Warning**
> This package requires the [WKG Entity Framework Core](https://git.wkg.lan/WKG/components/wkg-entity-framework-core) package to be installed in your project.

## Installation

The *WKG Entity Framework Core - MySQL Bindings* library is available as a NuGet package from our internal nuget feed. To install it, add the following package source to your NuGet configuration:

```xml
<PropertyGroup>
    <RestoreAdditionalProjectSources>
        https://baget.wkg.lan/v3/index.json
    </RestoreAdditionalProjectSources>
</PropertyGroup>
```

Then, install the package by adding the following package references to your project file:

```xml
<ItemGroup>
    <PackageReference Include="Wkg.EntityFrameworkCore" Version="X.Y.Z" />
    <PackageReference Include="Wkg.EntityFrameworkCore.MySql" Version="X.Y.Z" />
</ItemGroup>
```

> :warning: **Warning**
> Replace `X.Y.Z` with the latest stable version available on the [nuget feed](https://baget.wkg.lan/packages/wkg.entityframeworkcore.mysql/latest), where **the major version must match the major version of your targeted .NET runtime**.

> :warning: **Warning**
> Ensure that the major and minor versions of the `Wkg.EntityFrameworkCore` and `Wkg.EntityFrameworkCore.MySql` packages match.

## Getting Started

The *WKG Entity Framework Core - MySQL Bindings* library provides the RECAP bindings for MySQL. For more information on how to use the RECAP framework, please refer to the [WKG Entity Framework Core](https://git.wkg.lan/WKG/components/wkg-entity-framework-core) documentation.

For more information on features specific to the MySQL bindings, please refer to the [WKG Entity Framework Core - MySQL Bindings](/docs/documentation.md) documentation.