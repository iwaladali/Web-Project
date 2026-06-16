namespace SubscriptionHub.Models.DTO
{
    public class ChangePassword
    {
        public string CurrentPassword { set; get; }
        public string  NewPassword { set; get; }
        public string  ConfirmPassword { set; get; }
    }
}
