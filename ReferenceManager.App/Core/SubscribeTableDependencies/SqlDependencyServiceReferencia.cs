using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using ReferenceManager.App.Core.Hubs;
using ReferenceManager.App.Models;
using TableDependency.SqlClient;

namespace ReferenceManager.App.Core.SubscribeTableDependencies
{
    public class SqlDependencyServiceReferencia : ISqlDependencyService
    {
        private readonly IConfiguration _configuration;
        private GestionReferenciaHub _hubContext;
        private SqlTableDependency<GestionReferencium> _tableDependency;

        public SqlDependencyServiceReferencia(IConfiguration configuration, GestionReferenciaHub hubContext)
        {
            _configuration = configuration;
            _hubContext = hubContext;
        }

        public void SubscribeTableDependency()
        {
            _tableDependency = new SqlTableDependency<GestionReferencium>(_configuration.GetConnectionString("DataBaseReferencias"));
            _tableDependency.OnChanged += TableDependency_OnChanged;
            _tableDependency.OnError += TableDependency_OnError;
            _tableDependency.Start();
        }

        private void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<GestionReferencium> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                _hubContext.SendGestionReferencia();
            }
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(GestionReferencium)} SqlTableDependency error: {e.Error.Message}");
        }


    }
}
