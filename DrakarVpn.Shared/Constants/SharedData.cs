
namespace DrakarVpn.Shared.Constants;

public static class SharedData
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string User = "User";

        private static readonly IReadOnlyList<string> _allRoles = new List<string>() { Admin, User };
        public static IReadOnlyList<string> AllRoles => _allRoles;

    }
}
