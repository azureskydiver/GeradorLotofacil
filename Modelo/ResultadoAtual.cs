using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
   public class ResultadoAtual
    {
        public ResultadoAtual(int idLoteria, bool acumulado, string dataApuracao, string dataProximoConcurso, string[] dezenasSorteadasOrdemSorteio,
          bool exibirDetalhamentoPorCidade, object id, int indicadorConcursoEspecial, string[] listaDezenas, object listaDezenasSegundoSorteio,
          object[] listaMunicipioUFGanhadores, Listarateiopremio[] listaRateioPremio, object listaResultadoEquipeEsportiva, string localSorteio,
          string nomeMunicipioUFSorteio, string nomeTimeCoracaoMesSorte, int numero, int numeroConcursoAnterior,
          int numeroConcursoFinal_0_5, int numeroConcursoProximo, int numeroJogo, string observacao, object premiacaoContingencia,
          string tipoJogo, int tipoPublicacao, bool ultimoConcurso, float valorArrecadado, float valorAcumuladoConcurso_0_5,
          float valorAcumuladoConcursoEspecial, float valorAcumuladoProximoConcurso, float valorEstimadoProximoConcurso,
          float valorSaldoReservaGarantidora, float valorTotalPremioFaixaUm)
        {
            this.idLoteria = idLoteria;
            this.acumulado = acumulado;
            this.dataApuracao = dataApuracao;
            this.dataProximoConcurso = dataProximoConcurso;
            this.dezenasSorteadasOrdemSorteio = dezenasSorteadasOrdemSorteio;
            this.exibirDetalhamentoPorCidade = exibirDetalhamentoPorCidade;
            this.id = id;
            this.indicadorConcursoEspecial = indicadorConcursoEspecial;
            this.listaDezenas = listaDezenas;
            this.listaDezenasSegundoSorteio = listaDezenasSegundoSorteio;
            this.listaMunicipioUFGanhadores = listaMunicipioUFGanhadores;
            this.listaRateioPremio = listaRateioPremio;
            this.listaResultadoEquipeEsportiva = listaResultadoEquipeEsportiva;
            this.localSorteio = localSorteio;
            this.nomeMunicipioUFSorteio = nomeMunicipioUFSorteio;
            this.nomeTimeCoracaoMesSorte = nomeTimeCoracaoMesSorte;
            this.numero = numero;
            this.numeroConcursoAnterior = numeroConcursoAnterior;
            this.numeroConcursoFinal_0_5 = numeroConcursoFinal_0_5;
            this.numeroConcursoProximo = numeroConcursoProximo;
            this.numeroJogo = numeroJogo;
            this.observacao = observacao;
            this.premiacaoContingencia = premiacaoContingencia;
            this.tipoJogo = tipoJogo;
            this.tipoPublicacao = tipoPublicacao;
            this.ultimoConcurso = ultimoConcurso;
            this.valorArrecadado = valorArrecadado;
            this.valorAcumuladoConcurso_0_5 = valorAcumuladoConcurso_0_5;
            this.valorAcumuladoConcursoEspecial = valorAcumuladoConcursoEspecial;
            this.valorAcumuladoProximoConcurso = valorAcumuladoProximoConcurso;
            this.valorEstimadoProximoConcurso = valorEstimadoProximoConcurso;
            this.valorSaldoReservaGarantidora = valorSaldoReservaGarantidora;
            this.valorTotalPremioFaixaUm = valorTotalPremioFaixaUm;

        }

        public ResultadoAtual()
        {
            this.idLoteria = idLoteria;
            this.acumulado = acumulado;
            this.dataApuracao = dataApuracao;
            this.dataProximoConcurso = dataProximoConcurso;
            this.dezenasSorteadasOrdemSorteio = dezenasSorteadasOrdemSorteio;
            this.exibirDetalhamentoPorCidade = exibirDetalhamentoPorCidade;
            this.id = id;
            this.indicadorConcursoEspecial = indicadorConcursoEspecial;
            this.listaDezenas = listaDezenas;
            this.listaDezenasSegundoSorteio = listaDezenasSegundoSorteio;
            this.listaMunicipioUFGanhadores = listaMunicipioUFGanhadores;
            this.listaRateioPremio = listaRateioPremio;
            this.listaResultadoEquipeEsportiva = listaResultadoEquipeEsportiva;
            this.localSorteio = localSorteio;
            this.nomeMunicipioUFSorteio = nomeMunicipioUFSorteio;
            this.nomeTimeCoracaoMesSorte = nomeTimeCoracaoMesSorte;
            this.numero = numero;
            this.numeroConcursoAnterior = numeroConcursoAnterior;
            this.numeroConcursoFinal_0_5 = numeroConcursoFinal_0_5;
            this.numeroConcursoProximo = numeroConcursoProximo;
            this.numeroJogo = numeroJogo;
            this.observacao = observacao;
            this.premiacaoContingencia = premiacaoContingencia;
            this.tipoJogo = tipoJogo;
            this.tipoPublicacao = tipoPublicacao;
            this.ultimoConcurso = ultimoConcurso;
            this.valorArrecadado = valorArrecadado;
            this.valorAcumuladoConcurso_0_5 = valorAcumuladoConcurso_0_5;
            this.valorAcumuladoConcursoEspecial = valorAcumuladoConcursoEspecial;
            this.valorAcumuladoProximoConcurso = valorAcumuladoProximoConcurso;
            this.valorEstimadoProximoConcurso = valorEstimadoProximoConcurso;
            this.valorSaldoReservaGarantidora = valorSaldoReservaGarantidora;
            this.valorTotalPremioFaixaUm = valorTotalPremioFaixaUm;

        }



        public int idLoteria { get; set; }
        public bool acumulado { get; set; }
        public string dataApuracao { get; set; }
        public string dataProximoConcurso { get; set; }
        public string[] dezenasSorteadasOrdemSorteio { get; set; }
        public bool exibirDetalhamentoPorCidade { get; set; }
        public object id { get; set; }
        public int indicadorConcursoEspecial { get; set; }
        public string[] listaDezenas { get; set; }
        public object listaDezenasSegundoSorteio { get; set; }
        public object[] listaMunicipioUFGanhadores { get; set; }
        public Listarateiopremio[] listaRateioPremio { get; set; }
        public object listaResultadoEquipeEsportiva { get; set; }
        public string localSorteio { get; set; }
        public string nomeMunicipioUFSorteio { get; set; }
        public string nomeTimeCoracaoMesSorte { get; set; }
        public int numero { get; set; }
        public int numeroConcursoAnterior { get; set; }
        public int numeroConcursoFinal_0_5 { get; set; }
        public int numeroConcursoProximo { get; set; }
        public int numeroJogo { get; set; }
        public string observacao { get; set; }
        public object premiacaoContingencia { get; set; }
        public string tipoJogo { get; set; }
        public int tipoPublicacao { get; set; }
        public bool ultimoConcurso { get; set; }
        public float valorArrecadado { get; set; }
        public float valorAcumuladoConcurso_0_5 { get; set; }
        public float valorAcumuladoConcursoEspecial { get; set; }
        public float valorAcumuladoProximoConcurso { get; set; }
        public float valorEstimadoProximoConcurso { get; set; }
        public float valorSaldoReservaGarantidora { get; set; }
        public float valorTotalPremioFaixaUm { get; set; }
    }
}
