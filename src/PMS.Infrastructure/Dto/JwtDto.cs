namespace PMS.Infrastructure.Dto
{
    public class JwtDto
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public string Role { get; set; }

        public JwtDto(string token, DateTime expires, string role)
        {
            Token = token;
            Expires = expires;
            Role = role;
        }
    }
}