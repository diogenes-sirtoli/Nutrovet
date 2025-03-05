using System;
using System.Collections.Generic;
using System.Linq;
using DCL;
using DAL;
using System.Web;
using System.IO;
using System.Collections;
using System.Drawing;
using PagarMeOld;

namespace BLL
{
	public class clConfigReceituarioBll
    {
		public bllRetorno Inserir(ConfigReceituario _config)
		{
            bllRetorno ret = _config.ValidarRegras();

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Inserir(_config);

                    return bllRetorno.GeraRetorno(true,
                        "INSERÇÃO efetuada com sucesso!!!");

                }
                catch (Exception err)
                {
                    var erro = err;
                    return bllRetorno.GeraRetorno(false,
                        "Não foi possível efetuar a INSERÇÃO!!!");
                }
            }
            else
            {
                return ret;
            }
        }

		public bllRetorno Alterar(ConfigReceituario _config)
		{
			bllRetorno ret = _config.ValidarRegras();

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
					crud.Alterar(_config);

					return bllRetorno.GeraRetorno(true,
						"ALTERAÇÃO efetuada com sucesso!!!");
				}
				catch (Exception err)
				{
					var erro = err;
					return bllRetorno.GeraRetorno(false,
						"Não foi possível efetuar a ALTERAÇÃO!!! ");
				}
			}
			else
			{
				return ret;
			}
		}

		public bllRetorno Excluir(ConfigReceituario _config)
		{
            CrudDal crud = new CrudDal();

            try
            {
                crud.Excluir(_config);

                return bllRetorno.GeraRetorno(true,
                    "EXCLUSÃO efetuada com sucesso!!!");
            }
            catch (Exception err)
            {
                var erro = err;
                return bllRetorno.GeraRetorno(false,
                    "Não foi possível efetuar a EXCLUSÃO!!!");
            }
        }

		public ConfigReceituario Carregar(int _id)
		{
			CrudDal crud = new CrudDal();
            ConfigReceituario _retorno;

			string _sql = string.Format(
                @"SELECT	IdPessoa, DtIniUso, DtIniUsoLT, NomeClinica, Slogan, 
                            fLivreCabecalho, LivreCabecalho, fLivreRodape, 
                            LivreRodape, Site, Facebook, Twitter, Instagram, Logotipo, 
                            Assinatura, CRMV, CrmvUf, Email, Telefone, Celular, 
                            Logradouro, Logr_Nr, Logr_Compl, Logr_Bairro, Logr_CEP, 
                            Logr_Cidade, Logr_UF, Logr_Pais, Ativo, IdOperador, 
                            IP, DataCadastro, Versao
					FROM	ConfigReceituario
					WHERE	(IdPessoa = {0})", _id);

            _retorno = crud.ExecutarComando<ConfigReceituario>(_sql).FirstOrDefault();

			return _retorno;
		}

        public string CarregarImgLogo(int idPessoa)
        {
            HttpServerUtility _server = HttpContext.Current.Server;
            string path = _server.MapPath("~/Perfil/Logotipos/");
            DirectoryInfo _directorio = new DirectoryInfo(path);

            string search = (from d in _directorio.GetFiles("Logotipo_" + idPessoa + ".*")
                             orderby d.LastAccessTime descending
                             select d.Name).FirstOrDefault();

            if ((search is null) || (search == ""))
            {
                search = "logo_receita.png";
            }

            return "~/Perfil/Logotipos/" + search + "?time=" + DateTime.Now.ToString();
        }

        public string CarregarImgLogoImpr(int idPessoa)
        {
            HttpServerUtility _server = HttpContext.Current.Server;
            string path = _server.MapPath("~/Perfil/Logotipos/");
            DirectoryInfo _directorio = new DirectoryInfo(path);

            string search = (from d in _directorio.GetFiles("Logotipo_" + idPessoa + ".*")
                             orderby d.LastAccessTime descending
                             select d.Name).FirstOrDefault();

            if ((search is null) || (search == ""))
            {
                search = "logo_receita.png";
            }

            string _retorno = path + search;

            return _retorno;
        }

        public string CarregarImgAssinatura(int idPessoa)
        {
            HttpServerUtility _server = HttpContext.Current.Server;
            string path = _server.MapPath("~/Perfil/Assinaturas/");
            DirectoryInfo _directorio = new DirectoryInfo(path);

            string search = (from d in _directorio.GetFiles("Assinatura_" + idPessoa + ".*")
                             orderby d.LastAccessTime descending
                             select d.Name).FirstOrDefault();

            if ((search is null) || (search == ""))
            {
                search = "assinatura_receita.png";
            }

            return "~/Perfil/Assinaturas/" + search + "?time=" + DateTime.Now.ToString();
        }

        public string CarregarImgAssinaturaImpr(int idPessoa)
        {
            HttpServerUtility _server = HttpContext.Current.Server;
            string path = _server.MapPath("~/Perfil/Assinaturas/");
            DirectoryInfo _directorio = new DirectoryInfo(path);

            string search = (from d in _directorio.GetFiles("Assinatura_" + idPessoa + ".*")
                             orderby d.LastAccessTime descending
                             select d.Name).FirstOrDefault();

            if ((search is null) || (search == ""))
            {
                search = "assinatura_receita.png";
            }

            string _retorno = path + search;

            return _retorno;
        }

        public List<ConfigReceituario> Listar(bool? _ativo)
		{
			CrudDal crud = new CrudDal();

			string _sql = @"
				SELECT  IdPessoa, DtIniUso, DtIniUsoLT, NomeClinica, Slogan, 
                        fLivreCabecalho, LivreCabecalho, fLivreRodape, 
                        LivreRodape, Site, Facebook, Twitter, Instagram, Logotipo, 
                        Assinatura, CRMV, CrmvUf, Email, Telefone, Celular, 
                        Logradouro, Logr_Nr, Logr_Compl, Logr_Bairro, Logr_CEP, 
                        Logr_Cidade, Logr_UF, Logr_Pais, Ativo, IdOperador, 
                        IP, DataCadastro
				FROM	ConfigReceituario ";

			if (_ativo != null)
			{
				_sql += string.Format(@"
				WHERE	(Ativo = {0})", (_ativo.Value ? 1 : 0));
			}

			_sql += @"
				ORDER BY NomeClinica";

			var lista = crud.Listar<ConfigReceituario>(_sql);

			return lista.ToList();
		}

        public string MontaCamposEndereco(ConfigReceituario _campos)
        {
            ArrayList arrayEnder = new ArrayList
            {
                _campos.Logradouro,
                _campos.Logr_Nr,
                _campos.Logr_Compl,
                _campos.Logr_Bairro,
                _campos.Logr_Cidade,
                _campos.Logr_UF
            };

            string _retorno = Funcoes.Funcoes.MontaStringDeParametros(arrayEnder);

            return _retorno;
        }

        public Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }

        public bllRetorno PermissaoAcessoReceituario(int _idPessoa)
        {
            bllRetorno _retorno = new bllRetorno();
            clConfigReceituarioBll configReceitBll = new clConfigReceituarioBll();
            ConfigReceituario configReceitDcl;
            clAcessosBll acessosBll = new clAcessosBll();
            Acesso acessosDcl;
            clPessoasBll pessoaBll = new clPessoasBll();
            Pessoa pessoaDcl;
            clAcessosVigenciaPlanosBll planosBll = new clAcessosVigenciaPlanosBll();
            TOAcessosVigenciaPlanosBll planoTO;

            pessoaDcl = pessoaBll.Carregar(_idPessoa);
            acessosDcl = acessosBll.Carregar(pessoaDcl.IdPessoa);
            configReceitDcl = configReceitBll.Carregar(pessoaDcl.IdPessoa);
            planoTO = planosBll.CarregarPlano(pessoaDcl.IdPessoa);

            if ((configReceitDcl != null) && (configReceitDcl.IdPessoa > 0))
            {
                if ((Funcoes.Funcoes.ConvertePara.Bool(acessosDcl.SuperUser)) ||
                    (planoTO.dNomePlano == (int)DominiosBll.PlanosAuxNomes.Completo))
                {
                    _retorno.retorno = true;
                    _retorno.mensagem = "";
                }
                else
                {
                    if ((DateTime.Today >= configReceitDcl.DtIniUso) &&
                        ((DateTime.Today <= configReceitDcl.DtIniUso.Value.AddDays(30))))
                    {
                        _retorno.retorno = true;
                        _retorno.mensagem = "";
                    }
                    else
                    {
                        _retorno.retorno = false;
                        _retorno.mensagem = "</br>Somente Plano COMPLETO Possuí Acesso a " +
                            "esta Funcionalidade!</br>Faça um Upgrade de Planos na Tela " +
                            "do seu Perfil!";
                    }
                }
            }
            else
            {
                _retorno.retorno = false;
                _retorno.mensagem = "É Necessário Configurar os Dados Básicos da Receita no " +
                    "MENU Minha Conta > Perfil > Aba Receituário!!!</br>Clique no link abaix" +
                    "o para abir a Tela de Perfil!</br></br>";
            }

            return _retorno;
        }

        public bllRetorno PermissaoAcessoLinhaTempo(int _idPessoa)
        {
            bllRetorno _retorno = new bllRetorno();
            clConfigReceituarioBll configReceitBll = new clConfigReceituarioBll();
            ConfigReceituario configReceitDcl;
            clAcessosBll acessosBll = new clAcessosBll();
            Acesso acessosDcl;
            clPessoasBll pessoaBll = new clPessoasBll();
            Pessoa pessoaDcl;
            clAcessosVigenciaPlanosBll planosBll = new clAcessosVigenciaPlanosBll();
            TOAcessosVigenciaPlanosBll planoTO;

            pessoaDcl = pessoaBll.Carregar(_idPessoa);
            acessosDcl = acessosBll.Carregar(pessoaDcl.IdPessoa);
            configReceitDcl = configReceitBll.Carregar(pessoaDcl.IdPessoa);
            planoTO = planosBll.CarregarPlano(pessoaDcl.IdPessoa);

            List<object> objetoConfigReceitDcl = new List<object>();

            if ((configReceitDcl != null) && (configReceitDcl.IdPessoa > 0) && 
                (configReceitDcl.DtIniUsoLT != null))
            {
                if ((Funcoes.Funcoes.ConvertePara.Bool(acessosDcl.SuperUser)) ||
                    (planoTO.dNomePlano == (int)DominiosBll.PlanosAuxNomes.Intermediário) ||
                    (planoTO.dNomePlano == (int)DominiosBll.PlanosAuxNomes.Completo))
                {
                    _retorno.retorno = true;
                    _retorno.mensagem = "";
                }
                else
                {
                    if ((DateTime.Today >= configReceitDcl.DtIniUsoLT) &&
                        ((DateTime.Today <= configReceitDcl.DtIniUsoLT.Value.AddDays(30))))
                    {
                        _retorno.retorno = true;
                        _retorno.mensagem = "";
                    }
                    else
                    {
                        _retorno.retorno = false;
                        _retorno.mensagem = "</br>Somente Planos COMPLETO e INTERMEDIÁRIO" +
                            " Possuí Acesso a esta Funcionalidade!</br>Faça um Upgrade de" +
                            "Planos na Tela do seu Perfil!";
                    }
                }
            }
            else
            {
                _retorno.retorno = false;
                _retorno.mensagem = "Para usar a nova Funcionalidade de LINHA DO TEMPO é " +
                    "Necessário Configurar os Dados Básicos da Receita no MENU Minha Cont" +
                    "a > Perfil > Aba Receituário!!!</br>Clique no link abaixo para abir " +
                    "a Tela de Perfil!</br></br>";
            }
            objetoConfigReceitDcl.Add(configReceitDcl);
            _retorno.objeto = objetoConfigReceitDcl;

            return _retorno;
        }
    }
}
