using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SqlDemo.Data.Entities;

namespace SqlDemo.Data.Repositories
{
    public class TrackRepository : BaseRepository
    {
        private CDRepository _cdRepository;

        public TrackRepository()
        {
            _cdRepository = new CDRepository();
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
    }
}