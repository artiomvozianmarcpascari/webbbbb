using System.Web;

namespace Vintagefur.Domain.DTO
{
    public class SignOutResultDto
    {
        public bool IsSuccess { get; set; }
        public HttpCookie Cookie { get; set; }
    }
} 