namespace Löwen.Application.Messaging.ICache;

public static class PrefexesAndDurationsForCacheSettings
{
    public static readonly string User_prefix = "User";
    public static readonly string Users_ForAdmins_prefix = "UserAdmin";
    public static readonly string admins_prefix = "admins";
    public static readonly string RootAdmin_prefix = "RootAdmin";
    public static readonly string Product_prefix = "Product";

    public static readonly int Users_list_durationMinutes = 4;
    public static readonly int membership_list_durationMinutes = 2;
    public static readonly int admins_list_durationMinutes = 1;
    public static readonly int Individual_result_durationMinutes = 5;
    public static readonly int Product_ForAdmins_durationMinutes = 5;
    public static readonly int User_durationMinutes = 1;
}
