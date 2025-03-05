using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
    public static class clCardapioExt
    {
        public static bllRetorno ValidarRegras(this Cardapio _cardapio,
            bool _insersao)
        {
            if (_cardapio.IdAnimal <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo ANIMAL deve ser selecionado!");
            }
            else if (_cardapio.NEM <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo NEM deve ser calculado!");
            }
            else if (_cardapio.DtCardapio == DateTime.Parse("01/01/1910"))
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo DATA DO CARDÁPIO deve ser preenchido!");
            }
            else if (Funcoes.Funcoes.ConvertePara.Bool(_cardapio.Lactante) &&
                    !((_cardapio.LactacaoSemanas >= 1) && (_cardapio.LactacaoSemanas <= 7)))
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo SEMANAS DE LACTAÇÃO deve ser preenchido!");
            }
            else if (Funcoes.Funcoes.ConvertePara.Bool(_cardapio.Lactante) &&
                    (_cardapio.LactacaoSemanas > 7))
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo SEMANAS DE LACTAÇÃO NÃO Necessita ser Preenchido!");
            }
            else if (Funcoes.Funcoes.ConvertePara.Bool(_cardapio.Lactante) &&
                    (_cardapio.NrFilhotes <= 0))
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo NÚMERO DE FILHOTES deve ser preenchido!");
            }
            else if (_cardapio.Descricao == "")
            {
                return bllRetorno.GeraRetorno(false, 
                    "Campo DESCRIÇÃO deve ser preenchido!!!");
            }

            if (_cardapio.FatorEnergia <= 0)
            {
                _cardapio.FatorEnergia = null;
            }
            if (_cardapio.EmCardapio <= 0)
            {
                _cardapio.EmCardapio = null;
            }
            if (_cardapio.IdDieta <= 0)
            {
                _cardapio.IdDieta = null;
            }

            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }

        public static bllRetorno ValidarRegrasCopia(this Cardapio _cardapio,
            bool _insersao)
        {
            if (_cardapio.DtCardapio == DateTime.Parse("01/01/1910"))
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo DATA DO CARDÁPIO deve ser preenchido!");
            }
            else if (_cardapio.Descricao == "")
            {
                return bllRetorno.GeraRetorno(false,
                    "Campo DESCRIÇÃO deve ser preenchido!!!");
            }

            if (_cardapio.IdAnimal <= 0)
            {
                _cardapio.IdAnimal = null;
            }
            if (_cardapio.FatorEnergia <= 0)
            {
                _cardapio.FatorEnergia = null;
            }
            if (_cardapio.EmCardapio <= 0)
            {
                _cardapio.EmCardapio = null;
            }
            if (_cardapio.IdDieta <= 0)
            {
                _cardapio.IdDieta = null;
            }

            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }

        private static bool RegistroDuplicado(Cardapio _cardapio, bool? _ativo)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(
                @"SET DATEFORMAT dmy 

                  Select    Count(IdCardapio) Total
                  From      Cardapio
                  Where     (IdAnimal = {0}) And (DtCardapio = '{1}') And
                            (Ativo = {2})", _cardapio.IdAnimal,
                _cardapio.DtCardapio.ToString("dd/MM/yyyy"), (_ativo.Value ? 1 : 0));

            int ret = crud.ExecutarComandoTipoInteiro(_sql);

            return Funcoes.Funcoes.ConvertePara.Bool(ret);
        }
    }
}
