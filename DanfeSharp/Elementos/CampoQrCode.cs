using DanfeSharp.Graphics;
using System;
using System.Drawing;

namespace DanfeSharp
{
    internal class CampoQrCode : ElementoBase
    {
        public string Cabecalho { get; set; } = "QR CODE";
        public string Conteudo { get; set; }

        public CampoQrCode(string cabecalho, string conteudo, Estilo estilo) : base(estilo)
        {
            Cabecalho = cabecalho ?? "QR CODE";
            Conteudo = conteudo;
        }

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);

            float padding = 1.0f;
            float alturaCabecalho = Estilo.FonteCampoCabecalho.AlturaLinha;
            var rCabecalho = new RectangleF(X + padding, Y + padding, Width - 2 * padding, alturaCabecalho);
            if (!string.IsNullOrWhiteSpace(Cabecalho))
            {
                gfx.DrawString(Cabecalho.ToUpper(), rCabecalho, Estilo.FonteCampoCabecalho, AlinhamentoHorizontal.Centro, AlinhamentoVertical.Topo);
            }

            if (!string.IsNullOrWhiteSpace(Conteudo))
            {
                try
                {
                    float topDisponivel = rCabecalho.Bottom + 0.5f;
                    float hDisponivel = Height - (topDisponivel - Y) - padding;
                    float wDisponivel = Width - 2 * padding;

                    float ladoQr = Math.Min(wDisponivel, hDisponivel);
                    if (ladoQr > 5)
                    {
                        float qrX = X + (Width - ladoQr) / 2f;
                        float qrY = topDisponivel + (hDisponivel - ladoQr) / 2f;

                        var qrGen = new QRCodeGenerator();
                        var qrData = qrGen.CreateQrCode(Conteudo, QRCodeGenerator.ECCLevel.M);

                        int modulos = qrData.ModuleMatrix.Count;
                        if (modulos > 0)
                        {
                            float tamModulo = ladoQr / modulos;

                            for (int y = 0; y < modulos; y++)
                            {
                                for (int x = 0; x < modulos; x++)
                                {
                                    if (qrData.ModuleMatrix[y][x])
                                    {
                                        gfx.DrawRectangle(qrX + x * tamModulo, qrY + y * tamModulo, tamModulo, tamModulo);
                                    }
                                }
                            }
                            gfx.Fill();
                        }
                    }
                }
                catch
                {
                    // Falha silenciosa defensiva
                }
            }
        }
    }
}
