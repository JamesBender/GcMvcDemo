using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SqlDemo.Data.Entities;

namespace SqlDemo.Data.Repositories
{
    public class DVDRepository
    {
        private readonly SqlConnection _sqlConnection;

        public DVDRepository()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            _sqlConnection = new SqlConnection(connectionString);
        }

        public IEnumerable<DVD> All
        {
            get
            {
                OpenSqlConnection();

                var listOfResult = new List<DVD>();

                const string query =
                    @"Select Id, RunningTime, IsSpecialEdition, Synopsis, Title, Genre, Year from DVD Order By Id";
                var command = new SqlCommand(query, _sqlConnection);
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    var dvd = new DVD
                    {
                        Id = (int)dataReader["Id"],
                        RunningTime = (int)dataReader["RunningTime"],
                        IsSpecialEdition = (bool)dataReader["IsSpecialEdition"],
                        Synopsis = dataReader["Synopsis"].ToString(),
                        Title = dataReader["Title"].ToString(),
                        Genre = dataReader["Genre"].ToString(),
                        Year = (int)dataReader["Year"]
                    };

                    listOfResult.Add(dvd);
                }

                return listOfResult;
            }
        }

        private void OpenSqlConnection()
        {
            if (_sqlConnection.State == ConnectionState.Closed)
            {
                _sqlConnection.Open();
            }
        }

        public DVD GetById(int Id)
        {
            OpenSqlConnection();
            const string query =
                    @"Select Id, RunningTime, IsSpecialEdition, Synopsis, Title, Genre, Year from DVD Where Id = @DvdId";
            var command = new SqlCommand(query, _sqlConnection);
            command.Parameters.AddWithValue("@DvdId", Id);
            var dataReader = command.ExecuteReader();

            dataReader.Read();
            var dvd = new DVD
            {
                Id = (int)dataReader["Id"],
                RunningTime = (int)dataReader["RunningTime"],
                IsSpecialEdition = (bool)dataReader["IsSpecialEdition"],
                Synopsis = dataReader["Synopsis"].ToString(),
                Title = dataReader["Title"].ToString(),
                Genre = dataReader["Genre"].ToString(),
                Year = (int)dataReader["Year"]
            };
            return dvd;
        }

        public int Save(DVD dvd)
        {
            OpenSqlConnection();
            int result;
            if (dvd.Id == 0)
            {
                result = InsertDvd(dvd);
            }
            else
            {
                result = UpdateDvd(dvd);
            }
            return result;
        }

        private int UpdateDvd(DVD dvd)
        {
            const string query = "Update DVD Set RunningTime = @RunningTime, IsSpecialEdition = @IsSpecialEdition, Synopsis = @Synopsis, Title = @Title, Genre = @Genre, Year = @Year Where Id = @Id";

            var command = new SqlCommand(query, _sqlConnection);
            command.Parameters.AddWithValue("@Id", dvd.Id);
            command.Parameters.AddWithValue("@RunningTime", dvd.RunningTime);
            command.Parameters.AddWithValue("@IsSpecialEdition", dvd.IsSpecialEdition);
            command.Parameters.AddWithValue("@Synopsis", dvd.Synopsis);
            command.Parameters.AddWithValue("@Title", dvd.Title);
            command.Parameters.AddWithValue("@Genre", dvd.Genre);
            command.Parameters.AddWithValue("@Year", dvd.Year);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                throw;
            }

            return dvd.Id;
        }

        private int InsertDvd(DVD dvd)
        {
            const string query = "Insert Into DVD(RunningTime, IsSpecialEdition, Synopsis, Title, Genre, Year) Values(@RunningTime, @IsSpecialEdition, @Synopsis, @Title, @Genre, @Year); SELECT CAST(scope_identity() AS int)";

            var command = new SqlCommand(query, _sqlConnection);
            command.Parameters.AddWithValue("@RunningTime", dvd.RunningTime);
            command.Parameters.AddWithValue("@IsSpecialEdition", dvd.IsSpecialEdition);
            command.Parameters.AddWithValue("@Synopsis", dvd.Synopsis);
            command.Parameters.AddWithValue("@Title", dvd.Title);
            command.Parameters.AddWithValue("@Genre", dvd.Genre);
            command.Parameters.AddWithValue("@Year", dvd.Year);

            return (Int32) command.ExecuteScalar();
        }

        public void Delete(int id)
        {
            OpenSqlConnection();

            const string query = "Delete From DVD Where Id = @Id";
            var command = new SqlCommand(query, _sqlConnection);

            command.Parameters.AddWithValue("@Id", id);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                var message = exception.Message;
                throw;
            }
        }        
    }
    
}