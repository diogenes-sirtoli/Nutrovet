using System;
using System.Collections.Generic;

namespace DAL
{
    interface ICrud<TEntity> where TEntity : class
    {
        void Inserir(TEntity obj);

        void Alterar(TEntity obj);

        void Excluir(Func<TEntity, bool> predicate);

        TEntity Carregar(params object[] key);

        List<TEntity> Listar(Func<TEntity, bool> predicate);

        List<TEntity> Listar(string _sql);

        List<TEntity> Listar();

        int ExecutarComando(string _sql);

        int ExecutarComandoTipoInteiro(string _sql);

        IEnumerable<TEntity> ExecutarComandoTO(string _sql);

        IEnumerable<T> ExecutarComandoTipoBasico<T>(string _sql);

        void SalvarTodos();
    }
}
