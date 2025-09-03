namespace Löwen.Domain.ErrorHandleClasses;

public static class ErrorCodes
{
    public static class User
    {
        public const string NotFound = "User.NotFound";
        public const string AlreadyExists = "User.AlreadyExists";
        public const string CreateError = "User.CreateError";

        public const string EmailAlreadyExists = "User.EmailAlreadyExists";
        public const string UsernameAlreadyExists = "User.UsernameAlreadyExists";
        public const string InvalidEmail = "User.InvalidEmail";
        public const string InvalidUsername = "User.InvalidUsername";
        public const string WeakPassword = "User.WeakPassword";

        public const string InvalidCredentials = "User.InvalidCredentials";
        public const string AccountLocked = "User.AccountLocked";
        public const string Unauthorized = "User.Unauthorized";

        public const string EmailNotConfirmed = "User.EmailNotConfirmed";
        public const string PhoneNotConfirmed = "User.PhoneNotConfirmed";
        public const string ConfirmationTokenInvalid = "User.ConfirmationTokenInvalid";

        public const string InvalidProfileData = "User.InvalidProfileData";
        public const string CannotUpdateEmail = "User.CannotUpdateEmail";
        public const string CannotUpdateUsername = "User.CannotUpdateUsername";

        public const string CannotDelete = "User.CannotDelete";
        public const string AlreadyDeactivated = "User.AlreadyDeactivated";

        public const string InvalidPassword = "User.InvalidPassword";
        public const string PasswordMismatch = "User.PasswordMismatch";
        public const string PasswordReuseNotAllowed = "User.PasswordReuseNotAllowed";

        public const string AlreadyBlocked = "User.AlreadyBlocked";
        public const string NotBlocked = "User.NotBlocked";
        public const string CannotBlockYourself = "User.CannotBlockYourself";
    }
}
