using Microsoft.EntityFrameworkCore;
using rs_backends_api_2019.DAL.Enums;
using rs_backends_api_2019.DAL.Models.Defaults.RepositoryModel;
using rs_backends_api_2019.DAL.Models.Defaults.TransactionModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> getRepository<TEntity>() where TEntity : class;

        ContextFactory ContxFactory { get; set; }

        int saveChanges();

        ResponseModel saveChangesWithResponse();

        TransactionResult createTransaction(Action action);
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork
     where TContext : DbContext
    {
        TContext Context { get; }
    }
}
