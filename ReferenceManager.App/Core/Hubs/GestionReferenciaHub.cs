using Microsoft.AspNetCore.SignalR;
using ReferenceManager.App.Models;

namespace ReferenceManager.App.Core.Hubs
{
    public class GestionReferenciaHub : Hub
    {
        private IConfiguration _configuration;
        private IGestionReferenciaRepository _repository;
        public GestionReferenciaHub(IConfiguration configuration,IGestionReferenciaRepository repository)
        {
            _configuration = configuration;
            _repository = repository;

        }

        public async Task SendGestionReferencia()
        {
            var ListReferencias = _repository.ObtenerReferencias();
            await Clients.All.SendAsync("ReceivedGestionReferencia", ListReferencias);
        }

        public async Task SendGestionReferenciaByUser(int idUser)
        {
            var ListReferencias = _repository.ObtenerReferenciasByIdUser(idUser);
            await Clients.All.SendAsync("ReceivedGestionReferenciaByUser", ListReferencias);
        }
    }
}
