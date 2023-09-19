namespace Modelo
{
    public class Listarateiopremio
    {
        public Listarateiopremio(string descricaoFaixa, int faixa, int numeroDeGanhadores, double valorPremio)
        {
            this.descricaoFaixa = descricaoFaixa;
            this.faixa = faixa;
            this.numeroDeGanhadores = numeroDeGanhadores;
            this.ValorPremio = valorPremio;
        }
        public Listarateiopremio()
        {
            this.descricaoFaixa = descricaoFaixa;
            this.faixa = faixa;
            this.numeroDeGanhadores = numeroDeGanhadores;
            this.ValorPremio = ValorPremio;
        }
        public string descricaoFaixa { get; set; }
        public int faixa { get; set; }
        public int numeroDeGanhadores { get; set; }
        public double ValorPremio { get; set; }
    }
}