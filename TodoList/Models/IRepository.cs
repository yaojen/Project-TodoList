using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Models
{
    public interface IRepository<T>
    {
        
        void Create(T entity);

        //取得第一筆符合的資料就回傳
        T Read(Expression<Func<T, bool>> predicate);

        //讀全部
        IQueryable<T> Reads();

        void Update(T entity);

        void Delete(T entity);

        void SaveChanges();

    }
}
