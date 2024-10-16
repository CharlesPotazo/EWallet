﻿using MimeKit;
using System;
using MailKit.Net.Smtp;

namespace EWalletBusinessLogic
{
    public class EmailServices
    {

        DateTime dateTime = DateTime.Now;

        public bool emailNewUser(string username, string email)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("(C)-Cash", "do-not-reply@ccashemail.com"));
            message.To.Add(new MailboxAddress(username, email));
            message.Subject = "Thanks for Subscribing!";

            message.Body = new TextPart("html")
            {
                Text = $"<h1>Welcome to (C)-Cash {username}! </h1>" +
                "<p>Thank you for subscribing to (C)-Cash</p> " +
                "<br><p>Terms and Agreement</p>" +
                "<p>-Bawal lumabas by kimchu</p>" +
                "<p>-confidential funds by sara du30</p>" +
                "<p>-dubidubidapdap by welleh reveleme</p>" +
                "<p>-dubidubidapdap by welleh reveleme</p>" +
                $"<br><p>-(C)-Cash admin <i> {dateTime}</i></p>"
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("sandbox.smtp.mailtrap.io", 2525, MailKit.Security.SecureSocketOptions.StartTls);

                    client.Authenticate("8ec65822a4a8c1", "2a1539095e13a9");

                    client.Send(message);

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }

        public bool emailCashin(string receiver, string emailReceiver, decimal amount, string sender)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("(C)-Cash", "do-not-reply@ccashemail.com"));
            message.To.Add(new MailboxAddress(receiver, emailReceiver));
            message.Subject = "Money Transaction";

            message.Body = new TextPart("html")
            {
                Text = $"<h1>Received Money! </h1>" +
                $"<br><p>{receiver} received  <strong>P{amount}</strong> from  <strong>Account Number: </strong>{sender} </p>" +
                $"<br><p>-(C)-Cash admin <i> {dateTime}</i></p>"
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("sandbox.smtp.mailtrap.io", 2525, MailKit.Security.SecureSocketOptions.StartTls);

                    client.Authenticate("8ec65822a4a8c1", "2a1539095e13a9");

                    client.Send(message);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;

                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }

        public bool emailCashout(string user, string userAccNum, string emailUser, decimal amount)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("(C)-Cash", "do-not-reply@ccashemail.com"));
            message.To.Add(new MailboxAddress(user, emailUser));
            message.Subject = "Money Transaction";

            message.Body = new TextPart("html")
            {
                Text = $"<h1>Withdraw Money! </h1>" +
                $"<br><p>You withdraw <strong>P{amount}</strong> from your <strong>Account Number: </strong>{userAccNum} </p>" +
                $"<br><p>-(C)-Cash admin <i> {dateTime}</i></p>"
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("sandbox.smtp.mailtrap.io", 2525, MailKit.Security.SecureSocketOptions.StartTls);

                    client.Authenticate("8ec65822a4a8c1", "2a1539095e13a9");

                    client.Send(message);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;

                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }

        public bool emailTransferMoney(string senderName, string senderAccNum, string senderEmail, string receiverName, string receiverAccNum, string receiverEmail, decimal amount)
        {
            emailCashin(receiverName, receiverEmail, amount, senderAccNum);
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("(C)-Cash", "do-not-reply@ccashemail.com"));
            message.To.Add(new MailboxAddress(senderName, senderEmail));
            message.Subject = "Money Transaction";

            message.Body = new TextPart("html")
            {
                Text = $"<h1>Withdraw Money! </h1>" +
                $"<br><p>You transfer <strong>P{amount}</strong> to <strong>Account Number: </strong>{receiverAccNum} <strong> Username: </strong>{receiverName} </p>" +
                $"<br><p>-(C)-Cash admin <i> {dateTime}</i></p>"
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("sandbox.smtp.mailtrap.io", 2525, MailKit.Security.SecureSocketOptions.StartTls);

                    client.Authenticate("8ec65822a4a8c1", "2a1539095e13a9");

                    client.Send(message);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;

                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }

        public bool emailChangeUsernam(string senderName, string senderAccNum, string senderEmail, string receiverName, string receiverAccNum, string receiverEmail, decimal amount)
        {
            emailCashin(receiverName, receiverEmail, amount, senderAccNum);
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("(C)-Cash", "do-not-reply@ccashemail.com"));
            message.To.Add(new MailboxAddress(senderName, senderEmail));
            message.Subject = "Money Transaction";

            message.Body = new TextPart("html")
            {
                Text = $"<h1>Withdraw Money! </h1>" +
                $"<br><p>You transfer <strong>P{amount}</strong> to <strong>Account Number: </strong>{receiverAccNum} <strong> Username: </strong>{receiverName} </p>" +
                $"<br><p>-(C)-Cash admin <i> {dateTime}</i></p>"
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("sandbox.smtp.mailtrap.io", 2525, MailKit.Security.SecureSocketOptions.StartTls);

                    client.Authenticate("8ec65822a4a8c1", "2a1539095e13a9");

                    client.Send(message);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;

                }
                finally
                {
                    client.Disconnect(true);
                }
            }


        }

        public bool emailReactivate(string email)
        {

            UserServices userServices = new UserServices();

            var user = userServices.GetUserByEmail(email);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("(C)-Cash", "do-not-reply@ccashemail.com"));
            message.To.Add(new MailboxAddress(user.userName, email));
            message.Subject = "Thanks for Subscribing!";

            message.Body = new TextPart("html")
            {
                Text = $"<h1>Welcome Back to (C)-Cash {user.userName}! </h1>" +
                "<p>We are glad to see you again (C)-Cash</p> " +
                "<br><p>Below are your credentials.\n You can change your credential in the settings</p>" +
                $"<p>Username: {user.userName}</p>" +
                $"<p>AccountNumber: {user.accountNumber}</p>" +
                $"<p>Email: {user.email}</p>" +
                $"<p>Pin: {user.pinNumber}</p>" +
                $"<br><p>- (C)-Cash admin <i>{dateTime}</i></p>"
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("sandbox.smtp.mailtrap.io", 2525, MailKit.Security.SecureSocketOptions.StartTls);

                    client.Authenticate("8ec65822a4a8c1", "2a1539095e13a9");

                    client.Send(message);

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }

        public bool emailDeleteAccount(string email)
        {

            UserServices userServices = new UserServices();

            var user = userServices.GetUserByEmail(email);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("(C)-Cash", "do-not-reply@ccashemail.com"));
            message.To.Add(new MailboxAddress(user.userName, email));
            message.Subject = "Thanks for using (C)-Cash!";

            message.Body = new TextPart("html")
            {
                Text = $"<h1>Thank you user! </h1>" +
                "<p>Register this email again if you want o reativate your account</p> " +
                $"<br><p>- (C)-Cash admin <i>{dateTime}</i></p>"
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("sandbox.smtp.mailtrap.io", 2525, MailKit.Security.SecureSocketOptions.StartTls);

                    client.Authenticate("8ec65822a4a8c1", "2a1539095e13a9");

                    client.Send(message);

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }
    }
}