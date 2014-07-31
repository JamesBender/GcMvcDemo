using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SqlDemo.Data.Entities;

namespace SqlDemo.Data.Repositories
{
    public class DVDRepository : BaseRepository
    {
        public IEnumerable<DVD> All
        {
            get
            {
                OpenSqlConnection();

                var listOfResult = new List<DVD>();

                const string query =
                    @"Select Id, RunningTime, IsSpecialEdition, Synopsis, Title, Genre, Year from DVD Order By Id";
                var command = new SqlCommand(query, SqlConnection);
                var dataReader = command.ExecuteReader();

                try
                {
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
                }
                finally
                {
                    dataReader.Close();
                }
                return listOfResult;
            }
        }

        public DVD GetById(int id)
        {
            OpenSqlConnection();
            const string query =
                    @"Select Id, RunningTime, IsSpecialEdition, Synopsis, Title, Genre, Year from DVD Where Id = @DvdId";
            var command = new SqlCommand(query, SqlConnection);
            command.Parameters.AddWithValue("@DvdId", id);
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
            dataReader.Close();
            return dvd;
        }

        public int Save(DVD dvd)
        {
            OpenSqlConnection();
            var result = dvd.Id == 0 ? InsertDvd(dvd) : UpdateDvd(dvd);
            return result;
        }

        private int UpdateDvd(DVD dvd)
        {
            const string query = "Update DVD Set RunningTime = @RunningTime, IsSpecialEdition = @IsSpecialEdition, Synopsis = @Synopsis, Title = @Title, Genre = @Genre, Year = @Year Where Id = @Id";

            var command = new SqlCommand(query, SqlConnection);
            command.Parameters.AddWithValue("@Id", dvd.Id);
            command.Parameters.AddWithValue("@RunningTime", dvd.RunningTime);
            command.Parameters.AddWithValue("@IsSpecialEdition", dvd.IsSpecialEdition);
            command.Parameters.AddWithValue("@Synopsis", dvd.Synopsis);
            command.Parameters.AddWithValue("@Title", dvd.Title);
            command.Parameters.AddWithValue("@Genre", dvd.Genre);
            command.Parameters.AddWithValue("@Year", dvd.Year);

            command.ExecuteNonQuery();

            return dvd.Id;
        }

        private int InsertDvd(DVD dvd)
        {
            const string query = "Insert Into DVD(RunningTime, IsSpecialEdition, Synopsis, Title, Genre, Year) Values(@RunningTime, @IsSpecialEdition, @Synopsis, @Title, @Genre, @Year); SELECT CAST(scope_identity() AS int)";

            var command = new SqlCommand(query, SqlConnection);
            command.Parameters.AddWithValue("@RunningTime", dvd.RunningTime);
            command.Parameters.AddWithValue("@IsSpecialEdition", dvd.IsSpecialEdition);
            command.Parameters.AddWithValue("@Synopsis", dvd.Synopsis);
            command.Parameters.AddWithValue("@Title", dvd.Title);
            command.Parameters.AddWithValue("@Genre", dvd.Genre);
            command.Parameters.AddWithValue("@Year", dvd.Year);

            return (Int32)command.ExecuteScalar();
        }

        public void Delete(int id)
        {
            OpenSqlConnection();

            const string query = "Delete From DVD Where Id = @Id";
            var command = new SqlCommand(query, SqlConnection);

            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();

        }
    }

}