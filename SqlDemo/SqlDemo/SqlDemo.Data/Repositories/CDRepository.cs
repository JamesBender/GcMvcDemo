using System.Collections.Generic;
using System.Data.SqlClient;
using SqlDemo.Data.Entities;

namespace SqlDemo.Data.Repositories
{
    public class CDRepository : BaseRepository
    {
        private readonly TrackRepository _trackRepository;

        public CDRepository()
        {
            _trackRepository = new TrackRepository(this);
        }

        public IEnumerable<CD> All
        {
            get
            {
                OpenSqlConnection();

                var listOfResult = new List<CD>();

                const string query =
                    @"Select Id, Artist, Title, Genre, Year from CD Order By Id";
                var command = new SqlCommand(query, SqlConnection);
                var dataReader = command.ExecuteReader();

                try
                {
                    while (dataReader.Read())
                    {
                        var cdId = (int)dataReader["Id"];

                        var cd = new CD
                        {
                            Id = cdId,
                            Artist = dataReader["Artist"].ToString(),
                            Title = dataReader["Title"].ToString(),
                            Genre = dataReader["Genre"].ToString(),
                            Year = (int)dataReader["Year"],
                            Tracks = _trackRepository.GetListOfTracksByCd(cdId)
                        };
                        listOfResult.Add(cd);
                    }
                }
                finally
                {
                    dataReader.Close();
                }

                return listOfResult;
            }
        }

        public CD GetById(int id)
        {
            OpenSqlConnection();
            var cd = new CD();
            const string query = @"Select Id, Artist, Title, Genre, Year from CD Where Id = @Id";
            var command = new SqlCommand(query, SqlConnection);

            command.Parameters.AddWithValue("@Id", id);
            var dataReader = command.ExecuteReader();

            try
            {
                dataReader.Read();

                cd.Artist = dataReader["Artist"].ToString();
                cd.Id = (int)dataReader["Id"];
                cd.Genre = dataReader["Genre"].ToString();
                cd.Title = dataReader["Title"].ToString();
                cd.Year = (int)dataReader["Year"];

                cd.Tracks = cd.Tracks = _trackRepository.GetListOfTracksByCd(id);
            }
            finally
            {
                dataReader.Close();
            }

            return cd;
        }

        public int Save(CD cd)
        {
            OpenSqlConnection();
            int id;
            if (cd.Id == 0)
            {
                id = InsertCD(cd);

                if (cd.Tracks != null)
                {
                    foreach (var track in cd.Tracks)
                    {
                        _trackRepository.Save(track);
                    }
                }
            }
            else
            {
                UpdateCD(cd);

                if (cd.Tracks != null)
                {
                    foreach (var track in cd.Tracks)
                    {
                        _trackRepository.Save(track);
                    }
                }
                id = cd.Id;
            }

            return id;
        }

        private void UpdateCD(CD cd)
        {
            const string query = "Update CD Set Artist = @Artist, Title = @Title, Genre = @Genre, Year = @Year Where Id = @Id";
            var command = new SqlCommand(query, SqlConnection);

            command.Parameters.AddWithValue("@Id", cd.Id);
            command.Parameters.AddWithValue("@Artist", cd.Artist);
            command.Parameters.AddWithValue("@Title", cd.Title);
            command.Parameters.AddWithValue("@Genre", cd.Genre);
            command.Parameters.AddWithValue("@Year", cd.Year);

            command.ExecuteNonQuery();
        }

        private int InsertCD(CD cd)
        {
            const string query =
                "Insert Into CD(Artist, Title, Genre, Year) Values(@Artist, @Title, @Genre, @Year); SELECT CAST(scope_identity() AS int)";
            var command = new SqlCommand(query, SqlConnection);

            command.Parameters.AddWithValue("@Artist", cd.Artist);
            command.Parameters.AddWithValue("@Title", cd.Title);
            command.Parameters.AddWithValue("@Genre", cd.Genre);
            command.Parameters.AddWithValue("@Year", cd.Year);

            return (int)command.ExecuteScalar();
        }

        public void Delete(int id)
        {
            OpenSqlConnection();

            const string query = "Delete from CD where Id = @Id";
            var command = new SqlCommand(query, SqlConnection);

            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();

        }
    }
}