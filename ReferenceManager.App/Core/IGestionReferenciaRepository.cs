using ReferenceManager.App.Models;

namespace ReferenceManager.App.Core
{
    public interface IGestionReferenciaRepository
    {
        public List<GestionReferencium> ObtenerReferencias();
        public List<GestionReferencium> ObtenerReferenciasByIdUser(int idUser);
    }
}
