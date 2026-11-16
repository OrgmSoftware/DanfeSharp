using System;
using System.Collections.Generic;
using System.Drawing;
using DanfeSharp.Graphics;
using DanfeSharp.Modelo;

namespace DanfeSharp.Blocos
{
    internal class TabelaProdutosServicos : ElementoBase
    {
        public CabecalhoBloco CabecalhoBloco { get; private set; }
        public Tabela Tabela { get; private set; }
        public DanfeViewModel ViewModel { get; private set; }

        public TabelaProdutosServicos(DanfeViewModel viewModel, Estilo estilo) : base(estilo)
        {
            ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            CabecalhoBloco = new CabecalhoBloco(estilo, "DADOS DOS PRODUTOS / SERVIÇOS");

            var ad = AlinhamentoHorizontal.Direita;
            var ac = AlinhamentoHorizontal.Centro;
            var ae = AlinhamentoHorizontal.Esquerda;

            Tabela = new Tabela(Estilo);

            if (ViewModel.VersaoDanfe == "NOV2026")
            {
                // NOVO LAYOUT REFORMA TRIBUTÁRIA (DANFE 2027)
                Tabela
                    .ComColuna(28F, ae, "DESCRIÇÃO DO PRODUTO / SERVIÇO")
                    .ComColuna(7.5F, ac, "CST /", "CFOP")
                    .ComColuna(7.5F, ac, "QTD /", "UN")
                    .ComColuna(8F, ad, "VLR UNIT")
                    .ComColuna(8F, ad, "VLR TOTAL")
                    .ComColuna(13.5F, ad, "BASES DE CÁLCULO")
                    .ComColuna(13.5F, ad, "ALÍQUOTAS")
                    .ComColuna(14F, ad, "VALOR DOS TRIBUTOS");

                Tabela.AjustarLarguraColunas();

                foreach (var p in ViewModel.Produtos)
                {
                    string desc = p.Descricao ?? "";
                    if (!string.IsNullOrWhiteSpace(p.InformacoesAdicionais))
                        desc += "\n" + p.InformacoesAdicionais;
                    string classTrib = !string.IsNullOrWhiteSpace(p.CClassTrib) ? $" [cClassTrib {p.CClassTrib}]" : "";
                    desc += $"\n[NCM {p.Ncm}]{classTrib}";

                    string cst = p.OCst ?? "000";
                    string cstCfop = $"CST.....{cst}\nCFOP..{p.Cfop}";

                    string qtdUn = $"{p.Quantidade:N2}\n[{p.Unidade}]";
                    string vlrUnit = p.ValorUnitario.ToString("N2");
                    string vlrTotal = p.ValorTotal.ToString("N2");

                    string bc = $"ICMS ........ {p.BaseIcms:N2}\nIBS/CBS ... {p.BaseIbsCbs:N2}\nIS ............. {p.BaseImpostoSeletivo:N2}\nIPI ............ {p.BaseIpi:N2}";
                    string aliqIpi = p.AliquotaIpi.HasValue ? p.AliquotaIpi.Value.ToString("N2") : "0,00";
                    string aliq = $"ICMS {p.AliquotaIcms:N2}% / CBS {p.AliquotaCbs:N2}%\nIBS UF {p.AliquotaIbsUf:N2}% / IPI {aliqIpi}%\nIBS MUN {p.AliquotaIbsMun:N2}% / IS {p.AliquotaImpostoSeletivo:N2}%";

                    string vlrIpi = p.ValorIpi.HasValue ? p.ValorIpi.Value.ToString("N2") : "0,00";
                    string trib = $"ICMS {p.ValorIcms:N2} / CBS {p.ValorCbs:N2}\nIBS UF {p.ValorIbsUf:N2} / IPI {vlrIpi}\nIBS MUN {p.ValorIbsMun:N2} / IS {p.ValorImpostoSeletivo:N2}";

                    var linha = new List<String>
                    {
                        desc,
                        cstCfop,
                        qtdUn,
                        vlrUnit,
                        vlrTotal,
                        bc,
                        aliq,
                        trib
                    };

                    Tabela.AdicionarLinha(linha);
                }
            }
            else
            {
                // LAYOUT CLÁSSICO LEGADO
                String cabecalho4 = ViewModel.Emitente.CRT == "3" ? "O/CST" : "O/CSOSN";

                if (ViewModel.IsRetrato)
                { 
                    Tabela
                    .ComColuna(8.5f, ac, "CÓDIGO", "PRODUTO")
                    .ComColuna(0, ae, "DESCRIÇÃO DO PRODUTO / SERVIÇO")
                    .ComColuna(5.6F, ac, "NCM/SH")
                    .ComColuna(3.9F, ac, cabecalho4)
                    .ComColuna(3.5F, ac, "CFOP")
                    .ComColuna(3.25F, ac, "UN")
                    .ComColuna(6F, ad, "QUANTI.")
                    .ComColuna(6F, ad, "VALOR", "UNIT.")
                    .ComColuna(6F, ad, "VALOR", "TOTAL")
                    .ComColuna(6F, ad, "B CÁLC", "ICMS")
                    .ComColuna(5, ad, "VALOR", "ICMS")
                    .ComColuna(5, ad, "VALOR", "IPI")
                    .ComColuna(3.5F, ad, "ALIQ.", "ICMS")
                    .ComColuna(3.5F, ad, "ALIQ.", "IPI");
                }
                else
                {
                    Tabela
                    .ComColuna(8.1f, ac, "CÓDIGO PRODUTO")
                    .ComColuna(0, ae, "DESCRIÇÃO DO PRODUTO / SERVIÇO")
                    .ComColuna(5.5F, ac, "NCM/SH")
                    .ComColuna(3.1F, ac, cabecalho4)
                    .ComColuna(3.1F, ac, "CFOP")
                    .ComColuna(3F, ac, "UN")
                    .ComColuna(5.25F, ad, "QUANTI.")
                    .ComColuna(5.6F, ad, "VALOR UNIT.")
                    .ComColuna(5.6F, ad, "VALOR TOTAL")
                    .ComColuna(5.6F, ad, "B CÁLC ICMS")
                    .ComColuna(5.6F, ad, "VALOR ICMS")
                    .ComColuna(5.6F, ad, "VALOR IPI")
                    .ComColuna(3F, ad, "ALIQ.", "ICMS")
                    .ComColuna(3F, ad, "ALIQ.", "IPI");
                }

                Tabela.AjustarLarguraColunas();

                foreach (var p in ViewModel.Produtos)
                {
                    var linha = new List<String>
                    {
                        p.Codigo,
                        p.DescricaoCompleta,
                        p.Ncm,
                        p.OCst,
                        p.Cfop.Formatar("N0"),
                        p.Unidade,
                        p.Quantidade.Formatar(),
                        p.ValorUnitario.Formatar(),
                        p.ValorTotal.Formatar(),
                        p.BaseIcms.Formatar(),
                        p.ValorIcms.Formatar(),
                        p.ValorIpi.Formatar(),
                        p.AliquotaIcms.Formatar(),
                        p.AliquotaIpi.Formatar()
                    };

                    Tabela.AdicionarLinha(linha);
                }
            }
        }

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);

            Tabela.SetPosition(RetanguloTabela.Location);
            Tabela.SetSize(RetanguloTabela.Size);
            Tabela.Draw(gfx);

            CabecalhoBloco.SetPosition(X, Y);
            CabecalhoBloco.Width = Width;
            CabecalhoBloco.Draw(gfx);    
        }

        public RectangleF RetanguloTabela => BoundingBox.CutTop(CabecalhoBloco.Height);
        public Boolean CompletamenteDesenhada => Tabela.LinhaAtual == ViewModel.Produtos.Count;
        public override bool PossuiContono => false;
    }
}
