namespace IntraVision.Web.Mvc
{
    public class ListViewModelBase : IListViewModel
    {
        public string Title { get; set; }
        public bool Creatable { get; set; }
        public bool CreateInModal { get; set; }
        public bool EditInModal { get; set; }
        public bool Sortable { get; set; }

        public ListViewModelBase()
        {
            Creatable = true;
        }
    }
}
