# `Wkg.EntityFrameworkCore.MySql` Documentation

`Wkg.EntityFrameworkCore.MySql` only provides the RECAP bindings for MySQL. For more information on how to use the RECAP framework, please refer to the [WKG Entity Framework Core documentation](https://github.com/WKG-Software-GmbH/wkg-entity-framework-core/blob/main/docs/documentation.md).

- [`Wkg.EntityFrameworkCore.MySql` Documentation](#wkgentityframeworkcoremysql-documentation)
  - [Getting Started](#getting-started)
    - [Requirements](#requirements)
    - [Installation](#installation)
  - [Usage](#usage)
    - [Mapping Procedure Command Objects](#mapping-procedure-command-objects)
    - [Features](#features)

> :warning: **Warning**
> This documentation is a work in progress and may not be complete or up-to-date. For the most accurate and up-to-date information, please refer to the source code and the XML documentation comments.

## Getting Started

### Requirements

In order to use the `Wkg.EntityFrameworkCore.MySql` package, you must have the following installed:

- A .NET runtime matching a major version of the `Wkg.EntityFrameworkCore.MySql` package.
- The `Wkg.EntityFrameworkCore` package containing the core components of the RECAP framework. The major and minor versions of the `Wkg.EntityFrameworkCore` and `Wkg.EntityFrameworkCore.MySql` packages must match.

### Installation

Please refer to the installation instructions provided in the [README](../README.md).

## Usage

> :bulb: **Tip**
> It is recommended to read the [WKG Entity Framework Core documentation](https://github.com/WKG-Software-GmbH/wkg-entity-framework-core/blob/main/docs/documentation.md) before using the `Wkg.EntityFrameworkCore.MySql` package.

### Mapping Procedure Command Objects

Mapping RECAP Procedure Command Objects (PCOs) targeting MySQL follows the same pattern described in the [WKG Entity Framework Core documentation](https://github.com/WKG-Software-GmbH/wkg-entity-framework-core/blob/main/docs/documentation.md#getting-started-with-pco-mapping), however, there are some minor differences to be aware of:

- All PCOs targeting MySQL must inherit from the `MySqlStoredProcedure<TIOContainer>` or `MySqlStoredProcedure<TIOContainer, TResult>` base classes, where `TIOContainer` is the type of the Input/Output Container (I/O Container) used by the PCO.
- MySQL does not support `out` parameters in database functions. RECAP will throw an exception if a PCO mapping a MySQL function is configured to use `out` parameters.

### Features

The `Wkg.EntityFrameworkCore.MySql` package provides the following additional features:

- **Boolean support for MySQL** - When mapping I/O Containers with boolean properties to numeric MySQL parameters, RECAP will automatically convert the boolean values to the corresponding numeric values (0 for `false`, everything else for `true`). The following configuration demonstrates a working example:

    ```csharp
    public record MyIOContainer(bool MyBoolean);
    ...
    self.Parameter(x => x.MyBoolean)
        .HasParameterName("my_boolean")
        .HasDirection(...)
    ```
    
    In above example, the `MyBoolean` property will be mapped to a MySQL parameter named `my_boolean` of type `TINYINT(1)`. Input parameters will automatically be converted to the corresponding numeric value (0 for `false`, 1 for `true`), and output parameters will automatically be converted to the corresponding boolean value (`false` for 0, `true` for everything else).
- **JSON support** - When mapping JSON columns of a result set, RECAP can automatically deserialize the JSON data into a .NET object for you. The following example demonstrates the usage of the `GetAsJson()` extension method in `MySqlResultColumnBuilderExtensions`:

    ```csharp
    public record MyJsonModel(int Id, string Name);
    public record MyResult(MyJsonModel? JsonModel, string SomeOtherProperty);
    ...
    resultBuilder.Column(result => result.JsonModel)
        .HasName("my_json_column")
        .MayBeNull()    // enables null value awareness for the column
        .GetAsJson();   // tells RECAP to deserialize the JSON data into the CLR type of the property
    ```
    
    In above example, the `JsonModel` property will be mapped to a JSON column named `my_json_column` of MySQL type `JSON`. If the JSON column contains a `null` value, the `JsonModel` property will be set to `null`. If the JSON column contains a JSON object, the JSON object will be deserialized into a `MyJsonModel` object and assigned to the `JsonModel` property.

    > :information_source: **Note**
    > RECAP uses `System.Text.Json` to deserialize JSON data. Therefore, you may use the `System.Text.Json.Serialization` attributes to control the deserialization process. For more information, please refer to the [System.Text.Json documentation](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.serialization?view=net-7.0).

    You can also opt to implicitly deserialize the JSON data by mapping a column of type `MySqlDbType.JSON` to a non-string CLR type. The following example demonstrates this:

    ```csharp
    ...
    resultBuilder.Column(result => result.JsonModel)
        .HasName("my_json_column")
        .HasDbType(MySqlDbType.JSON);   // implicitly instructs RECAP to deserialize the JSON data into the CLR type of the property
    ```

    > :x: **Caution**
    > Ensure that the CLR type of the property matches the JSON data in the column. Otherwise, an exception will be thrown at runtime.

    JSON serialization for input parameters must be handled manually. The following example demonstrates JSON serialization for input parameters:

    ```csharp
    public string Invoke(MyJsonModel jsonModel)
    {
        var json = JsonSerializer.Serialize(jsonModel);
        var ioContainer = new MyIOContainer(json);
        var result = Execute(ioContainer);
        return result.AsSingle().SomeOtherProperty;
    }
    ```

    > :pray: **Feature Request**
    > Feel free to open Merge Requests to add support for JSON serialization of input parameters :eyes:.
    
- **Support for MySQL datatypes** - The `MySqlResultColumnBuilderExtensions` class provides exhaustive support for MySQL datatypes including `MySqlDecimal`, `MySqlDateTime`, and spacial types represented by `MySqlGeometry`. PCOs can map these datatypes to parameters as usual via the `HasDbType()` method.
- **Conditional Entity Discovery** - If you are using multiple database providers in the same project, you can decorate `IReflectiveModelConfiguration<>` implementations with the `MySqlModelAttribute` to restrict entity discovery to MySQL only. Subsequently pass the attribute type to the `LoadReflectiveModels<T>()` method:
  
    ```csharp
    public class MySqlDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.LoadReflectiveModels<MySqlModel>();
        }
    }
    ```
    
    In above example, only entities decorated with the `MySqlModelAttribute` will be discovered by RECAP. All other entities will be ignored.
- **Automatic type inference** - RECAP's MySQL provider also supports automatic database type inference if `HasDbType()` is omitted when mapping PCO parameters. In these cases RECAP tries to determine the corresponding database type based on the CLR type of the I/O Container property being mapped. For a full list of supported types, please refer to the [`MySqlTypeMap` class](../Wkg.EntityFrameworkCore.MySql/Wkg.EntityFrameworkCore.MySql/MySqlTypeMap.cs).