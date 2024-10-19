using CCRS.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCRS.Business.Services
{
    public class EmailTemplateService : IEmailTemplateService
    {
        public string GenerateEmailConfirmationTemplate(string callbackUrl)
        {
            return $@"<!DOCTYPE html>
                        <html lang='en'>
                        <body style='font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0;'>
                            <div style='width: 100%; max-width: 600px; margin: auto; background: white; padding: 20px; border-radius: 5px; box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);'>
                                <h1 style='color: #333;'>Welcome to NutriPlanner!</h1>
                                <p style='color: #555;'>Hi there!</p>
                                <p style='color: #555;'>Thank you for registering with us. Please confirm your email address by clicking the button below:</p>
                                <a href='{callbackUrl}' style='display: inline-block; padding: 10px 15px; color: white; background-color: #28a745; text-decoration: none; border-radius: 5px; margin-top: 20px;'>Confirm your email</a>
                                <p style='color: #555;'>If you did not create an account, no further action is required.</p>
                                <p style='color: #555;'>Best regards,<br>The NutriPlanner Team</p>
                                <div style='margin-top: 20px; font-size: 12px; color: #777;'>
                                    <p>NutriPlanner | All rights reserved</p>
                                </div>
                            </div>
                        </body>
                        </html>";
        }

        public string GeneratePasswordResetTemplate(string callbackUrl)
        {
            throw new NotImplementedException();
        }
    }
}
