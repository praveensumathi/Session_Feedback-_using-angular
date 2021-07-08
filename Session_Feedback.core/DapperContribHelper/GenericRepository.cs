using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;

namespace Session_Feedback.core.ConnectionHelper
{
    public class GenericRepository<T> where T : class
    {
        private readonly IDbConnection _session;

        protected IDbConnection Session { get { return _session; } }

        public GenericRepository(IDbConnection session)
        {
            _session = session;
        }

        public IEnumerable<T> GetAll()
        {
            return _session.GetAll<T>();
        }

        public T GetById(long id)
        {
            return _session.Get<T>(id);
        }

        public long Create(T entity)
        {
            return _session.Insert<T>(entity);
        }

        public bool Update(T entity)
        {
            return _session.Update<T>(entity);
        }

        public bool Delete(T entity)
        {
            return _session.Delete<T>(entity);
        }

        public bool DeleteAll()
        {
            return _session.DeleteAll<T>();
        }
    }
}
