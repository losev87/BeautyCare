using System.Runtime.Serialization;

namespace IntraVision.Core.Security
{
    public interface IUser : ISerializable //WTF??
    {
        long Id { get; }
        string Name { get; }
        string[] Roles { get; set; }
    }
}
