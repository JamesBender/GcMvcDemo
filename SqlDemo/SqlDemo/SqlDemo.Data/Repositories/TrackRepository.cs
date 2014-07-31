using System.Collections.Generic;
using System.Data.SqlClient;
using SqlDemo.Data.Entities;

namespace SqlDemo.Data.Repositories
{
    public class TrackRepository : BaseRepository
    {
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
    }
}