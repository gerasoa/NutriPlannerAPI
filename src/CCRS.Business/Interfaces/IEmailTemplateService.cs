using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCRS.Business.Interfaces
{
    public interface IEmailTemplateService
    {
        string GenerateEmailConfirmationTemplate(string callbackUrl);
        string GeneratePasswordResetTemplate(string callbackUrl);
    }
}
