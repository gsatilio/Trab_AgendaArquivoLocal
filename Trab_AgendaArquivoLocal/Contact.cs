using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trab_AgendaArquivoLocal
{
    internal class Contact
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public List<Phone> Phone { get; set; }

        public Contact(string name, string email, Address address, List<Phone> phone)
        {
            this.Name = name;
            this.Email = email;
            this.Address = address;
            this.Phone = phone;
        }

        public override string? ToString()
        {
            string text = "";
            text = "=> Contato";
            text = $"\nNome: {Name}\nEmail: {Email}\n{Address.ToString()}";
            text += "\n=> Telefone(s)";
            for (int i = 0; i < Phone.Count; i++)
            {
                text += "\n" + Phone[i].ToString();
            }
            return text;
        }
        public string printPhones()
        {
            string text = "";
            text += "\n=> Telefone(s)";
            for (int i = 0; i < Phone.Count; i++)
            {
                text += "\n" + Phone[i].ToString();
            }
            return text;
        }
        public string printToFile()
        {
            string text = "";
            text = $"{this.Name};{this.Email};{this.Address.printToFile()};";
            for (int i = 0; i < Phone.Count; i++)
            {
                text += Phone[i].printToFile();
            }
            return text;
        }
    }
}
