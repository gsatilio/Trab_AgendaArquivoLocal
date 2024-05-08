using System.Collections.Generic;
using System.IO;
using System.Runtime.Intrinsics.X86;
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
                    reorganizeList(listContact);
                    break;
                case 2:
                    removeContact(listContact);
                    reorganizeList(listContact);
                    break;
                case 3:
                    printAll(listContact);
                    break;
                case 4:
                    findContact(listContact);
                    break;
                case 5:
                    modifyContact(listContact);
                    reorganizeList(listContact);
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
        string postalcode, state, city, street, neighborhood, number;
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
        number = Console.ReadLine();


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
                Console.WriteLine("Deseja inserir mais um telefone?\n[S - Sim][Qualquer outra tecla para Não]");
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
        if (emptyAgenda(listContact))
        {
            Console.WriteLine("Agenda vazia!");
        }
        else
        {
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
    }
    static void findContact(List<Contact> listContact)
    {
        string name;
        bool found = false;
        if (emptyAgenda(listContact))
        {
            Console.WriteLine("Agenda vazia!");
        }
        else
        {
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
    }
    static void printAll(List<Contact> listContact)
    {
        if (emptyAgenda(listContact))
        {
            Console.WriteLine("Agenda vazia!");
        }
        else
        {
            for (int i = listContact.Count - 1; i >= 0; i--)
            {
                Console.WriteLine(listContact[i].ToString());
                Console.WriteLine("------------------------\n");
            }
        }
    }
    static void modifyContact(List<Contact> listContact)
    {
        string name, opt;
        bool found = false;
        if (emptyAgenda(listContact))
        {
            Console.WriteLine("Agenda vazia!");
        }
        else
        {
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
                                    Console.WriteLine("Deseja inserir mais um telefone?\n[S - Sim][Qualquer outra tecla para Não]");
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
    }
    static void saveToFile(List<Contact> listContact)
    {
        string path = @"C:\Dados\", file = "agenda.txt";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        if (emptyAgenda(listContact))
        {
            Console.WriteLine("Agenda vazia!");
        }
        else
        {
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
    static List<Contact> importFile()
    {
        List<Contact> templista = new();
        string path = @"C:\Dados\", file = "agenda.txt";
        string[] content;
        if (File.Exists(path + file))
        {
            foreach (string item in File.ReadLines(path + file))
            {
                if (item.Split(';')[0] != "nome")
                {
                    templista.Add(importAgenda(item.Split(';'))); // Separo o conteudo do arquivo para ser consumido e ser criado um objeto Contact
                                                                  // em seguida eu adiciono ele a uma lista
                }
            }
        }
        else
        {
            Console.WriteLine($"Arquivo {path}{file} inexistente!");
        }
        Console.WriteLine("Arquivo carregado com sucesso!");
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

        string postalcode, state, city, street, neighborhood, number;
        postalcode = content[2];
        state = content[3];
        city = content[4];
        street = content[5];
        number = content[6];
        neighborhood = content[7];
        temp_address = new Address(postalcode, state.ToUpper(), city.ToUpper(), street.ToUpper(), neighborhood.ToUpper(), number);

        string type, phonenumber;
        int phonequantity = 1;
        string[] phonenumberaux = new string[9];
        List<Phone> temp_listphone = new();
        Phone temp_phone;
        phonenumber = content[8];
        phonenumberaux = content[8].Split('|');

        if (phonenumberaux.Length > 3)
        {
            phonequantity = 2;
        }
        while (phonequantity > 0)
        {
            if (phonequantity == 2)
            {
                type = phonenumberaux[3];
                phonenumber = phonenumberaux[4];
            }
            else
            {
                type = phonenumberaux[1];
                phonenumber = phonenumberaux[2];
            }
            temp_phone = new Phone(phonenumber, type.ToUpper());
            temp_listphone.Add(temp_phone);
            phonequantity--;
        }
        temp_contact = new Contact(name, email, temp_address, temp_listphone);
        return temp_contact;
    }
    static bool emptyAgenda(List<Contact> listContact)
    {
        if (listContact.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    static void reorganizeList(List<Contact> listContact)
    {
        // método para reorganizar a lista de forma Name Ascendente
        List<Contact> listContactAux = new(); // lista temporaria para armazenar o elemento substituido
        int compare = 0;
        for (int i = 0; i < listContact.Count; i++) // primeiro laço para pegar o primeiro elemento da lista
        {
            for (int j = i+1; j < listContact.Count; j++) // segundo laço para comparar esse elemento com os demais elementos da lista
            {
                if (listContact[j].Name != null) // enquanto elemento próximo não for nulo
                {
                    // comparo o elemento próximo com o elemento atual. se for menor que zero, entao o atual recebe o próximo
                    compare = String.Compare(listContact[j].Name, listContact[i].Name, comparisonType: StringComparison.OrdinalIgnoreCase);
                    if(compare < 0)
                    {
                        listContactAux.Add(listContact[i]); // adiciono na auxiliar o elemento atual
                        listContact[i] = listContact[j];    // elemento atual recebe o próximo
                        listContact[j] = listContactAux[0]; // elemento próximo recebe o auxiliar (antigo atual)
                        listContactAux.RemoveAt(0);         // removo o elemento do auxiliar, para ser usado novamente no outro laço se preciso
                    }
                }
            }
        }
    }
}