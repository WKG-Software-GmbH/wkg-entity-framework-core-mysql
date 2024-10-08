﻿using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Runtime.CompilerServices;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.Output;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler.Output;

internal class MySqlCompiledResult(bool isCollection, CompiledResultFactory<MySqlDataReader> resultFactory) 
    : CompiledResult<MySqlDataReader>(isCollection, resultFactory)
{
    public override object ReadFrom(DbDataReader reader) => 
        CompiledResultFactory.Invoke(Unsafe.As<MySqlDataReader>(reader));
}
