using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FnhDemo.Data.Entities;
using NHibernate;

namespace FnhDemo.Data
{
    public class Repository<T> where T: BaseEntity
    {
        private ISessionFactory _sessionFactory;

        public Repository()
        {
            _sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                    .ConnectionString(
                        c =>
                            c.Is(
                                ConfigurationManager.ConnectionStrings["FnhDemo"]
                                    .ToString())))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<BaseEntity>())
                .BuildSessionFactory();
        }

        public IList<T> All
        {
            get
            {
                using (var session = _sessionFactory.OpenSession())
                {
                    return session.CreateCriteria<T>().List<T>();
                }
            }
        }

        public T GetById(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Get<T>(id);
            }
        }
    }
}