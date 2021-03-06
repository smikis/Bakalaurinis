﻿using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;

namespace TinkloProblemos.API.Identity
{
    public interface IDatabaseConnectionService : IDisposable
    {
        Task<MySqlConnection> CreateConnectionAsync();
        MySqlConnection CreateConnection();
    }
}