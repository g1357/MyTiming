using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyTiming.ViewModels
{
    public class SendMailViewModel : BaseViewModel
    {
        Page _page;
        public ICommand SendMailCommand => new Command(async () => await SendMailAsync());

        public SendMailViewModel(Page page)
        {
            _page = page;
        }

        private async Task SendMailAsync()
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = "Data from My Timing Application",
                    Body = "Data from My Timing Application",
                    To = new List<string>() { "g1357@yandex.ru"},
                    //Cc = ccRecipients,
                    //Bcc = bccRecipients
                };
                var fn = "Attachment.txt";
                var file = Path.Combine(FileSystem.CacheDirectory, fn);
                File.WriteAllText(file, "Hello World");

                message.Attachments.Add(new EmailAttachment(file));
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                // Email is not supported on this device
            }
            catch (Exception ex)
            {
                // Some other exception occurred
            }
        }
    }
}
