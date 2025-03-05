using DAL;
using DCL;

namespace BLL
{
    public static class clAtendimentoExt
    {
        public static bllRetorno ValidarRegras(this Atendimento _atend,
            bool _insersao)
        {
            CrudDal crud = new CrudDal();
            
            if (_atend.IdPessoa <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, 
                        "Campo CLIENTE deve ser selecionado!");
            }
            else if (_atend.IdAnimal <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, 
                        "Campo PACIENTE deve ser selecionado!");
            }
            else if (_atend.IdTpAtend <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo TIPO DE ATENDIMENTO deve ser selecionado!");
            }
            else if (_atend.Descricao == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo DESCRIÇÃO deve ser preenchido!");
            }
            else if (_atend.Atendimento1 == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo ATENDIMENTO deve ser preenchido!");
            }
            else if ((_insersao) && (RegistroDuplicado(_atend)))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }

        private static bool RegistroDuplicado(Atendimento atend)
        {
            CrudDal crud = new CrudDal();
            bool retorno = false;
            string _sql = string.Format(@"
                SET DATEFORMAT dmy

                SELECT	COUNT (a.IdAtend) Total
                FROM	Atendimento a 
                WHERE	(a.IdAnimal = {0}) AND 
		                (a.IdTpAtend = {1}) AND
		                (FORMAT(CAST(a.DtHrAtend AS datetime), 
                            N'dd/MM/yyyy HH:mm') = '{2}')",
                atend.IdAnimal, atend.IdTpAtend, 
                atend.DtHrAtend.Value.ToString("dd/MM/yyyy HH:mm"));

            int reg = crud.ExecutarComandoTipoInteiro(_sql);

            retorno = Funcoes.Funcoes.ConvertePara.Bool(reg);

            return retorno;
        }
    }
}
