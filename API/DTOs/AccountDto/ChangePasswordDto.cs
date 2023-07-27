namespace API.DTOs.AccountDto
{
    public class ChangePasswordDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public int OTP { get; set; }
    }
}
