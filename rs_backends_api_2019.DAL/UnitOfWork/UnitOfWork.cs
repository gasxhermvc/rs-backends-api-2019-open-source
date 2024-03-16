using Microsoft.EntityFrameworkCore;
using rs_backends_api_2019.DAL.Enums;
using rs_backends_api_2019.DAL.Interfaces;
using rs_backends_api_2019.DAL.Models.Defaults.RepositoryModel;
using rs_backends_api_2019.DAL.Models.Defaults.TransactionModel;
using rs_backends_api_2019.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.DAL.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>
        where TContext : DbContext
    {
        //private Dictionary<Type, object> _repositories;
        public ContextFactory ContxFactory { get; set; } = ContextFactory.ActiveBeachContext;

        public TContext Context { get; }

        public UnitOfWork(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public int saveChanges()
        {
            return Context.SaveChanges();
        }

        public ResponseModel saveChangesWithResponse()
        {

            ResponseModel response = new ResponseModel()
            {
                isSuccess = false,
                message = ""
            };

            try
            {
                int rowEfect = Context.SaveChanges();

                if (rowEfect >= 1)
                {
                    response.isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message.ToString();
            }

            return response;
        }

        public TransactionResult createTransaction(Action action)
        {
            bool success = false;
            TransactionResult result = new TransactionResult()
            {
                success = success,
                exception = null
            };

            try
            {
                this.Context.Database.BeginTransaction();
                action();
                this.Context.Database.CommitTransaction();
                success = true;
                result.success = true;
            }
            catch (Exception e)
            {
                this.Context.Database.RollbackTransaction();
                result.success = false;
                result.exception = e;
            }

            return result;
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        //Function Imprement Repository by Generic Repository.
        public IRepository<TEntity> getRepository<TEntity>() where TEntity : class
        {
            return (IRepository<TEntity>)new Repository<TEntity>(Context);
        }
    }
}
