namespace WebApplication1.DTO
{
    public class RegisterDTO
    {
        public string Name { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; } = "";
        public string email { get; set; }

        public string MobileNo { get; set; }
        public string img { get; set; }


    }
}
