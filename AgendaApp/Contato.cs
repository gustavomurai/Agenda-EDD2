using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgendaApp
{
    public class Contato
    {
        public string Email { get; set; }
        public string Nome { get; set; }
        public Data DtNasc { get; set; }

        // lista interna de telefones (encapsulada)
        private List<Telefone> telefones = new List<Telefone>();
        public IReadOnlyList<Telefone> Telefones => telefones.AsReadOnly();

        public Contato() { }

        public Contato(string nome, string email, Data dtNasc)
        {
            Nome = nome;
            Email = email;
            DtNasc = dtNasc;
        }

        // Retorna a idade calculada (considerando mês/dia)
        public int GetIdade()
        {
            if (DtNasc == null) return 0;
            var hoje = DateTime.Today;
            var nasc = DtNasc.ToDateTime();
            int idade = hoje.Year - nasc.Year;
            if (hoje.Month < nasc.Month || (hoje.Month == nasc.Month && hoje.Day < nasc.Day))
                idade--;
            return Math.Max(0, idade);
        }

        // Adiciona telefone; se o telefone for marcado como principal, desmarca outros
        public void AdicionarTelefone(Telefone t)
        {
            if (t == null) return;
            if (t.Principal)
            {
                foreach (var tel in telefones)
                    tel.Principal = false;
            }
            telefones.Add(t);
        }

        // Substitui lista de telefones (usado para alterar)
        public void SetTelefones(IEnumerable<Telefone> lista)
        {
            telefones.Clear();
            if (lista == null) return;
            foreach (var t in lista)
            {
                AdicionarTelefone(new Telefone(t.Tipo, t.Numero, t.Principal));
            }
        }

        // Retorna o número do telefone principal (ou string vazia)
        public string GetTelefonePrincipal()
        {
            var p = telefones.FirstOrDefault(t => t.Principal);
            return p?.Numero ?? "";
        }

        // ToString: retorna todos os dados do contato (considerando telefone principal)
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Nome: {Nome}");
            sb.AppendLine($"Email: {Email}");
            sb.AppendLine($"Data de nascimento: {(DtNasc != null ? DtNasc.ToString() : "N/A")}");
            sb.AppendLine($"Idade: {GetIdade()}");
            sb.AppendLine($"Telefone principal: {GetTelefonePrincipal()}");
            if (telefones.Count > 0)
            {
                sb.AppendLine("Telefones:");
                foreach (var tel in telefones)
                    sb.AppendLine($" - {tel}");
            }
            return sb.ToString();
        }

        // Equals: considera contato igual por EMAIL (se existir); caso contrário por nome+data
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Contato)) return false;
            var other = (Contato)obj;
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(other.Email))
                return Email.Equals(other.Email, StringComparison.OrdinalIgnoreCase);

            return !string.IsNullOrEmpty(Nome)
                && !string.IsNullOrEmpty(other.Nome)
                && Nome.Equals(other.Nome, StringComparison.OrdinalIgnoreCase)
                && DtNasc != null && other.DtNasc != null
                && DtNasc.ToString() == other.DtNasc.ToString();
        }

        public override int GetHashCode()
        {
            if (!string.IsNullOrEmpty(Email)) return Email.ToLower().GetHashCode();
            return (Nome?.ToLower().GetHashCode() ?? 0) ^ (DtNasc?.ToString().GetHashCode() ?? 0);
        }
    }
}
