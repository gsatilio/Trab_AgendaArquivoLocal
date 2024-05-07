using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trab_AgendaArquivoLocal
{
    internal class Contact
    {
        public string name {  get; set; }
        public string email {  get; set; }
        public Address address {  get; set; }
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
            return $"Nome: {name}\nEmail: {email}\n{address.ToString()}\n{phone.ToString()}";
        }
    }
}
