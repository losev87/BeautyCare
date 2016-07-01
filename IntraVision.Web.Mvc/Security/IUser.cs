namespace IntraVision.Web.Mvc.Security
{
    public interface IUser
    {
        int Id { get; }
        string Login { get; }
        string Password { get; }
        string UserName { get; set; }

        string GetPermissions();
    }
}
