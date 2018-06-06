using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.WebAPI.API.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> All { get; }
        TEntity Find(int key);
        void Incluir(params TEntity[] obj);
        void Alterar(params TEntity[] obj);
        void Excluir(params TEntity[] obj);
    }
}
