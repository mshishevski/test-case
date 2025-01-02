using System.Data;

namespace TotalOne.Application;

public interface ITotalOneContext
{
    public IDbConnection CreateConnection();
}
