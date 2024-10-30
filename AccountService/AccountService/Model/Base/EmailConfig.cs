namespace AccountService.Model.Base
{
    public class EmailConfig
    {
        public string SMTP { get; set; }
        public int Porta { get; set; }
        public string Remetente { get; set; }
        public string Email { get; set; }
        public bool SSLAtivo { get; set; }
        public string Senha { get; set; }
        public string BaseUrl { get; set; }
    }

}
