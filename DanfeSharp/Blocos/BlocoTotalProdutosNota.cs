using DanfeSharp.Modelo;

namespace DanfeSharp.Blocos
{
    internal class BlocoTotalProdutosNota : BlocoBase
    {
        public BlocoTotalProdutosNota(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            var c = viewModel.CalculoImposto;

            AdicionarLinhaCampos()
                .ComCampoNumerico("VALOR TOTAL DOS PRODUTOS", c.ValorTotalProdutos)
                .ComCampoNumerico("VALOR DO FRETE", c.ValorFrete)
                .ComCampoNumerico("VALOR DO SEGURO", c.ValorSeguro)
                .ComCampoNumerico("DESCONTO", c.Desconto)
                .ComCampoNumerico("OUTRAS DESPESAS", c.OutrasDespesas)
                .ComLargurasIguais();

            AdicionarLinhaCampos()
                .ComCampoNumerico("VALOR TOTAL DA NOTA", c.ValorTotalNota)
                .ComLarguras(100);
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override string Cabecalho => "TOTAL DOS PRODUTOS E TOTAL DA NOTA";
    }
}
