using System.Data;
using System.Security.Claims;
using System.Security;

using Npgsql;
using WebApplication1.ServiceRegistration;
using Microsoft.Extensions.Options;
using WebApplication1.Interfaces;


namespace WebApplication1.Services;
public class RubiconService : IRubiconService//because I can
{
    public RubiconService(IOptions<RubiconServiceOptions> rso)
    {
        _conString = rso.Value.Con ?? string.Empty;
    }


    //Creates a single connection for the life of the session, which is closed and disposed immediately after the session ends.
    private NpgsqlConnection? _con;
    private readonly string _conString;
    /// <summary>
    /// This this obsolete as of Npgsql7.0 Opens a connection.
    /// </summary>
    [Obsolete]
    private async Task Open()
    {
        if (_con is null)
        {
            _con = new(_conString);
            await _con.OpenAsync();
        }
        //it is only possible to open connections within this service.
    }

    private async Task Close()
    {
        if (_con is not null)
        {
            await _con.CloseAsync();
            await _con.DisposeAsync();
        }
    }

    /// <summary>
    /// Allows a valid stored procedure to be executed. Resources are disposed once complete.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    /// <exception cref="SecurityException"></exception>
    public async Task<DataSet> ExecuteCommand(SpRocket command)
    {
        await Open();
        if (_con is null)
        {
            throw new SecurityException("User may be invalid.");
        }

        //this pretty bad since Postgres does not truly have a concept of stored procedures, rather "routines", where a function attempts to do the same, albeit, poorly.
        NpgsqlCommand commandCon = _con.CreateCommand();
        commandCon.CommandText = SpRocketCommands.GetCommand(command);
        List<Claim> claims = new();
        NpgsqlDataAdapter handle = new(commandCon);
        DataSet set = new();
        handle.Fill(set);
        await Close();
        return set;
        //it additionally becomes increasingly cumbersome/painful as we still essentially execute sql as strings. thus i question, is security truly a concern or merely an after thought in light of events?
    }
}
//final remarks:
//this should probably be tied to the discriminated user after authentication, meaning, if and only if the identity is valid, will this resource be provisioned to user by the app
//should they not meet the requirements, they will not be allotted this resource, additionally, as an added layer, these commands shall be executed on their behalf, only with proper authorization.

public enum SpRocket
{
    GetUserById,
    CallUserByIdProc
}

public static class SpRocketCommands
{
    public static string GetCommand(SpRocket command)
    {
        switch (command)
        {
            case SpRocket.GetUserById:
                return "";
            case SpRocket.CallUserByIdProc:
                return "CALL get_user_proc(2);FETCH ALL FROM user_cursor;";
            default:
                throw new NotImplementedException($"The specified SpRocket command {command} does not exist. Ensure the drive chain is connected with the respective database.");
        }
    }
}



