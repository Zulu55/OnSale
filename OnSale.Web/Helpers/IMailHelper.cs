using OnSale.Common.Responses;

namespace OnSale.Web.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string to, string subject, string body);
    }
}
