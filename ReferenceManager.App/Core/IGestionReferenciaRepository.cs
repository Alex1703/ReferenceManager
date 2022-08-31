using ReferenceManager.App.Models;

namespace ReferenceManager.App.Core
{
    public interface IGestionReferenciaRepository
    {
        void AutoAsignacion(int idUser);
        public List<GestionReferencium> ObtenerReferencias();
        public List<GestionReferencium> ObtenerReferenciasByIdUser(int idUser);
    }
}
