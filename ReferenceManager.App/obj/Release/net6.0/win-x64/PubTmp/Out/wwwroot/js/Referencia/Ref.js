var _idReferencia = "";
var _idUsuario = "";

$(function () {
    _idReferencia = $.urlParam('IdReferencia')
    $("#txtIdReferencia").val(_idReferencia);
    $("#txtIdReferencia1").val(_idReferencia);

    _idUsuario = $("#lblUserName").data("id");
    $("#txtIdUser").val(_idUsuario);
    $("#txtIdUser1").val(_idUsuario);

    var view = $("#ScriptRef").data("view");
    callDataReferencia(view);
});
$.urlParam = function (name) {
    var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
    return results[1] || 0;
}

function callDataReferencia(view) {

    if (_idReferencia != null || _idReferencia != "") {
        $.ajax(
            {
                type: 'POST',
                dataType: 'JSON',
                url: '/' + view + '/GetDataReferencia',
                data: { IdReferencia: "" + _idReferencia + "" },
                success: function (response) {
                    printData(response);
                },
                error: function (response) {
                    console.log("Error: " + response);
                }
            });
    }

}

function printData(data) {

    var tr = '<tbody>' +
        '    <tr>' +
        '        <td>' + data.fkClienteNavigation.fullName + '</td>' +
        '        <td>' + data.fkClienteNavigation.tipoIdentificacion + '</td>' +
        '        <td>' + data.fkClienteNavigation.noIdentificacion + '</td>' +
        '        <td>' + data.fkClienteNavigation.nombreNegocio + '</td>' +
        '        <td>' + data.fkClienteNavigation.tipoNegocio + '</td>' +
        '        <td>' + data.fkClienteNavigation.direccionNegocio + '</td>' +
        '        <td>' + data.fkClienteNavigation.tiempoOperacionAno + '</td>' +
        '        <td>' + data.fkClienteNavigation.tiempoOperacionMese + '</td>' +
        '        <td>' + data.fkClienteNavigation.tiempoEstablecimientoAno + '</td>' +
        '        <td>' + data.fkClienteNavigation.tiempoEstablecimientoMes + '</td>' +
        '        <td>' + data.fkClienteNavigation.tipoLocal + '</td>' +
        '        <td>' + data.fkClienteNavigation.tipoVivienda + '</td>' +
        '        <td>' + data.fkClienteNavigation.estadoCivil + '</td>' +
        '    </tr>' +
        '</tbody>';

    $("#idDataCliente").append(tr);
    $("#idDataReferencia").append(" <label><b>Tipo de Referencia:</b> " + data.fkTipoReferenciaNavigation.nombre + " </label> <label> <b>Persona de Contacto:</b> " + data.personaContacto + "   </label><label><b>Numero de Telefono:</b> " + data.telefono + "   </label>");
}

$("#slTipificacion").change(function () {
    if ($(this).val() == "Se verificaron datos") {
        $("#divCreate").show();
        $("#divSaveComentario").hide();
    } else if ($(this).val() == "0") {
        $("#divSaveComentario").hide();
        $("#divCreate").hide();
    } else {
        $("#divSaveComentario").show();
        $("#divCreate").hide();
    }
});