namespace IntraVision.Web.Mvc
{
    public interface IListViewModel
    {
        string Title { get; set; }
        bool Creatable { get; set; }
        bool CreateInModal { get; set; }
        bool EditInModal { get; set; }
        bool Sortable { get; set; }
    }
}
