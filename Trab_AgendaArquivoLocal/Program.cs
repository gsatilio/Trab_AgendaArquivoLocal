using System.Collections.Generic;
using System.IO;
using Trab_AgendaArquivoLocal;

internal class Program
{
    private static void Main(string[] args)
    {
        List<Contact> listContact = new();
        int opt;
        do
        {
            Console.Clear();
            Console.WriteLine(">> Agenda <<");
            Console.WriteLine("1 - Criar Contato");
            Console.WriteLine("2 - Remover Contato");
            Console.WriteLine("3 - Mostrar todos os Contatos");
            Console.WriteLine("4 - Pesquisar Contato");
            Console.WriteLine("5 - Modificar Dados do Contato");
            Console.WriteLine("6 - Salvar Agenda no Computador");
            Console.WriteLine("7 - Importar Arquivo de Agenda");
            Console.WriteLine("0 - Sair");
            opt = int.Parse(Console.ReadLine());
            switch (opt)
            {
                case 0:
                    break;
                case 1:
                    listContact.Add(createContact());
                    break;
                case 2:
                    removeContact(listContact);
                    break;
                case 3:
                    printAll(listContact);
                    break;
                case 4:
                    findContact(listContact);
                    break;
                case 5:
                    modifyContact(listContact);
                    break;
                case 6:
                    saveToFile(listContact);
                    break;
                case 7:
                    listContact = importFile();
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        } while (opt != 0);

    }
    static Contact createContact()
    {
        string name, email;
        Console.WriteLine("Informe o Nome do contato:");
        name = Console.ReadLine();
        Console.WriteLine("Informe o Email do contato:");
        email = Console.ReadLine();

        Contact contact = new Contact(name, email, createAddress(), createPhone());
        return contact;
    }
    static Address createAddress()
    {
        string postalcode, state, city, street, neighborhood;
        int number;
        Console.WriteLine("Informe o CEP:");
        postalcode = Console.ReadLine();
        Console.WriteLine("Informe a UF:");
        state = Console.ReadLine();
        Console.WriteLine("Informe a Cidade:");
        city = Console.ReadLine();
        Console.WriteLine("Informe o Endereço:");
        street = Console.ReadLine();
        Console.WriteLine("Informe o Bairro:");
        neighborhood = Console.ReadLine();
        Console.WriteLine("Informe o número do logradouro:");
        number = int.Parse(Console.ReadLine());

        Address temp_address = new Address(postalcode, state.ToUpper(), city.ToUpper(), street.ToUpper(), neighborhood.ToUpper(), number);

        return temp_address;
    }
    static List<Phone> createPhone()
    {
        string type, number, opt = "";
        int phonequantity = 0;
        List<Phone> temp_listphone = new();
        Phone temp_phone;

        do
        {
            Console.WriteLine("Informe o tipo de contato (Fixo/Celular/etc)");
            type = Console.ReadLine();
            Console.WriteLine("Informe o número do telefone:");
            number = Console.ReadLine();
            temp_phone = new Phone(number, type.ToUpper());
            temp_listphone.Add(temp_phone);
            if (phonequantity < 1)
            {
                Console.WriteLine("Deseja inserir mais um telefone?\n[S - Sim][Qualquer outa tecla para Não]");
                opt = Console.ReadLine();
            }
            phonequantity++;
        } while (opt == "s" && phonequantity < 2);

        return temp_listphone;
    }
    static void removeContact(List<Contact> listContact)
    {
        string name;
        bool removed = false;
        Console.WriteLine("Informe o nome do contato que deseja excluir:");
        name = Console.ReadLine();
        for (int i = listContact.Count - 1; i >= 0; i--)
        {
            if (listContact[i].Name == name)
            {
                listContact.RemoveAt(i);
                removed = true;
            }
        }
        if (removed)
        {
            Console.WriteLine("Contato excluído com sucesso.");
        }
        else
        {
            Console.WriteLine("Contato não localizado.");
        }
    }
    static void findContact(List<Contact> listContact)
    {
        string name;
        bool found = false;
        Console.WriteLine("Informe o nome do contato que deseja localizar:");
        name = Console.ReadLine();
        for (int i = listContact.Count - 1; i >= 0; i--)
        {
            if (listContact[i].Name == name)
            {
                Console.WriteLine(listContact[i].ToString());
                found = true;
            }
        }
        if (!found)
        {
            Console.WriteLine("Contato não localizado.");
        }
    }
    static void printAll(List<Contact> listContact)
    {
        bool found = false;
        for (int i = listContact.Count - 1; i >= 0; i--)
        {
            Console.WriteLine(listContact[i].ToString());
            Console.WriteLine("------------------------\n");
            found = true;
        }
        if (!found)
        {
            Console.WriteLine("Agenda vazia.");
        }
    }
    static void modifyContact(List<Contact> listContact)
    {
        string name, opt;
        bool found = false;
        Console.WriteLine("Informe o nome do contato que deseja alterar os dados:");
        name = Console.ReadLine();
        for (int i = listContact.Count - 1; i >= 0; i--)
        {
            if (listContact[i].Name == name)
            {
                found = true;
                if (found)
                {
                    //////// Alterar Nome do contato
                    Console.WriteLine($"Deseja alterar o Nome do contato?(Nome atual: {listContact[i].Name})\n[S - Sim][Outra tecla - Não]");
                    opt = Console.ReadLine();
                    if (opt.ToLower() == "s")
                    {
                        Console.WriteLine("Informe o nome desejado:");
                        listContact[i].Name = Console.ReadLine();
                        opt = "";
                    }
                    //////// Alterar Email do contato
                    Console.WriteLine($"Deseja alterar o Email do contato?(Email atual: {listContact[i].Email})\n[S - Sim][Outra tecla - Não]");
                    opt = Console.ReadLine();
                    if (opt.ToLower() == "s")
                    {
                        Console.WriteLine("Informe o Email desejado:");
                        listContact[i].Email = Console.ReadLine();
                        opt = "";
                    }
                    //////// Alterar Endereço do contato
                    Console.WriteLine("=> Endereço atual <=");
                    Console.WriteLine(listContact[i].Address.ToString());
                    Console.WriteLine($"Deseja alterar o Endereço do contato?\n[S - Sim][Outra tecla - Não]");
                    opt = Console.ReadLine();
                    if (opt.ToLower() == "s")
                    {
                        listContact[i].Address = createAddress();
                        opt = "";
                    }
                    //////// Alterar Telefones do contato
                    Console.WriteLine(listContact[i].printPhones());
                    Console.WriteLine($"Deseja alterar o(s) telefone(s) do contato?\n[S - Sim][Outra tecla - Não]");
                    opt = Console.ReadLine();
                    if (opt.ToLower() == "s")
                    {
                        int phonequantity = 0;
                        string type, number;
                        List<Phone> temp_listphone = new();
                        Phone temp_phone;
                        do
                        {
                            Console.WriteLine("Informe o tipo de contato (Fixo/Celular/etc)");
                            type = Console.ReadLine();
                            Console.WriteLine("Informe o número do telefone:");
                            number = Console.ReadLine();
                            temp_phone = new Phone(number, type.ToUpper());
                            temp_listphone.Add(temp_phone);
                            if (phonequantity < 1)
                            {
                                Console.WriteLine("Deseja inserir mais um telefone?\n[S - Sim][Qualquer outa tecla para Não]");
                                opt = Console.ReadLine();
                            }
                            phonequantity++;
                        } while (opt == "s" && phonequantity < 2);
                        listContact[i].Phone = temp_listphone;
                        opt = "";
                    }
                }
            }
        }
        if (!found)
        {
            Console.WriteLine("Contato não localizado.");
        }
    }
    static void saveToFile(List<Contact> listContact)
    {
        string path = @"C:\Dados\", file = "agenda.txt";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        Console.WriteLine("Esse processo salvará um arquivo na pasta C:\\Dados\\agenda.txt");
        StreamWriter filecontent = new(path + file);
        filecontent.WriteLine("nome;email;cep;uf;cidade;endereco;numero;bairro;telefones");
        foreach (var item in listContact)
        {
            filecontent.WriteLine(item.printToFile());
        }
        Console.WriteLine("Arquivo gerado com sucesso.");
        filecontent.Close();
    }
    static List<Contact> importFile()
    {
        List<Contact> templista = new();
        string path = @"C:\Dados\", file = "agenda.txt";
        string[] content;
        foreach (string item in File.ReadLines(path + file))
        {
            if (item.Split(';')[0] != "nome")
            {
                templista.Add(importAgenda(item.Split(';'))); // Separo o conteudo do arquivo para ser consumido e ser criado um objeto Contact
                // em seguida eu adiciono ele a uma lista
            }
        }
        return templista; // retorno essa lista para a Main e importo a minha agenda
    }
    static Contact importAgenda(string[] content)
    {
        // Nesse método eu leio o arquivo e salvo os valores em um array separado
        // depois trato eles um a um e finalizo criando a classe Contact, a qual eu retorno na funcao
        List<Contact> temp_listContact = new();
        Address temp_address;
        Contact temp_contact;
        // filecontent.WriteLine("nome;email;cep;uf;cidade;endereco;numero;bairro;telefones");
        //         array content    0    1    2   3   4        5      6     7      8
        string name, email;
        name = content[0];
        email = content[1];

        string postalcode, state, city, street, neighborhood;
        int number;
        postalcode = content[2];
        state = content[3];
        city = content[4];
        street = content[5];
        number = int.Parse(content[6]);
        neighborhood = content[7];
        temp_address = new Address(postalcode, state.ToUpper(), city.ToUpper(), street.ToUpper(), neighborhood.ToUpper(), number);

        string type, phonenumber;
        int phonequantity = 1;
        string[] ph1 = new string[9];
        List<Phone> temp_listphone = new();
        Phone temp_phone;
        phonenumber = content[8];
        ph1 = content[8].Split('|');

        if (ph1.Length > 3)
        {
            phonequantity = 2;
        }
        while (phonequantity > 0)
        {
            if (phonequantity == 2)
            {
                type = ph1[3];
                phonenumber = ph1[4];
            }
            else
            {
                type = ph1[1];
                phonenumber = ph1[2];
            }
            temp_phone = new Phone(phonenumber, type.ToUpper());
            temp_listphone.Add(temp_phone);
            phonequantity--;
        }
        temp_contact = new Contact(name, email, temp_address, temp_listphone);
        return temp_contact;
    }
}