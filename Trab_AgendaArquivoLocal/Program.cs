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
            Console.WriteLine("Agenda");
            Console.WriteLine("1 - Criar Contato");
            Console.WriteLine("2 - Remover Contato");
            Console.WriteLine("3 - Mostrar todos os Contatos");
            Console.WriteLine("4 - Pesquisar Contato");
            Console.WriteLine("5 - Modificar Dados do Contato");
            Console.WriteLine("6 - Modificar Telefones do Contato");
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
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        } while (opt != 0);
        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
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

        Address temp_address = new Address(postalcode, state, city, street, neighborhood, number);

        return temp_address;
    }
    static List<Phone> createPhone()
    {
        string type, number, opt="";
        int phonequantity = 0;
        List<Phone> temp_listphone = new();
        Phone temp_phone;

        do
        {
            Console.WriteLine("Informe o tipo de contato (Fixo/Celular/etc)");
            type = Console.ReadLine();
            Console.WriteLine("Informe o número do telefone:");
            number = Console.ReadLine();
            temp_phone = new Phone(number, type);
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
        } else
        {
            Console.WriteLine("Contato não localizado.");
        }
    }
}