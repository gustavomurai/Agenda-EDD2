using System;

namespace AgendaApp
{
    public class Data
    {
        public int Dia { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }

        public Data() { }

        public Data(int dia, int mes, int ano)
        {
            SetData(dia, mes, ano);
        }

        public void SetData(int dia, int mes, int ano)
        {
            // Validação básica: ajusta para valores válidos
            if (ano < 1) ano = 1;
            if (mes < 1 || mes > 12) mes = 1;
            int maxDia = DateTime.DaysInMonth(ano, mes);
            if (dia < 1) dia = 1;
            if (dia > maxDia) dia = maxDia;

            Dia = dia;
            Mes = mes;
            Ano = ano;
        }

        public override string ToString()
        {
            return $"{Dia:D2}/{Mes:D2}/{Ano:D4}";
        }

        public DateTime ToDateTime()
        {
            return new DateTime(Ano, Mes, Dia);
        }
    }
}
