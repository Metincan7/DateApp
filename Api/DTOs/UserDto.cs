namespace Api.DTOs
{
    public class UserDto
    {
        // kodumuzu istediğimiz şekilde geri döndüreceğimiz için büyük küçük harf önemli değil burada.!!
        public string Username { get; set; }
        public string Token { get; set; }
    }
}