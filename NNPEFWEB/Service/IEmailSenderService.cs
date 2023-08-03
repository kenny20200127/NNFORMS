
using NNPEFWEB.Models;

namespace NNPEFWEB.Service
{
	public interface IEmailSenderService
	{
		string SendEmailAsync(EmailModel mailRequest);
		string SendMultipleEmail(EmailModel2 mailRequest);

    }
}
