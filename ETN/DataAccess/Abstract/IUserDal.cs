using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        // Join Atma İşlemi Vardır. // Rolleri OperationClaimler
        List<OperationClaim> GetClaims(User user);
    }
}
