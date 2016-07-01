using System;

namespace IntraVision.Data
{
    public interface IEntityBaseFile : IEntityBase
    {
        Guid Guid { get; set; }
        byte[] Data { get; set; }
        string ContentType { get; set; }
        DateTime? DateChanged { get; set; }
        string FileName { get; set; }
    }
}
