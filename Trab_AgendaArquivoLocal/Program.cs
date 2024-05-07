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
            if (listContact[i].name == name)
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
            if (listContact[i].name == name)
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
            if (listContact[i].name == name)
            {
                found = true;
                if (found)
                {
                    //////// Alterar Nome do contato
                    Console.WriteLine($"Deseja alterar o Nome do contato?(Nome atual: {listContact[i].name})\n[S - Sim][Outra tecla - Não]");
                    opt = Console.ReadLine();
                    if (opt.ToLower() == "s")
                    {
                        Console.WriteLine("Informe o nome desejado:");
                        listContact[i].name = Console.ReadLine();
                        opt = "";
                    }
                    //////// Alterar Email do contato
                    Console.WriteLine($"Deseja alterar o Email do contato?(Email atual: {listContact[i].email})\n[S - Sim][Outra tecla - Não]");
                    opt = Console.ReadLine();
                    if (opt.ToLower() == "s")
                    {
                        Console.WriteLine("Informe o Email desejado:");
                        listContact[i].email = Console.ReadLine();
                        opt = "";
                    }
                    //////// Alterar Endereço do contato
                    Console.WriteLine("=> Endereço atual <=");
                    Console.WriteLine(listContact[i].address.ToString());
                    Console.WriteLine($"Deseja alterar o Endereço do contato?\n[S - Sim][Outra tecla - Não]");
                    opt = Console.ReadLine();
                    if (opt.ToLower() == "s")
                    {
                        listContact[i].address = createAddress();
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
                        listContact[i].phone = temp_listphone;
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
}