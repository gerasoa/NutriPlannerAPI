using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCRS.Business.Interfaces
{
    public interface IEmailService
    {
        Task SendHtmlEmailAsync(string email, string subject, string message);
    }
}
