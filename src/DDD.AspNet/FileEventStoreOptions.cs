using DDD.Domain;

namespace DDD
{
    public class FileEventStoreOptions
    {
        public string RootDirectory { get; set; }
        public IFileSystem FileSystem { get; set; }
        public IEventSerializer EventSerializer { get; set; }
        public IEventTypeResolver EventTypeResolver { get; set; }
    }
}