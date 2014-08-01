using System.Collections.Generic;
using System.Data.SqlClient;
using SqlDemo.Data.Entities;

namespace SqlDemo.Data.Repositories
{
    public class TrackRepository : BaseRepository
    {
        private readonly CDRepository _cdRepository;

        public TrackRepository(CDRepository cdRepository)
        {
            // This needs an instacne of the CD repository, 
            // and the CD repository needs an instance of this.
            // To avoide a stack overflow issues (circular
            // creation of objects until I run out of memeory)
            // I am passing in an instance of CD repository to
            // this class
            _cdRepository = cdRepository;
        }

        public IList<Track> GetListOfTracksByCd(int cdId)
        {
            OpenSqlConnection();

            const string query = "Select Id, Name, Length, Artist from Track where CDId = @CdId";
            var command = new SqlCommand(query, SqlConnection);
            command.Parameters.AddWithValue("@CdId", cdId);

            var listOfTracks = new List<Track>();

            var dataReader = command.ExecuteReader();

            try
            {
                while (dataReader.Read())
                {
                    var track = new Track
                    {
                        Id = (int) dataReader["Id"],
                        Name = dataReader["Name"].ToString(),
                        Length = (int) dataReader["Length"],
                        Artist = dataReader["Artist"].ToString()
                    };

                    listOfTracks.Add(track);
                }
            }
            finally
            {
                dataReader.Close();
            }
            return listOfTracks;
        }

        public Track GetTrackDetails(int id)
        {
            OpenSqlConnection();
            var query = "Select Id, Name, Length, Artist, CDId from Track where Id = @Id";
            var command = new SqlCommand(query, SqlConnection);
            command.Parameters.AddWithValue("@Id", id);

            var dataReader = command.ExecuteReader();

            try
            {
                dataReader.Read();

                var track = new Track
                {
                    Artist = dataReader["Artist"].ToString(),
                    Id = (int) dataReader["Id"],
                    Length = (int) dataReader["Length"],
                    Name = dataReader["Name"].ToString()
                };

                var cd = _cdRepository.GetById((int) dataReader["CDId"]);

                track.CD = cd;

                return track;
            }
            finally
            {
                dataReader.Close();
            }
        }

        public int Save(Track track)
        {
            if (track.Id == 0)
            {
                return InsertTrack(track.CD.Id, track);
            }
            return UpdateTrack(track);
        }

        private int UpdateTrack(Track track)
        {
            OpenSqlConnection();
            const string trackQuery =
                "Update Track Set Name = @Name, Length = @Length, Artist = @Artist Where Id = @Id";
            var trackCommand = new SqlCommand(trackQuery, SqlConnection);

            trackCommand.Parameters.AddWithValue("@Id", track.Id);
            trackCommand.Parameters.AddWithValue("@Name", track.Name);
            trackCommand.Parameters.AddWithValue("@Length", track.Length);
            trackCommand.Parameters.AddWithValue("@Artist", track.Artist);

            trackCommand.ExecuteNonQuery();

            return track.Id;
        }

        private int InsertTrack(int cdId, Track track)
        {
            var trackInsertQuery =
                "Insert Into Track (Name, Length, Artist, CDId) Values(@Name, @Length, @Artist, @CDId); SELECT CAST(scope_identity() AS int)";
            var trackInsertCommand = new SqlCommand(trackInsertQuery, SqlConnection);

            trackInsertCommand.Parameters.AddWithValue("@Name", track.Name);
            trackInsertCommand.Parameters.AddWithValue("@Length", track.Length);
            trackInsertCommand.Parameters.AddWithValue("@Artist", track.Artist);
            trackInsertCommand.Parameters.AddWithValue("@CDId", cdId);

            return (int)trackInsertCommand.ExecuteScalar();
        }

        public void Delete(int id)
        {
            OpenSqlConnection();
            var query = "Delete from Track where Id = @Id";
            var command = new SqlCommand(query, SqlConnection);

            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }
    }
}