namespace Jogging_Times.Core.Const
{
    public class ResponseMessage
    {
        public const string RegisterErrorMessage = "User creation failed! Please check user details and try again.";
        public const string RegistersuccessMessage = "User created successfully!";
        public const string EmailErrorMessage = "Email is already registered!";
        public const string UserNameErrorMessage = "Email is already registered!";
        public const string UserNamePasswordMessage = "Email or password is incorrect!";
        public const string UserNotFoundMessage = "User not found";
        public const string ErrorUpdateMessage = "An error accurred while updating the user!";
        public const string SuccessUpdateMessage = "Updated User successfully !";
        public const string ErrorDeleteUserMessage = "An error accurred while deletion the user !";
        public const string SuccessDeleteUserMessage = "Deleted User successfully !";
        public const string RequiredUserIdMessage = "User Id Is required !";
        public const string RegisterRoleErrorMessage = "You Don't have permession to create Admin!";
        public const string RoleExistMessage = "This user already has this role !";
        public const string JoggingErrorMessage = "There is no jogging time to shown !";
        public const string JoggingNullErrorMessage = "please enter your jogging time";
        public const string JoggingTimeCreatedMessage = "Jogging time created succesfully!";
        public const string JoggingTimeDeletedMessage = "Jogging time Deleted succesfully!";
        public const string JoggingTimeUpdatedMessage = "Jogging time updated succesfully!";
        public const string JoggingTimeIdRequiredErrorMessage = "Jogging time Id required!";
        public const string joggingNotFoundMessage = "There is no jogging time by this id !";
        public const string DeleteJoggingtoanotheruserErrorMessage = "You Don't have permession to remove jog times of another user!";
    }
}
