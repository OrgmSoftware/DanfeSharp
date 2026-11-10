using DanfeSharp.Modelo;

namespace DanfeSharp.Blocos
{
    internal class BlocoTotalIcmsIpi : BlocoBase
    {
        public BlocoTotalIcmsIpi(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            var c = viewModel.CalculoImposto;

            AdicionarLinhaCampos()
                .ComCampoNumerico("BASE DE CÁLCULO DO ICMS", c.BaseCalculoIcms)
                .ComCampoNumerico("VALOR DO ICMS", c.ValorIcms)
                .ComCampoNumerico("BASE DE CÁLCULO DO ICMS ST", c.BaseCalculoIcmsSt)
                .ComCampoNumerico("VALOR DO ICMS ST", c.ValorIcmsSt)
                .ComCampoNumerico("VALOR DO IPI", c.ValorIpi)
                .ComLargurasIguais();

            AdicionarLinhaCampos()
                .ComCampoNumerico("VALOR DO FCP", c.ValorFcp)
                .ComCampoNumerico("VALOR DO FCP RETIDO POR ST", c.ValorFcpSt)
                .ComCampoNumerico("VALOR DO DIFAL NA UF DE DESTINO", c.ValorIcmsUfDestino ?? c.vICMSUFDest)
                .ComCampoNumerico("VALOR DO FCP NA UF DE DESTINO", c.ValorFcpUfDestino ?? c.vFCPUFDest)
                .ComLargurasIguais();

            AdicionarLinhaCampos()
                .ComCampoNumerico("BC DO ICMS MONOFÁSICO", c.BaseCalculoIcmsMonofasico)
                .ComCampoNumerico("VALOR DO ICMS MONOFÁSICO", c.ValorIcmsMonofasico)
                .ComCampoNumerico("BC DO ICMS MONOFÁSICO POR RETENÇÃO", c.BaseCalculoIcmsMonofasicoRetencao)
                .ComCampoNumerico("VALOR DO ICMS MONOFÁSICO POR RETENÇÃO", c.ValorIcmsMonofasicoRetencao)
                .ComLargurasIguais();
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override string Cabecalho => "TOTAL DO ICMS / IPI";
    }
}
