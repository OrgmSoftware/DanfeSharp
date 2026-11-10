using System;

namespace DanfeSharp.Modelo
{
    public class ProdutoViewModel
    {
        public String Codigo { get; set; }
        public String InformacoesAdicionais { get; set; }
        public String Descricao { get; set; }
        public String Ncm { get; set; }
        public String OCst { get; set; }
        public int Cfop { get; set; }
        public String Unidade { get; set; }
        public double Quantidade { get; set; }
        public double ValorUnitario { get; set; }
        public double ValorTotal { get; set; }
        public double BaseIcms { get; set; }
        public double ValorIcms { get; set; }
        public double AliquotaIcms { get; set; }
        public double? ValorIpi { get; set; }
        public double? AliquotaIpi { get; set; }
        public double? ValorAproximadoTributos { get; set; }

        // Campos Reforma Tributária NOV2026
        public double BaseIbsCbs { get; set; }
        public double AliquotaCbs { get; set; }
        public double ValorCbs { get; set; }
        public double AliquotaIbsUf { get; set; }
        public double ValorIbsUf { get; set; }
        public double AliquotaIbsMun { get; set; }
        public double ValorIbsMun { get; set; }
        public double BaseImpostoSeletivo { get; set; }
        public double AliquotaImpostoSeletivo { get; set; }
        public double ValorImpostoSeletivo { get; set; }
        public double BaseIpi { get; set; }
        public string CClassTrib { get; set; }

        public ProdutoViewModel()
        {
            AliquotaIpi = null;
            ValorIpi = null;
        }

        public String DescricaoCompleta
        {
            get
            {
                String descriCaoCompleta = Descricao;

                if (!String.IsNullOrWhiteSpace(InformacoesAdicionais))
                {
                    descriCaoCompleta += "\r\n" + InformacoesAdicionais;
                }
                
                return descriCaoCompleta;
            }
        }
    }
}
