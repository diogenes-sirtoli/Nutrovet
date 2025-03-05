using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using System.Data.Linq.Mapping;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DAL
{
    public class CrudDal
    {
        protected NutrovetDataContext dc;

        public CrudDal()
        {
            dc = new NutrovetDataContext(Conexao.StrCon());
        }

        public T Carregar<T>(string _id) where T : class
        {
            try
            {
                var table = dc.GetTable<T>();

                MetaModel modelMap = table.Context.Mapping;

                ReadOnlyCollection<MetaDataMember> dataMembers =
                    modelMap.GetMetaType(typeof(T)).DataMembers;

                string pk = (dataMembers.Single<MetaDataMember>(
                    m => m.IsPrimaryKey)).Name;

                return table.SingleOrDefault<T>(delegate (T t)
                {
                    string memberId = t.GetType().GetProperty(pk)
                        .GetValue(t, null).ToString();
                    return memberId.ToString() == _id.ToString();
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T Carregar<T>(string _valor, string _campo) where T : class
        {
            try
            {
                var table = dc.GetTable<T>();

                return table.SingleOrDefault<T>(delegate (T t)
                {
                    string memberId = t.GetType().GetProperty(_campo)
                        .GetValue(t, null).ToString();

                    return memberId.ToString() == _valor.ToString();
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T Carregar<T>(string _valor, string _campo, bool _ativo) where T : class
        {
            try
            {
                var table = dc.GetTable<T>();

                return table.SingleOrDefault<T>(delegate (T t)
                {
                    string memberId = t.GetType().GetProperty(_campo)
                        .GetValue(t, null).ToString();
                    string ativo = t.GetType().GetProperty("Ativo")
                        .GetValue(t, null).ToString();
                    return ((memberId.ToString() == _valor.ToString()) &&
                        (ativo == _ativo.ToString()));
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T Carregar<T>(string _valorRef, string _campoRef, string _valor,
            string _campo, bool _ativo) where T : class
        {
            try
            {
                var table = dc.GetTable<T>();

                return table.SingleOrDefault<T>(delegate (T t)
                {
                    string campoRef = t.GetType().GetProperty(_campoRef)
                        .GetValue(t, null).ToString();
                    string campo = t.GetType().GetProperty(_campo)
                        .GetValue(t, null).ToString();
                    string ativo = t.GetType().GetProperty("Ativo")
                        .GetValue(t, null).ToString();
                    return ((campo.ToString() == _valor.ToString()) &&
                        (campoRef.ToString() == _valorRef.ToString()) &&
                        (ativo == _ativo.ToString()));
                });
            }
            catch
            {
                throw;
            }
        }

        public T Carregar<T>(string _valorRef, string _campoRef, string _valorRef2,
            string _campoRef2, string _valor, string _campo,
            bool _ativo) where T : class
        {
            try
            {
                var table = dc.GetTable<T>();

                return table.SingleOrDefault<T>(delegate (T t)
                {
                    string campo = t.GetType().GetProperty(_campo)
                        .GetValue(t, null).ToString();
                    string campoRef = t.GetType().GetProperty(_campoRef)
                        .GetValue(t, null).ToString();
                    string campoRef2 = t.GetType().GetProperty(_campoRef2)
                        .GetValue(t, null).ToString();
                    string ativo = t.GetType().GetProperty("Ativo")
                        .GetValue(t, null).ToString();
                    return ((campo.ToString() == _valor.ToString()) &&
                        (campoRef.ToString() == _valorRef.ToString()) &&
                        (campoRef2.ToString() == _valorRef2.ToString()) &&
                        (ativo == _ativo.ToString()));
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<T> Listar<T>() where T : class
        {
            dc.CommandTimeout = 0;
            List<T> table;

            try
            {
                table = dc.GetTable<T>().ToList<T>();

            }
            catch (Exception err)
            {
                table = null;

                //Funcoes.Funcoes.Erros.GravarLogErros("Não foi possível executar o COMANDO LISTAR!!!", err.Message,
                //    err.InnerException, clConexaoDcl.StrCon());
            }

            return table;
        }

        public IList<T> Listar<T>(string Sql) where T : class
        {
            dc.CommandTimeout = 0;
            IEnumerable<T> lista;

            try
            {
                lista = from l in dc.ExecuteQuery<T>(Sql)
                        select l;

            }
            catch (Exception err)
            {
                lista = null;

                //Funcoes.Funcoes.Erros.GravarLogErros("Não foi possível executar o COMANDO LISTAR!!!", err.Message,
                //    err.InnerException, Sql);
            }

            return lista.ToList<T>();
        }

        public void Inserir<T>(T item) where T : class
        {
            try
            {
                var table = dc.GetTable<T>();

                table.InsertOnSubmit(item);
                dc.SubmitChanges();
            }
            catch (Exception err)
            {
                throw;
            }
        }

        public void Alterar<T>(T item) where T : class
        {
            try
            {
                Object newObj = Activator.CreateInstance(typeof(T), new object[0]);
                PropertyDescriptorCollection originalProps =
                    TypeDescriptor.GetProperties(item);
                dc.CommandTimeout = 0;

                foreach (PropertyDescriptor currentProp in originalProps)
                {
                    if (currentProp.Attributes[typeof(
                        System.Data.Linq.Mapping.ColumnAttribute)] != null)
                    {
                        object val = currentProp.GetValue(item);
                        currentProp.SetValue(newObj, val);
                    }
                }

                var table = dc.GetTable<T>();
                table.Attach((T)newObj, true);
                dc.SubmitChanges();
            }
            catch (Exception err)
            {
                throw;
            }
        }

        public void Excluir<T>(T item) where T : class
        {
            try
            {
                Type tType = item.GetType();
                Object newObj = Activator.CreateInstance(tType, new object[0]);

                PropertyDescriptorCollection originalProps =
                    TypeDescriptor.GetProperties(item);

                foreach (PropertyDescriptor currentProp in originalProps)
                {
                    if (currentProp.Attributes[typeof(
                        System.Data.Linq.Mapping.ColumnAttribute)] != null)
                    {
                        object val = currentProp.GetValue(item);
                        currentProp.SetValue(newObj, val);
                    }
                }

                var table = dc.GetTable<T>();
                table.Attach((T)newObj, true);
                table.DeleteOnSubmit((T)newObj);
                dc.SubmitChanges();
            }
            catch (Exception err)
            {
                throw;
            }
        }

        public int ExecutarComando(string Sql)
        {
            dc.CommandTimeout = 0;
            int _exec;

            try
            {
                _exec = dc.ExecuteCommand(Sql);
            }
            catch (Exception err)
            {
                _exec = 0;
                //Funcoes.Funcoes.Erros.GravarLogErros("Não foi possível executar o COMANDO!!!", err.Message,
                //    err.InnerException, Sql);
            }

            return _exec;
        }

        public IEnumerable<T> ExecutarComando<T>(string Sql) where T : class
        {
            dc.CommandTimeout = 0;
            IEnumerable<T> query;

            try
            {
                query = dc.ExecuteQuery<T>(Sql);
            }
            catch (Exception err)
            {
                query = null;
                //Funcoes.Funcoes.Erros.GravarLogErros("Não foi possível executar o COMANDO!!!", err.Message,
                //    err.InnerException, Sql);
            }

            return query;
        }

        public IEnumerable<T> ExecutarComandoTipoBasico<T>(string Sql)
        {
            dc.CommandTimeout = 0;
            IEnumerable<T> query;

            try
            {
                query = dc.ExecuteQuery<T>(Sql);
            }
            catch (Exception err)
            {
                query = null;
                //Funcoes.Funcoes.Erros.GravarLogErros("Não foi possível executar o COMANDO!!!", err.Message,
                //    err.InnerException, Sql);
            }

            return query;
        }

        public int ExecutarComandoTipoInteiro(string Sql)
        {
            int retorno = 0;
            dc.CommandTimeout = 0;

            try
            {
                var reg = dc.ExecuteQuery<int>(Sql);

                foreach (var item in reg)
                {
                    retorno = Funcoes.Funcoes.ConvertePara.Int(item);
                }
            }
            catch (Exception err)
            {
                retorno = 0;
                //Funcoes.Funcoes.Erros.GravarLogErros("Não foi possível executar o COMANDO!!!", err.Message,
                //    err.InnerException, Sql);
            }

            return retorno;
        }

        public bool RegistroDuplicado<T>(string _valor, string _campo,
            bool _ativo) where T : class
        {
            var retorno = Carregar<T>(_valor, _campo, _ativo);

            if (retorno != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RegistroDuplicado<T>(string _valorRef, string _campoRef,
            string _valor, string _campo, bool _ativo) where T : class
        {
            var retorno = Carregar<T>(_valorRef, _campoRef, _valor, _campo, _ativo);

            if (retorno != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RegistroDuplicado<T>(string _valorRef, string _campoRef,
            string _valorRef2, string _campoRef2, string _valor, string _campo,
            bool _ativo) where T : class
        {
            var retorno = Carregar<T>(_valorRef, _campoRef, _valorRef2, _campoRef2,
                _valor, _campo, _ativo);

            if (retorno != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ReadOnlyCollection<MetaDataMember> Colunas<T>() where T : class
        {
            var columnNames = dc.Mapping.MappingSource.GetModel(
                typeof(NutrovetDataContext)).GetMetaType(typeof(T)).DataMembers;

            return columnNames;
        }

        public string VerificaConexao()
        {
            string _ret = "";

            try
            {
                if (dc.DatabaseExists())
                {
                    _ret = "sim";
                }
                else
                {
                    _ret = "Não Foi Possível Conectar ao Banco de Dados!";
                }
            }
            catch (Exception msg)
            {
                _ret = msg.Message;

                throw;
            }

            return _ret;
        }
    }
}
