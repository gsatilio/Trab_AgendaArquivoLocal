using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trab_AgendaArquivoLocal
{
    internal class Contact
    {
        public string name { get; set; }
        public string email { get; set; }
        public Address address { get; set; }
        public List<Phone> phone { get; set; }

        public Contact(string name, string email, Address address, List<Phone> phone)
        {
            this.name = name;
            this.email = email;
            this.address = address;
            this.phone = phone;
        }

        public override string? ToString()
        {
            string text = "";
            text = "=> Contato";
            text = $"\nNome: {name}\nEmail: {email}\n{address.ToString()}";
            text += "\n=> Telefone(s)";
            for (int i = 0; i < phone.Count; i++)
            {
                text += "\n" + phone[i].ToString();
            }
            return text;
        }
        public string printPhones()
        {
            string text = "";
            text += "\n=> Telefone(s)";
            for (int i = 0; i < phone.Count; i++)
            {
                text += "\n" + phone[i].ToString();
            }
            return text;
        }
        public string printToFile()
        {
            string text = "";
            text = $"{this.name};{this.email};{this.address.printToFile()};";
            for (int i = 0; i < phone.Count; i++)
            {
                text += phone[i].printToFile();
            }
            return text;
        }
    }
}
