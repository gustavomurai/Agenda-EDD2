# 📒 Agenda de Contatos (Estrutura de Dados II)

Projeto desenvolvido em **C# Console Application** no **Visual Studio** para disciplina de Estrutura de Dados II.  
O sistema implementa uma **Agenda de Contatos** utilizando classes baseadas no diagrama fornecido.

---

## 👥 Integrantes
- Gustavo Murai  
- Igor Murai  

---

## 📌 Diagrama de Classes

```text
----------------------------------------------
| Data                                       |
|--------------------------------------------|
| - dia: int                                 |
| - mes: int                                 |
| - ano: int                                 |
|--------------------------------------------|
| + setData(int dia, int mes, int ano): void |
| + ToString(): String (override)            |
|   -> retornando "dd/mm/aaaa"               |
----------------------------------------------

----------------------------------------------
| Telefone                                   |
|--------------------------------------------|
| - tipo: string                             |
| - numero: string                           |
| - principal: bool                          |
----------------------------------------------

-----------------------------------------
| Contato                               |
|---------------------------------------|
| - email: string                       |
| - nome: string                        |
| - dtNasc: Data                        |
| - telefones: List<Telefone>           |
|---------------------------------------|
| + getIdade(): int                     |
| + adicionarTelefone(Telefone t): void |
| + getTelefonePrincipal(): string      |
| + ToString(): String (override)       |
|   -> retornando uma string com        |
|      todos os dados do contato        |
|      (considerando o telefone         |
|      principal)                       |
| + Equals(object obj): bool (override) |
-----------------------------------------

-----------------------------------------
| Contatos                              |
|---------------------------------------|
| - agenda: List<Contato> (readOnly)    |
|---------------------------------------|
| + adicionar(Contato c): bool          |
| + pesquisar(Contato c): Contato       |
| + alterar(Contato c): bool            |
| + remover(Contato c): bool            |
-----------------------------------------
