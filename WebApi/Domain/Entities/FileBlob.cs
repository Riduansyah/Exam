namespace Domain.Entities
{
    public partial class FileBlob
    {
        public Guid FileBlobId { get; set; }
        public byte[] Content { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
    }
}
