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
                const string query = "Insert Into CD(Artist, Title, Genre, Year) Values(@Artist, @Title, @Genre, @Year); SELECT CAST(scope_identity() AS int)";
                var command = new SqlCommand(query, SqlConnection);

                command.Parameters.AddWithValue("@Artist", cd.Artist);
                command.Parameters.AddWithValue("@Title", cd.Title);
                command.Parameters.AddWithValue("@Genre", cd.Genre);
                command.Parameters.AddWithValue("@Year", cd.Year);

                id = (int)command.ExecuteScalar();

                if (cd.Tracks != null)
                {
                    foreach (var track in cd.Tracks)
                    {
                        const string trackQuery = "Insert Into Track (Name, Length, Artist, CDId) Values(@Name, @Length, @Artist, @CDId); SELECT CAST(scope_identity() AS int)";
                        var trackCommand = new SqlCommand(trackQuery, SqlConnection);

                        trackCommand.Parameters.AddWithValue("@Name", track.Name);
                        trackCommand.Parameters.AddWithValue("@Length", track.Length);
                        trackCommand.Parameters.AddWithValue("@Artist", track.Artist);
                        trackCommand.Parameters.AddWithValue("@CDId", id);

                        trackCommand.ExecuteScalar();
                    }
                }
            }
            else
            {
                const string query = "Update CD Set Artist = @Artist, Title = @Title, Genre = @Genre, Year = @Year Where Id = @Id";
                var command = new SqlCommand(query, SqlConnection);

                command.Parameters.AddWithValue("@Id", cd.Id);
                command.Parameters.AddWithValue("@Artist", cd.Artist);
                command.Parameters.AddWithValue("@Title", cd.Title);
                command.Parameters.AddWithValue("@Genre", cd.Genre);
                command.Parameters.AddWithValue("@Year", cd.Year);

                command.ExecuteNonQuery();

                if (cd.Tracks != null)
                {
                    foreach (var track in cd.Tracks)
                    {
                        const string trackQuery = "Update Track Set Name = @Name, Length = @Length, Artist = @Artist Where Id = @Id";
                        var trackCommand = new SqlCommand(trackQuery, SqlConnection);

                        trackCommand.Parameters.AddWithValue("@Id", track.Id);

                    }
                }
                id = cd.Id;
            }

            return id;
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