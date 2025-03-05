using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
	public static class clAnimaisPesoHistoricoExt
	{
		public static bllRetorno ValidarRegras(this AnimaisPesoHistorico _peso,
			bool _insersao)
		{
			CrudDal crud = new CrudDal();

			if (_peso.IdAnimal <= 0)
			{
				return
					bllRetorno.GeraRetorno(false,
						"Campo PACIENTE deve ser selecionado!");
			}
			else if (_peso.Peso <= 0)
			{
				return
					bllRetorno.GeraRetorno(false,
						"Campo PESO deve ser preenchido!");
			}
			else if ((_insersao) && (RegistroDuplicado(
				_peso.IdAnimal, _peso.Peso, _peso.DataHistorico, true)))
			{
				return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
			}

			if (_peso.DataHistorico == DateTime.Parse("01/01/1910"))
			{
				_peso.DataHistorico = null;
			}

			return bllRetorno.GeraRetorno(true, "Dados Válidos!");
		}

		private static bool RegistroDuplicado(int _idAnimal, decimal _peso, 
			DateTime? _dtHist, bool _ativo)
		{
			CrudDal crud = new CrudDal();
			bool retorno = false;
            string _pesoAtual = Funcoes.Funcoes.ConvertePara.String(_peso);
            string _pesoNew = _pesoAtual.Replace(",", ".");

            string _sql = string.Format(@"
				SET DATEFORMAT dmy 

				SELECT	COUNT (h.IdHistorico) Total
				FROM	Animaispesohistorico h
				WHERE	(h.IdAnimal = {0}) And (h.Peso = {1}) And 
						(h.DataHistorico = '{2}') AND 
						(Ativo = {3})", _idAnimal, _pesoNew, _dtHist,
				(_ativo ? 1 : 0));

			int reg = crud.ExecutarComandoTipoInteiro(_sql);

			retorno = Funcoes.Funcoes.ConvertePara.Bool(reg);

			return retorno;
		}
	}
}
