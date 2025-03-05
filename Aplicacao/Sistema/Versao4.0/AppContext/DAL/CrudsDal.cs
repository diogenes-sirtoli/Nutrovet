using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using DCL;

namespace DAL
{
    public class CrudsDal<TEntity> : IDisposable, ICrud<TEntity> where TEntity : class
    {
        protected NutrovetEntities dc;

        public CrudsDal()
        {
            dc = new NutrovetEntities();
        }

        public void Alterar(TEntity obj)
        {
            try
            {
                dc.Entry(obj).State = EntityState.Modified;
                dc.SaveChanges();
            }
            catch (Exception msg)
            {
                throw msg;
            }
        }

        public TEntity Carregar(params object[] key)
        {
            return dc.Set<TEntity>().Find(key);
        }

        public void Dispose()
        {
            dc.Dispose();
        }

        public void Excluir(Func<TEntity, bool> predicate)
        {
            try
            {
                dc.Set<TEntity>().
                Where(predicate).ToList().
                ForEach(del => dc.Set<TEntity>().Remove(del));

                dc.SaveChanges();
            }
            catch (Exception msg)
            {
                throw msg;
            }
        }

        public void Inserir(TEntity obj)
        {
            try
            {
                dc.Set<TEntity>().Add(obj);
                dc.SaveChanges();
            }
            catch (Exception msg)
            {
                throw msg;
            }
        }

        public List<TEntity> Listar()
        {
            try
            {
                dc.Database.CommandTimeout = 0;
                return dc.Set<TEntity>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TEntity> Listar(string _sql)
        {
            try
            {
                dc.Database.CommandTimeout = 0;
                return dc.Database.SqlQuery<TEntity>(_sql).ToList();
            }
            catch (Exception msg)
            {
                throw msg;
            }
        }

        public List<TEntity> Listar(Func<TEntity, bool> predicate)
        {
            dc.Database.CommandTimeout = 0;

            return Listar().Where(predicate).AsQueryable().ToList();
        }

        public void SalvarTodos()
        {
            dc.SaveChanges();
        }

        //Serve para executar comandos crud direto (Insert, Update, Delete)
        public int ExecutarComando(string _sql)
        {
            dc.Database.CommandTimeout = 0;
            int _exec;

            try
            {
                _exec = dc.Database.ExecuteSqlCommand(_sql);
            }
            catch (Exception)
            {
                _exec = 0;
            }

            return _exec;
        }

        public int ExecutarComandoTipoInteiro(string _sql)
        {
            int retorno = 0;
            dc.Database.CommandTimeout = 0;

            try
            {
                var reg = dc.Database.SqlQuery<int>(_sql);

                foreach (var item in reg)
                {
                    retorno = Funcoes.Funcoes.ConvertePara.Int(item);
                }
            }
            catch (Exception)
            {
                retorno = 0;
            }

            return retorno;
        }

        public IEnumerable<T> ExecutarComandoTipoBasico<T>(string _sql)
        {
            dc.Database.CommandTimeout = 0;
            var query = dc.Database.SqlQuery<T>(_sql);

            return query;
        }

        public IEnumerable<TEntity> ExecutarComandoTO(string _sql)
        {
            dc.Database.CommandTimeout = 0;

            try
            {
                var query = dc.Database.SqlQuery<TEntity>(_sql);

                return query;
            }
            catch (Exception msg)
            {
                throw msg;
            }
        }

        public bool VerificaConexao()
        {
            bool _ret;

            try
            {
                if (dc.Database.Exists())
                {
                    _ret = true;
                }
                else
                {
                    _ret = false;

                }
            }
            catch (Exception msg)
            {
                _ret = false;

                throw;
            }

            return _ret;
        }
    }
}
