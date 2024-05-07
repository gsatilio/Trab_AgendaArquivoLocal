﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trab_AgendaArquivoLocal
{
    internal class Address
    {
        public string postalcode {  get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string neighborhood { get; set; }
        public int number { get; set; }

        public Address(string postalcode, string state, string city, string street, string neighborhood, int number)
        {
            this.postalcode = postalcode;
            this.state = state;
            this.city = city;
            this.street = street;
            this.neighborhood = neighborhood;
            this.number = number;
        }

        public override string? ToString()
        {
            return $"CEP: {postalcode}\nUF: {state}\nCidade: {city}\nEndereço: {street}\nNúmero: {number}\nBairro: {neighborhood}";
        }
        public string printToFile()
        {
            string text = "";
            text = $"{postalcode};{state};{city};{street};{number};{neighborhood}";
            return text;
        }
    }
}
