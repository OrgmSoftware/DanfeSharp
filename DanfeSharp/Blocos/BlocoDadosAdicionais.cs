using DanfeSharp.Graphics;
using DanfeSharp.Modelo;
using DanfeSharp;
using System;

namespace DanfeSharp.Blocos
{
    internal class BlocoDadosAdicionais : BlocoBase
    {
        public const float AlturaMinima = 25;
        public const float AlturaMinimaReforma = 30;
        private CampoMultilinha _cInfComplementares;
        private FlexibleLine _Linha;
        private Campo _cReservadoFisco;
        private CampoQrCode _cQrCode;
        public const float InfComplementaresLarguraPorcentagem = 75;

        public BlocoDadosAdicionais(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            _cInfComplementares = new CampoMultilinha("INFORMAÇÕES COMPLEMENTARES", ViewModel.TextoAdicional(), estilo);
            _cReservadoFisco = new CampoMultilinha("RESERVADO AO FISCO", ViewModel.TextoAdicionalFisco(), estilo);

            _Linha = new FlexibleLine() { Height = _cInfComplementares.Height };

            if (ViewModel.VersaoDanfe == "NOV2026")
            {
                _cQrCode = new CampoQrCode("QR CODE", ViewModel.ObterConteudoQrCode(), estilo);
                _Linha.ComElemento(_cInfComplementares)
                      .ComElemento(_cReservadoFisco)
                      .ComElemento(_cQrCode)
                      .ComLarguras(60, 20, 0);
            }
            else
            {
                _Linha.ComElemento(_cInfComplementares)
                      .ComElemento(_cReservadoFisco)
                      .ComLarguras(InfComplementaresLarguraPorcentagem, 0);
            }
                        
            MainVerticalStack.Add(_Linha);           
        }

        public override float Width
        {
            get => base.Width;
            set
            {
                base.Width = value;
                // Força o ajuste da altura do InfComplementares
                if (_cInfComplementares != null && _Linha != null)
                {
                    _Linha.Width = value;
                    _Linha.Posicionar();
                    float altMin = ViewModel.VersaoDanfe == "NOV2026" ? AlturaMinimaReforma : AlturaMinima;
                    _cInfComplementares.Height = altMin;
                    _Linha.Height = _cInfComplementares.Height;
                }
            }
        }

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Base;
        public override string Cabecalho => "Dados adicionais";
    }
}
