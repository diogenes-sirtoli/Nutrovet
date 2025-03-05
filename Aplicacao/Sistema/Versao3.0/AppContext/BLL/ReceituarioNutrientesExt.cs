using DCL;
using DAL;

namespace BLL
{
    public static class clReceituarioNutrientesExt
    {
        public static bllRetorno ValidarRegras(this ReceituarioNutriente _recNutr,
            bool _insersao)
        {
            if (_recNutr.IdReceita <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, "Campo RECEITA deve ser selecionado!");
            }
            else if (_recNutr.IdNutr <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, "Campo NUTRIENTE deve ser selecionado!");
            }
            else if ((_insersao) && (RegistroDuplicado(_recNutr)))
            {
                return bllRetorno.GeraRetorno(false, "Este Nutriente já foi Cadastrado!!!");
            }

            if ((_recNutr.Dose <= 0) || (_recNutr.Dose == null))
            {
                _recNutr.Dose = 0;
            }

            if ((_recNutr.DoseMin <= 0) || (_recNutr.DoseMin == null))
            {
                _recNutr.DoseMin = 0;
            }

            if ((_recNutr.DoseMax <= 0) || (_recNutr.DoseMax == null))
            {
                _recNutr.DoseMax = 0;
            }

            if ((_recNutr.Consta <= 0) || (_recNutr.Consta == null))
            {
                _recNutr.Consta = 0;
            }

            if ((_recNutr.Falta <= 0) || (_recNutr.Falta == null))
            {
                _recNutr.Falta = 0;
            }

            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }

        private static bool RegistroDuplicado(ReceituarioNutriente _recNutr)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
                SELECT  COUNT(rn.IdNutrRec) AS Total
                FROM    ReceituarioNutrientes rn
                WHERE   (rn.IdReceita = {0}) AND (rn.IdNutr = {1})", 
                _recNutr.IdReceita, _recNutr.IdNutr);

            int reg = crud.ExecutarComandoTipoInteiro(_sql);

            bool retorno = Funcoes.Funcoes.ConvertePara.Bool(reg);

            return retorno;
        }
    }
}
