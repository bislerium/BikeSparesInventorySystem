using BikeSparesInventorySystem.Data.Models;
using BikeSparesInventorySystem.Data.Providers;

namespace BikeSparesInventorySystem.Data.Repositories
{
    internal abstract class RepositoryIO<TSource> where TSource: IModel
    {
        protected internal ICollection<TSource> _sourceData;

        readonly FileProvider<TSource> _fileProvider;

        internal RepositoryIO(FileProvider<TSource> fileProvider) => _fileProvider = fileProvider;

        public virtual async Task LoadAsync() => _sourceData = await _fileProvider.ReadAsync();

        public virtual async Task LoadAsync(string path, bool writeToPath = false)
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

        public virtual async Task FlushAsync() => await _fileProvider.WriteAsync(_sourceData);

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
    }
}
