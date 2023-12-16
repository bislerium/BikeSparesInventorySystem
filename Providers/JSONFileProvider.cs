namespace BikeSparesInventorySystem.Providers;

internal class JsonFileProvider<TSource> : FileProvider<TSource> where TSource : IModel
{
    private static readonly JsonSerializerOptions options = new()
    {
        TypeInfoResolver = new DefaultJsonTypeInfoResolver
        {
            Modifiers =
            {
                static typeInfo =>
                {
                    if (typeInfo.Kind != JsonTypeInfoKind.Object) { return; } foreach (JsonPropertyInfo propertyInfo in typeInfo.Properties)
                    {
                        // Add IsRequired constraint from every property.
                        propertyInfo.IsRequired = true;
                    }
                }
            }
        }
    };

    internal override string FilePath { get; set; } = Explorer.GetDefaultFilePath<TSource>(Enums.FileExtension.json);

    internal override async Task<ICollection<TSource>> ReadAsync(string path)
    {
        return await ReadAsync(File.OpenRead(path));
    }

    internal override async Task<ICollection<TSource>> ReadAsync(Stream stream)
    {
        try
        {
            var list = await JsonSerializer.DeserializeAsync<List<TSource>>(stream, options);            
            return list ??= [];
        }
        catch
        {
            throw;
        }
        finally
        {
            stream.Close();
        }
    }

    internal override async Task WriteAsync(string path, ICollection<TSource> data)
    {
        using FileStream stream = File.Create(path);
        await JsonSerializer.SerializeAsync(stream, data);
    }
}
