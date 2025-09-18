using System.Collections.Generic;
using System.Linq;

namespace AgendaApp
{
    public class Contatos
    {
        private List<Contato> agenda = new List<Contato>();
        public IReadOnlyList<Contato> Agenda => agenda.AsReadOnly();

        // Adicionar: retorna true se adicionado; false se já existe
        public bool Adicionar(Contato c)
        {
            if (c == null) return false;
            if (agenda.Any(x => x.Equals(c))) return false;
            agenda.Add(c);
            return true;
        }

        // Pesquisar: recebe um Contato (geralmente com Email preenchido) e retorna o objeto guardado
        public Contato Pesquisar(Contato c)
        {
            if (c == null) return null;
            return agenda.FirstOrDefault(x => x.Equals(c));
        }

        // Alterar: atualiza dados (nome, email, dtNasc e telefones). Retorna true se encontrado e alterado.
        public bool Alterar(Contato c)
        {
            if (c == null) return false;
            var existing = agenda.FirstOrDefault(x => x.Equals(c));
            if (existing == null) return false;

            existing.Nome = c.Nome;
            existing.Email = c.Email;
            existing.DtNasc = c.DtNasc;
            existing.SetTelefones(c.Telefones);
            return true;
        }

        // Remover: remove pelo critério de igualdade. Retorna true se removido.
        public bool Remover(Contato c)
        {
            if (c == null) return false;
            var existing = agenda.FirstOrDefault(x => x.Equals(c));
            if (existing == null) return false;
            return agenda.Remove(existing);
        }
    }
}
