namespace IntraVision.Web.Mvc.Security
{
    public class MVCIdentity : IMVCIdentity
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public MVCIdentity(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public string AuthenticationType
        {
            get { return "IntraVision.MVC.Security"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }
    }
}
