using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using TinkloProblemos.API.Identity.Entities;

namespace Daarto.IdentityProvider.Tables
{
    public class UsersClaimsTable
    {
        private readonly MySqlConnection _sqlConnection;

        public UsersClaimsTable(MySqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public Task<IList<Claim>> GetClaimsAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            const string command = "SELECT * " +
                                   "FROM UsersClaims " +
                                   "WHERE UserId = @UserId;";

            IEnumerable<UserClaim> userClaims = Task.Run(() => _sqlConnection.QueryAsync<UserClaim>(command, new
            {
                UserId = user.Id
            }), cancellationToken).Result;

            return Task.FromResult<IList<Claim>>(userClaims.Select(e => new Claim(e.ClaimType, e.ClaimValue)).ToList());
        }

        public Task AddClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims)
        {
            const string command = "INSERT INTO UsersClaims " +
                                   "VALUES (@Id, @UserId, @ClaimType, @ClaimValue);";

            return _sqlConnection.ExecuteAsync(command, claims.Select(e => new
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                ClaimType = e.Type,
                ClaimValue = e.Value
            }));
        }

        public Task ReplaceClaimAsync(ApplicationUser user, Claim claim, Claim newClaim)
        {
            const string command = "UPDATE UsersClaims " +
                                   "SET ClaimType = @NewClaimType, ClaimValue = @NewClaimValue " +
                                   "WHERE UserId = @UserId AND ClaimType = @ClaimType AND ClaimValue = @ClaimType;";

            return _sqlConnection.ExecuteAsync(command, new
            {
                NewClaimType = newClaim.Type,
                NewClaimValue = newClaim.Value,
                UserId = user.Id,
                ClaimType = claim.Type,
                ClaimValue = claim.Value
            });
        }
    }
}