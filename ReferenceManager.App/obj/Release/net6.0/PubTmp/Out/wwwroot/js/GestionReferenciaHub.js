var connection = new signalR.HubConnectionBuilder()
    .withUrl("/GestionReferenciaHub")
    .build();

$(function () {
    connection.start().then(function () {
        console.log('Connected to dashboardHub');
        InvokeReferencias();


    }).catch(function (err) {
        return console.error(err.toString());
    });
});

function InvokeReferencias() {
    connection.invoke("SendGestionReferencia").catch(function (err) {
        return console.error(err.toString());
    });
}
connection.on("ReceivedGestionReferencia", function (ListReferencias) {
    
    if (ListReferencias != null & ListReferencias.length > 0) {
        BindListReferenciasInConsole(ListReferencias);
    }

});


function InvokeReferenciasById() {
    var idUser = parseInt($("#lblUserName").data("id"));
    connection.invoke("SendGestionReferenciaByUser", idUser).catch(function (err) {
        return console.error(err.toString());
    });
}
connection.on("ReceivedGestionReferenciaByUser", function (ListReferencias) {
    
    if (ListReferencias != null & ListReferencias.length > 0) {
        BindListReferenciasInConsole(ListReferencias);
    }

});

connection.on("ReceivedGestionReferencia", function (ListReferencias) {
    var perfil = $("#lblUserActor").text();
    var idUser = parseInt($("#lblUserName").data("id"));
    var List = [];
    if (ListReferencias != null)
        List = ListReferencias.filter(x => x.fkUsuario == idUser);

    if (List.length>0) {
        BindListReferenciasInConsole(ListReferencias);
        $("#lblCountReferencias").show();
    } else if (perfil == "Auxiliar") {
        AutoAsignarReferencia();
    } else {
        $("#lblCountReferencias").hide();
    }
});

function BindListReferenciasInConsole(ListReferencias) {
    
    var notiItem =
        '<h6 style="margin-top: 5px;text-align: center;"> Referencias </h6>                            \
        <hr style="margin: 5px;width: 100%;margin-left: 0px;"/>                                        \
        <div class="notiItem" style = "margin: 10px;" >                                                \
            <a id = "linkNotiItem" href = "{UrlFull}" style = " color: #000;  text-decoration: none;">      \
                <span style="font-weight:bolder">Tipo de Referencia </span><br/>                     \
                <span>{TipoRef}</span><br/>                                                           \
                <span style="font-weight:bolder">Nombre del Cliente</span><br/>                                \
                <span>{Nombre}</span><br/>                                                           \
            </a>                                                                                     \
            <hr/>                                                                                    \
        </div> ';

    var content = $("#notiContent");
    content.children().remove();

    var idUser = parseInt($("#lblUserName").data("id"));

    var List = ListReferencias.filter(x => x.fkUsuario == idUser);

    $("#lblCountReferencias").text(List.length)

    $.each(List, function (i, item) {
        
        notiItem = notiItem.replace(/{TipoRef}/g, item.fkListaReferenciaNavigation.fkTipoReferenciaNavigation.nombre);
        notiItem = notiItem.replace(/{Nombre}/g, item.fkListaReferenciaNavigation.personaContacto);
        notiItem = notiItem.replace(/{UrlFull}/g, item.fullUrlRef);

        content.append(notiItem)
    });

}


function AutoAsignarReferencia()
{
    var idUser = parseInt($("#lblUserName").data("id"));
    connection.invoke("AutoAsignacionGestionReferencia", idUser).catch(function (err) {
        return console.error(err.toString());
    });
}