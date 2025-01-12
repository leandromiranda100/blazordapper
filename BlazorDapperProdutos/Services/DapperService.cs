﻿using Dapper;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;

namespace BlazorDapperProdutos.Services
{
    public class DapperService : IDapperService
    {
        private readonly IConfiguration _config;
        private readonly string _conn;

        public DapperService(IConfiguration config)
        {
            _config = config;
            _conn = _config.GetConnectionString("SQLServerConn");
        }

        public DbConnection GetConnection()
        {
            return new SqlConnection(_conn);
        }

        public int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(_conn);
            return db.Execute(sp, parms, commandType: commandType);
        }

        public T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(_conn);
            return db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
        }

        public List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(_conn);
            return db.Query<T>(sp, parms, commandType: commandType).ToList();
        }

        public T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result = default(T);
            using IDbConnection db = new SqlConnection(_conn);
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();

                    using var tran = db.BeginTransaction();
                    try
                    {
                        result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                        tran.Commit();
                    }
                    catch (Exception)
                    {
                        tran.Rollback();
                        throw;
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
            return result;
        }

        public T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result = default(T);
            using IDbConnection db = new SqlConnection(_conn);
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();

                    using var tran = db.BeginTransaction();
                    try
                    {
                        result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                        tran.Commit();
                    }
                    catch (Exception)
                    {
                        tran.Rollback();
                        throw;
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
            return result;
        }
    }
}
