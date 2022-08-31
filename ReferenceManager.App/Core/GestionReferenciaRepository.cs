using ReferenceManager.App.Models;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ReferenceManager.App.Core
{
    public class GestionReferenciaRepository : IGestionReferenciaRepository
    {
        private IConfiguration _configuration;
        public GestionReferenciaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void AutoAsignacion(int idUser)
        {//AutoAsignacionReferencia
            throw new NotImplementedException();
        }

        public List<GestionReferencium> ObtenerReferencias()
        {
            try
            {

                List<GestionReferencium> list = new List<GestionReferencium>();

                DataTable data = new DataTable();
                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DataBaseReferencias")))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT GR.Id,GR.Fk_ListaReferencia,GR.Fk_Usuario,GR.FullUrlRef,GR.Estado,TR.Nombre [TipoReferencia],LR.PersonaContacto FROM GestionReferencia GR inner join ListaReferencia LR On GR.Fk_ListaReferencia = LR.Id inner join TipoReferencia TR On LR.Fk_TipoReferencia = TR.Id Where GR.Estado IN ('Asignado','Encola')", conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            
                            while (reader.Read())
                            {
                                var record = (IDataRecord)reader;
                                var gestionReferencia = new GestionReferencium()
                                {
                                    Id = Convert.ToInt32(record[0].ToString()),
                                    FkListaReferencia = record[1].Equals(DBNull.Value) ? 0 : Convert.ToInt32(record[1].ToString()),
                                    FkUsuario = record[2].Equals(DBNull.Value) ? 0 : Convert.ToInt32(record[2].ToString()),
                                    FullUrlRef = record[3].ToString(),
                                    Estado = record[4].ToString(),
                                    FkListaReferenciaNavigation = new ListaReferencium()
                                    {
                                        PersonaContacto = record[6].ToString(),
                                        FkTipoReferenciaNavigation = new TipoReferencium()
                                        {
                                            Nombre = record[5].ToString()
                                        }
                                    }
                                };
                                list.Add(gestionReferencia);
                            }
                            
                        }
                    }

                }

                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<GestionReferencium> ObtenerReferenciasByIdUser(int idUser)
        {
            try
            {

                List<GestionReferencium> list = new List<GestionReferencium>();

                DataTable data = new DataTable();
                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DataBaseReferencias")))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT GR.Id,GR.Fk_ListaReferencia,GR.Fk_Usuario,GR.FullUrlRef,GR.Estado,TR.Nombre [TipoReferencia] FROM GestionReferencia GR inner join ListaReferencia LR On GR.Fk_ListaReferencia = LR.Id inner join TipoReferencia TR On LR.Fk_TipoReferencia = TR.Nombre Where GR.Fk_Usuario = "+idUser +" AND GR.Estado IN ('Asignado','Encola')", conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                var record = (IDataRecord)reader;
                                var gestionReferencia = new GestionReferencium()
                                {
                                    Id = Convert.ToInt32(record[0].ToString()),
                                    FkListaReferencia = record[1].Equals(DBNull.Value) ? 0 : Convert.ToInt32(record[1].ToString()),
                                    FkUsuario = record[2].Equals(DBNull.Value) ? 0 : Convert.ToInt32(record[2].ToString()),
                                    FullUrlRef = record[3].ToString(),
                                    Estado = record[4].ToString(),
                                    FkListaReferenciaNavigation = new ListaReferencium() {
                                        FkTipoReferenciaNavigation = new TipoReferencium() 
                                        {
                                            Nombre = record[5].ToString()
                                        }
                                    }
                                };
                                list.Add(gestionReferencia);
                            }

                        }
                    }

                }

                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
