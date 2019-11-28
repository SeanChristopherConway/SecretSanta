using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Linq;

namespace csharp
{

    class Program
    {

        static List<Person> Participants = new List<Person>();

        static Random rnd = new Random();


        class Person
        {
            string _Email, _Sender, _Receiver;

            public string Email
            {
                get
                {
                    return this._Email;
                }

                set
                {
                    this._Email = value;
                }

            }

            public string Sender
            {
                get
                {
                    return this._Sender;
                }

                set
                {
                    this._Sender = value;
                }

            }

            public string Receiver
            {
                get
                {
                    return this._Receiver;
                }

                set
                {
                    this._Receiver = value;
                }

            }
        }

        static Person GetRandomContact(Person person)
        {
            var newList = Participants.Where(x => !x.Equals(person) && x.Receiver== null).ToList();
            int r = rnd.Next(newList.Count);
            Console.WriteLine(r.ToString());
            return newList[r];
        }

        static Boolean SendEmail(string Sender, string Receiver)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("scconway8@gmail.com");
                mail.To.Add(Sender);
                mail.Subject = "Secret santa info for " + Sender;
                mail.Body = "You have been selected to be the secret Santa for " + Receiver;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("scconway8@gmail.com", "");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                Console.WriteLine("mail Sent");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        static void Main(string[] args)
        {

            Person Lesley = new Person();
            Lesley.Email = "conwaylesley1@gmail.com";
            Person Mike = new Person();
            Mike.Email = "educogo@gmail.com";
            Person Rod = new Person();
            Rod.Email = "rodneyaconway@gmail.com";
            Person Sean = new Person();
            Sean.Email = "scconway8@gmail.com";
            Person Mutsumi = new Person();
            Mutsumi.Email = "mutsumutsu0321@yahoo.co.jp";



            Participants.Add(Lesley);
            Participants.Add(Rod);
            Participants.Add(Mike);
            Participants.Add(Sean);
            Participants.Add(Mutsumi);
            while (Participants.Any(x => x.Sender == null))
            {
                var secretSantaRecipient = Participants.FirstOrDefault(x => x.Sender == null);
                var secretSanta = GetRandomContact(secretSantaRecipient);

                var isSent = SendEmail(secretSanta.Email, secretSantaRecipient.Email);
                if (isSent)
                {
                    secretSanta.Receiver = secretSantaRecipient.Email;
                    secretSantaRecipient.Sender = secretSanta.Email;
                }

            }

            //Participants.ForEach(x => Console.WriteLine(x.Email + ":" + x.Receiver));



        }
    }
}
