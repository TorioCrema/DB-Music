using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDBMusicLabel.musiclabeldb;

namespace WpfDBMusicLabel.Model
{
    public class QueryGenerator
    {
        readonly private MySqlConnection _conn;

        public QueryGenerator(MySqlConnection conn)
        {
            _conn = conn;
        }

        /// <summary>
        /// Returns a <see cref="IList{Album}"/> containing the discography
        /// of the music project represented by the given ID.
        /// </summary>
        /// <param name="IdProgetto">the ID of the music project</param>
        /// <returns></returns>
        public IList<Album> Discography(int IdProgetto)
        {
            List<Album> result = new();
            string query = "SELECT * FROM Album WHERE ID_Progetto = @progetto";
            using var cmd = new MySqlCommand(query, _conn);
            cmd.Parameters.AddWithValue("progetto", IdProgetto);
            cmd.Prepare();
            using MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Album(reader.GetInt32(0), reader.GetString(1),
                    reader.GetInt32(2), reader.GetInt32(3), reader.GetDateTime(4)));
            }
            return result;
        }

        /// <summary>
        /// Returns a <see cref="IList{Traccia}"/> containing all
        /// tracks that feature the project with id <paramref name="IdProgetto"/>.
        /// </summary>
        /// <param name="IdProgetto">the id of the project</param>
        /// <returns></returns>
        public IList<Traccia> FeatureFromProject(int IdProgetto)
        {
            List<Traccia> result = new();
            string query = "SELECT T.ID_Traccia, T.Nome, T.Data FROM Feature F join Traccia T on (F.ID_Traccia = T.ID_Traccia) WHERE F.ID_Progetto = @progetto";
            using var cmd = new MySqlCommand(query, _conn);
            cmd.Parameters.AddWithValue("progetto", IdProgetto);
            cmd.Prepare();
            using MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Traccia(reader.GetInt32(0), reader.GetString(1),
                    reader.GetInt32(2), reader.GetDateTime(3), reader.GetInt32(4),
                    reader.GetString(5)));
            }
            return result;
        }

        /// <summary>
        /// Returns a <see cref="IList{T}"/> containing all
        /// the merchandising of the project with id <paramref name="idProgetto"/>.
        /// </summary>
        /// <param name="idProgetto">the id of the project</param>
        /// <returns></returns>
        public IList<Merchandising> MerchFromProject(int idProgetto)
        {
            List<Merchandising> result = new();
            string query = "SELECT Codice, Descrizione, Prezzo, QtaProdotta, CostoFornituraUnitario, ID_Progetto, ID_Produttore FROM Merchandising WHERE ID_Progetto = @progetto;";
            using var cmd = new MySqlCommand(query, _conn);
            cmd.Parameters.AddWithValue("progetto", idProgetto);
            cmd.Prepare();
            using MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Merchandising(reader.GetInt32(0), reader.GetString(1),
                    reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4),
                    reader.GetInt32(5), reader.GetInt32(6)));
            }
            return result;
        }

        /// <summary>
        /// Returns a <see cref="IList{T}"/> containing
        /// all products related to the album with id <paramref name="idAlbum"/>.
        /// </summary>
        /// <param name="idAlbum">the id of the album</param>
        /// <returns></returns>
        public IList<Prodotto> ProdFromAlbum(int idAlbum)
        {
            List<Prodotto> result = new();
            string query = "SELECT * from Prodotto WHERE ID_Album = @album;";
            using var cmd = new MySqlCommand(query, _conn);
            cmd.Parameters.AddWithValue("album", idAlbum);
            cmd.Prepare();
            using MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Prodotto(reader.GetInt32(0), reader.GetString(1),
                    reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4),
                    reader.GetDateTime(5), reader.GetString(6), reader.GetInt32(7),
                    reader.GetInt32(8), reader.GetInt32(9)));
            }
            return result;
        }
    }
}
