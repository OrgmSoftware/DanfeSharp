using DanfeSharp.Modelo;

namespace DanfeSharp.Blocos
{
    internal class BlocoIbscbs : BlocoBase
    {
        public BlocoIbscbs(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            var m = viewModel;

            AdicionarLinhaCampos()
                .ComCampoNumerico("VALOR DA CBS", m.ValorCbs)
                .ComCampoNumerico("VALOR DO IBS UF", m.ValorIbsEstadual)
                .ComCampoNumerico("VALOR DO IBS MUNICÍPIO", m.ValorIbsMunicipal)
                .ComCampoNumerico("VALOR DO IMPOSTO SELETIVO", m.ValorImpostoSeletivo)
                .ComLargurasIguais();

            AdicionarLinhaCampos()
                .ComCampoNumerico("VALOR DO IBS MONOFÁSICO", m.ValorIbsMonofasico)
                .ComCampoNumerico("VALOR DA CBS MONOFÁSICA", m.ValorCbsMonofasico)
                .ComCampoNumerico("VALOR DO IBS MONOFÁSICO POR RETENÇÃO", m.ValorIbsMonofasicoRetencao)
                .ComCampoNumerico("VALOR DA CBS MONOFÁSICA POR RETENÇÃO", m.ValorCbsMonofasicoRetencao)
                .ComLargurasIguais();
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override string Cabecalho => "TOTAL DO IBS / CBS / IS";
    }
}
