using WebApplication1.Services;
using System.Data;

namespace WebApplication1.Interfaces;

public interface IRubiconService
{
    public Task<DataSet> ExecuteCommand(SpRocket command);
}
