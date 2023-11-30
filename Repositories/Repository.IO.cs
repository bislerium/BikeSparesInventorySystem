namespace BikeSparesInventorySystem.Repositories;

internal abstract class RepositoryIO<TSource> where TSource : IModel
{
    protected internal ICollection<TSource> _sourceData;

    private readonly FileProvider<TSource> _fileProvider;

    internal RepositoryIO(FileProvider<TSource> fileProvider)
    {
        _fileProvider = fileProvider;
    }

    public virtual async Task LoadAsync()
    {
        try
        {
            _sourceData = await _fileProvider.ReadAsync();
        }
        catch
        {
            _sourceData = new List<TSource>();
            // throw;
        }

    }

    public virtual async Task LoadAsync(string path, bool writeToPath = false)
    {
        try
        {
            if (writeToPath)
            {
                _fileProvider.FilePath = path;
                _sourceData = await _fileProvider.ReadAsync();
            }
            else
            {
                _sourceData = await _fileProvider.ReadAsync(path);
            }
        }
        catch
        {
            _sourceData = new List<TSource>();
        }
    }

    public virtual async Task ImportAsync(FileProvider<TSource> fileProvider, Stream stream, bool append = true)
    {
        try
        {
            IEnumerable<TSource> data = await fileProvider.ReadAsync(stream);

            if (append)
            {
                var idSet = new HashSet<Guid>(_sourceData.Select(x => x.Id));
                data = data.Where(x => !idSet.Contains(x.Id));
            }
            else
            {
                _sourceData.Clear();
            }

            foreach (var i in data)
            {
                _sourceData.Add(i);
            }
        }
        catch
        {
            throw new Exception("Invalid File!");
        }
    }

    public virtual void Load(ICollection<TSource> sourceData)
    {
        _sourceData = sourceData;
    }

    public virtual async Task FlushAsync()
    {
        await _fileProvider.WriteAsync(_sourceData);
    }

    public virtual async Task FlushAsync(string path, bool readFromPath = false)
    {
        if (readFromPath)
        {
            _fileProvider.FilePath = path;
            await _fileProvider.WriteAsync(_sourceData);
        }
        else
        {
            await _fileProvider.WriteAsync(path, _sourceData);
        }
    }

    public virtual async Task ExportAsync(FileProvider<TSource> fileProvider, string path)
    {
        await fileProvider.WriteAsync(path, _sourceData);
    }
}
