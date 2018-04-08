using Dapper;
using Microsoft.AspNetCore.Identity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TinkloProblemos.API.Identity.Entities;

namespace Daarto.IdentityProvider.Tables
{
    public class RolesTable
    {
        private MySqlConnection _sqlConnection;

        public RolesTable(MySqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            const string command = "INSERT INTO Roles " +
                                   "VALUES (@Id, @ConcurrencyStamp, @Name, @NormalizedName);";

            int rowsInserted = Task.Run(() => _sqlConnection.ExecuteAsync(command, new
            {
                role.Id,
                role.ConcurrencyStamp,
                role.Name,
                role.NormalizedName
            }), cancellationToken).Result;

            return Task.FromResult(rowsInserted.Equals(1) ? IdentityResult.Success : IdentityResult.Failed(new IdentityError
            {
                Code = string.Empty,
                Description = $"The role with name {role.Name} could not be inserted in the Roles table."
            }));
        }

        public Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            const string command = "UPDATE Roles " +
                                   "SET ConcurrencyStamp = @ConcurrencyStamp, Name = @Name, NormalizedName = @NormalizedName " +
                                   "WHERE Id = @Id;";

            int rowsUpdated = Task.Run(() => _sqlConnection.ExecuteAsync(command, new
            {
                role.ConcurrencyStamp,
                role.Name,
                role.NormalizedName,
                role.Id
            }), cancellationToken).Result;

            return Task.FromResult(rowsUpdated.Equals(1) ? IdentityResult.Success : IdentityResult.Failed(new IdentityError
            {
                Code = string.Empty,
                Description = $"The role with name {role.Name} could not be updated in the Roles table."
            }));
        }

        public Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            const string command = "DELETE " +
                                   "FROM Roles " +
                                   "WHERE Id = @Id;";

            int rowsDeleted = Task.Run(() => _sqlConnection.ExecuteAsync(command, new { role.Id }), cancellationToken).Result;

            return Task.FromResult(rowsDeleted.Equals(1) ? IdentityResult.Success : IdentityResult.Failed(new IdentityError
            {
                Code = string.Empty,
                Description = $"The role with name {role.Name} could not be deleted from the Roles table."
            }));
        }

        public Task<ApplicationRole> FindByIdAsync(Guid roleId)
        {
            const string command = "SELECT * " +
                                   "FROM Roles " +
                                   "WHERE Id = @Id;";

            return _sqlConnection.QuerySingleOrDefaultAsync<ApplicationRole>(command, new
            {
                Id = roleId
            });
        }

        public Task<ApplicationRole> FindByNameAsync(string normalizedRoleName)
        {
            const string command = "SELECT * " +
                                   "FROM Roles " +
                                   "WHERE NormalizedName = @NormalizedName;";

            return _sqlConnection.QuerySingleOrDefaultAsync<ApplicationRole>(command, new
            {
                NormalizedName = normalizedRoleName
            });
        }

        public Task<IEnumerable<ApplicationRole>> GetAllRoles()
        {
            const string command = "SELECT * " +
                                   "FROM Roles;";

            return _sqlConnection.QueryAsync<ApplicationRole>(command);
        }

        public void Dispose()
        {
            if (_sqlConnection == null)
            {
                return;
            }

            _sqlConnection.Dispose();
            _sqlConnection = null;
        }
    }
}