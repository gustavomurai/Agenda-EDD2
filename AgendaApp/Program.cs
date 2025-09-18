using System;

namespace AgendaApp
{
    class Program
    {
        static Contatos contatos = new Contatos();

        static void Main(string[] args)
        {
            Console.WriteLine("=== Agenda de Contatos - Console App ===");
            int opc;
            do
            {
                ShowMenu();
                Console.Write("Escolha uma opção: ");
                if (!int.TryParse(Console.ReadLine(), out opc))
                {
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    continue;
                }
                switch (opc)
                {
                    case 0: Console.WriteLine("Saindo..."); break;
                    case 1: AdicionarContato(); break;
                    case 2: PesquisarContato(); break;
                    case 3: AlterarContato(); break;
                    case 4: RemoverContato(); break;
                    case 5: ListarContatos(); break;
                    default: Console.WriteLine("Opção inválida"); break;
                }
            } while (opc != 0);
        }

        static void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("0. Sair");
            Console.WriteLine("1. Adicionar contato");
            Console.WriteLine("2. Pesquisar contato");
            Console.WriteLine("3. Alterar contato");
            Console.WriteLine("4. Remover contato");
            Console.WriteLine("5. Listar contatos");
            Console.WriteLine();
        }

        static void AdicionarContato()
        {
            Console.WriteLine("--- Adicionar contato ---");
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Data dt = LerData();
            var c = new Contato(nome, email, dt);

            // ler telefones
            while (true)
            {
                Console.Write("Deseja adicionar um telefone? (s/n): ");
                var resp = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(resp) || resp.ToLower() != "s") break;
                Console.Write("Tipo (ex: Celular, Residencial): ");
                string tipo = Console.ReadLine();
                Console.Write("Número: ");
                string num = Console.ReadLine();
                Console.Write("É o telefone principal? (s/n): ");
                bool principal = (Console.ReadLine()?.ToLower() == "s");
                c.AdicionarTelefone(new Telefone(tipo, num, principal));
            }

            bool added = contatos.Adicionar(c);
            Console.WriteLine(added ? "Contato adicionado com sucesso." : "Contato não adicionado (já existe).");
        }

        static Data LerData()
        {
            while (true)
            {
                Console.Write("Data de nascimento (dd/mm/aaaa): ");
                string s = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(s))
                {
                    Console.WriteLine("Data vazia. Tente de novo.");
                    continue;
                }
                var parts = s.Split('/');
                if (parts.Length != 3)
                {
                    Console.WriteLine("Formato inválido. Use dd/mm/aaaa");
                    continue;
                }
                if (int.TryParse(parts[0], out int d) && int.TryParse(parts[1], out int m) && int.TryParse(parts[2], out int a))
                {
                    try
                    {
                        var dt = new Data(d, m, a);
                        return dt;
                    }
                    catch
                    {
                        Console.WriteLine("Data inválida. Tente novamente.");
                    }
                }
                else
                {
                    Console.WriteLine("Valores inválidos. Tente novamente.");
                }
            }
        }

        static void PesquisarContato()
        {
            Console.WriteLine("--- Pesquisar contato ---");
            Console.Write("Pesquisar por email (deixe vazio para pesquisar por nome): ");
            string email = Console.ReadLine();
            Contato found = null;
            if (!string.IsNullOrWhiteSpace(email))
            {
                var temp = new Contato("", email, null);
                found = contatos.Pesquisar(temp);
            }
            else
            {
                Console.Write("Nome: ");
                string nome = Console.ReadLine();
                foreach (var c in contatos.Agenda)
                {
                    if (c.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase))
                    {
                        found = c;
                        break;
                    }
                }
            }

            if (found == null) Console.WriteLine("Contato não encontrado.");
            else Console.WriteLine(found.ToString());
        }

        static void AlterarContato()
        {
            Console.WriteLine("--- Alterar contato ---");
            Console.Write("Informe o email do contato a alterar: ");
            string email = Console.ReadLine();
            var temp = new Contato("", email, null);
            var existing = contatos.Pesquisar(temp);
            if (existing == null)
            {
                Console.WriteLine("Contato não encontrado.");
                return;
            }

            Console.WriteLine("Contato encontrado:");
            Console.WriteLine(existing.ToString());

            Console.Write("Novo nome (deixe vazio para manter): ");
            string nome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nome)) existing.Nome = nome;

            Console.Write("Novo email (deixe vazio para manter): ");
            string newEmail = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newEmail)) existing.Email = newEmail;

            Console.Write("Alterar data de nascimento? (s/n): ");
            if (Console.ReadLine()?.ToLower() == "s")
            {
                existing.DtNasc = LerData();
            }

            Console.Write("Deseja alterar telefones? (s/n): ");
            if (Console.ReadLine()?.ToLower() == "s")
            {
                var c2 = new Contato(existing.Nome, existing.Email, existing.DtNasc);
                while (true)
                {
                    Console.Write("Adicionar telefone (s/n)? ");
                    if (Console.ReadLine()?.ToLower() != "s") break;
                    Console.Write("Tipo: ");
                    string tipo = Console.ReadLine();
                    Console.Write("Número: ");
                    string num = Console.ReadLine();
                    Console.Write("Principal? (s/n): ");
                    bool p = Console.ReadLine()?.ToLower() == "s";
                    c2.AdicionarTelefone(new Telefone(tipo, num, p));
                }
                contatos.Alterar(c2);
                Console.WriteLine("Contato alterado com sucesso.");
            }
            else
            {
                contatos.Alterar(existing);
                Console.WriteLine("Contato alterado com sucesso.");
            }
        }

        static void RemoverContato()
        {
            Console.WriteLine("--- Remover contato ---");
            Console.Write("Email do contato a remover: ");
            string email = Console.ReadLine();
            var temp = new Contato("", email, null);
            bool removed = contatos.Remover(temp);
            Console.WriteLine(removed ? "Contato removido." : "Contato não encontrado.");
        }

        static void ListarContatos()
        {
            Console.WriteLine("--- Lista de contatos ---");
            if (contatos.Agenda.Count == 0) Console.WriteLine("Nenhum contato");
            else
            {
                foreach (var c in contatos.Agenda)
                {
                    Console.WriteLine(c.ToString());
                    Console.WriteLine("---------------------------");
                }
            }
        }
    }
}
