using DAL.Auth;
using DAL.ModelRepositories;
using Microsoft.Extensions.Configuration;
using Session_Feedback.core.ModelRepositories;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Session_Feedback.core.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IJwtAuthManager _jwtAuth;

        private SessionRepository _sessionRepository;
        private QuestionRepository _questionRepository;
        private AnswerRepository _answerRepository;
        private ApplicationUserRepository _applicationUserRepository;
        private QuestionTemplateRepository _questionTemplateRepository;

        private bool _disposed;

        public UnitOfWork(IConfiguration configuration,IJwtAuthManager jwtAuthManager)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("connection_string"));
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            _jwtAuth = jwtAuthManager;
        }

        public SessionRepository Sessions 
        { 
            get 
            { 
                return _sessionRepository ??= new SessionRepository(_transaction); 
            } 
        }

        public QuestionRepository Questions
        {
            get
            {
                return _questionRepository ??= new QuestionRepository(_transaction);
            }
        }

        public AnswerRepository Answers
        {
            get
            {
                return _answerRepository ??= new AnswerRepository(_transaction);
            }
        }

        public ApplicationUserRepository Users 
        {
            get
            {
                return _applicationUserRepository ??= new ApplicationUserRepository(_transaction, _jwtAuth);
            }
        }

        public QuestionTemplateRepository Templates
        {
            get
            {
                return _questionTemplateRepository ??= new QuestionTemplateRepository(_transaction);
            }
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        private void ResetRepositories()
        {
            _sessionRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

    }
}
