namespace AgendaApp
{
    public class Telefone
    {
        public string Tipo { get; set; }       // ex: "Celular", "Residencial"
        public string Numero { get; set; }     // ex: "11988887777"
        public bool Principal { get; set; }

        public Telefone() { }

        public Telefone(string tipo, string numero, bool principal = false)
        {
            Tipo = tipo;
            Numero = numero;
            Principal = principal;
        }

        public override string ToString()
        {
            return $"{(Principal ? "[Principal] " : "")}{Tipo}: {Numero}";
        }
    }
}
