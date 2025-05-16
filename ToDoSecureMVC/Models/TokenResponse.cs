namespace ToDoSecureMVC.Models
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string UserName {  get; set; }
        public List<string> Roles { get; set; }
    }
}
