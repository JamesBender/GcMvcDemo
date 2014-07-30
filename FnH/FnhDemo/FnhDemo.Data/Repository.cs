﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FnhDemo.Data.Entities;
using NHibernate;

namespace FnhDemo.Data
{
    public class Repository<T> where T : BaseEntity
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
                                    .ExposeConfiguration(x => x.SetInterceptor(new SqlStatementInterceptor()))
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

        public void Save(T entity)
        {
            try
            {
                using (var session = _sessionFactory.OpenSession())
                {
                    if (entity.Id == 0)
                    {
                        session.Save(entity);
                    }
                    else
                    {
                        session.Merge(entity);
                    }
                    session.Flush();
                    session.Evict(entity);
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                throw;
            }
        }

        public void Delete(int id)
        {
            var dvd = GetById(id);
            using (var session = _sessionFactory.OpenSession())
            {
                session.Delete(dvd);
                session.Flush();
                session.Evict(dvd);
            }
        }
    }

    public class SqlStatementInterceptor : EmptyInterceptor
    {
        public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
        {
            Trace.WriteLine(sql.ToString());
            Debug.WriteLine(sql.ToString());
            return sql;
        }
    }
}