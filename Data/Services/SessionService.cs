using BikeSparesInventorySystem.Data.Models;
using BikeSparesInventorySystem.Data.Utils;
using System.Text.Json;

namespace BikeSparesInventorySystem.Data.Services;

internal class SessionService
{
    internal string SessionPath { get; set; } = Explorer.GetDefaultFilePath<Session>(Enums.FileExtension.json);

    internal async Task<Session> LoadSession()
    {
        try
        {
            using var stream = File.OpenRead(SessionPath);
            return await JsonSerializer.DeserializeAsync<Session>(stream);
        }
        catch
        {
            return null;
        }
    }

    internal async Task SaveSession(Session data)
    {
        using var stream = File.Create(SessionPath);
        await JsonSerializer.SerializeAsync(stream, data);
    }

    internal void DeleteSession()
    {
        try
        {
            File.Delete(SessionPath);
        }
        catch { }
    }
}
