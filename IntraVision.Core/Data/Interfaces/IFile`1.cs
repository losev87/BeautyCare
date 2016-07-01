namespace IntraVision.Data
{
    public interface IFile<TData> where TData : EntityBaseFile
    {
        TData Data { get; set; }
        string Extension { get; set; }
    }
}
