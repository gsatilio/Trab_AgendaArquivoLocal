using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trab_AgendaArquivoLocal
{
    internal class Phone
    {
        public string number { get; set; }
        public string type { get; set; }

        public Phone(string number, string type)
        {
            this.number = number;
            this.type = type;
        }

        public override string? ToString()
        {
            return $"Tipo: {type} Número: {number}";
        }
        public string printToFile()
        {
            string text = "";
            text = $"|{type}|{number}";
            return text;
        }
    }
}
