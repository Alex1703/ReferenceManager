$(function () {

});

function GeDataCasos(value) {


    $.ajax(
        {
            type: 'POST',
            dataType: 'JSON',
            url: '/Reportes/BuscarCliente',
            data: { identificacion: "" + value + "" },
            async: false,
            success: function (response) {

                if (response != null) {

                    var tipoCliente = 'Titular';
                    PrintComercial(response.fkComercialNavigation);
                    PrintCliente(response.fkClienteNavigation, tipoCliente);
                    PrintReferencias(response.fkClienteNavigation.listaReferencia, tipoCliente);

                    $.ajax(
                        {
                            type: 'POST',
                            dataType: 'JSON',
                            url: '/Reportes/BuscarCoodeudor',
                            data: { identificacion: "" + value + "" },
                            async: false,
                            success: function (response) {
                                var tipoCliente = 'Coodeudor';
                                PrintCliente(response, tipoCliente);
                                PrintReferencias(response.listaReferencia, tipoCliente);

                            },
                            error: function (response) {
                                console.log("Error: " + response);
                            }
                        });
                    $("#btnExportar").show();
                } else {
                    Swal.fire({
                        icon: 'warning',
                        title: 'Alerta',
                        text: 'No se encontro el cliente'
                    })
                }
            },
            error: function (response) {
                console.log("Error: " + response);
            }
        });





}
$("#btnExportar").click(function () {
    window.print();
});

$("#btnBuscarCliente").click(function () {
    var $element = $("#identificacion");
    if ($element.val() != "") {
        GeDataCasos($element.val());
    }
});
function PrintComercial(comercial) {

    var $element = $("#divComercial");

    var html = '<h6>Informacion del Comercial</h6>' +
        '<table>' +
        '<thead>' +
        '    <tr>' +
        '        <th>Nombre</th>' +
        '        <th>Cedula</th>' +
        '        <th>Ciudad</th>' +
        '        <th>Zona</th>' +
        '        <th>Oficina</th>' +
        '    </tr>' +
        '</thead>' +
        '<tbody>' +
        '    <tr>' +
        '        <td>' + comercial.nombre + '</td>' +
        '        <td>' + comercial.cedula + '</td>' +
        '        <td>' + comercial.fkZonaNavigation.ciudad + '</td>' +
        '        <td>' + comercial.fkZonaNavigation.nombre + '</td>' +
        '        <td>' + comercial.fkZonaNavigation.oficina + '</td>' +
        '    </tr>' +
        '</tbody>' +
        '</table>';

    $element.append(html);

}

function PrintCliente(cliente, TipoCliente) {
    var divHijo = TipoCliente == "Titular" ? ".divTitular" : ".divCodeudor";
    var $element = $("#divClientes " + divHijo);

    var html = '<h6>Informacion del ' + TipoCliente + '</h6>' +
        '<table>' +
        '<thead>' +
        '    <tr>' +
        '        <th>Nombre del Cliente</th>' +
        '        <th>Tipo Identificacion</th>' +
        '        <th>Numero Identificacion</th>' +
        '        <th>Nombre de Negocio</th>' +
        '        <th>Tipo de Negocio</th>' +
        '        <th>Direccion Negocio</th>' +
        '        <th>Tiempo de Operacion (Años)</th>' +
        '        <th>Tiempo de Operacion (Meses)</th>' +
        '        <th>Tiempo de Operacion del Establecimiento(Meses)</th>' +
        '        <th>Tiempo de Operacion del Establecimiento(Años)</th>' +
        '        <th>Tipo Local</th>' +
        '        <th>Tipo Vivienda</th>' +
        '        <th>Estado Civil</th>' +
        '    </tr>' +
        '</thead>' +
        '<tbody>' +
        '    <tr>' +
        '        <td>' + cliente.fullName + '</td>' +
        '        <td>' + cliente.tipoIdentificacion + '</td>' +
        '        <td>' + cliente.noIdentificacion + '</td>' +
        '        <td>' + cliente.nombreNegocio + '</td>' +
        '        <td>' + cliente.tipoNegocio + '</td>' +
        '        <td>' + cliente.direccionNegocio + '</td>' +
        '        <td>' + cliente.tiempoOperacionAno + '</td>' +
        '        <td>' + cliente.tiempoOperacionMese + '</td>' +
        '        <td>' + cliente.tiempoEstablecimientoAno + '</td>' +
        '        <td>' + cliente.tiempoEstablecimientoMes + '</td>' +
        '        <td>' + cliente.tipoLocal + '</td>' +
        '        <td>' + cliente.tipoVivienda + '</td>' +
        '        <td>' + cliente.estadoCivil + '</td>' +
        '    </tr>' +
        '</tbody>' +
        '</table>';

    $element.append(html);
}
function PrintReferencias(referencias, TipoCliente) {
    var divHijo = TipoCliente == "Titular" ? ".divTitular" : ".divCodeudor";
    var $element = $("#divClientes " + divHijo);

    var html = '<h6>Informacion de las  referencias del ' + TipoCliente + '</h6>' +
        '<table class="table">' +
        '    <thead class="thead-dark">' +
        '        <tr>' +
        '            <th>Persona de Contacto</th>' +
        '            <th>Telefono</th>' +
        '            <th>Tipo de Referencia</th>' +
        '            <th>Estado</th>' +
        '            <th>Nombre del Analista</th>' +
        '        </tr>' +
        '    </thead>';

    var rows = "";
    $.each(referencias, function (index, element) {

        rows += '' +
            '<tr> ' +
            '   <td>' + element.personaContacto + '</td>' +
            '   <td>' + element.telefono + '</td>' +
            '   <td>' + element.fkTipoReferenciaNavigation.nombre + '</td>' +
            '   <td>' + element.estado + '</td>' +
            '   <td>' + element.fkUsuarioNavigation.nombre + '</td>' +
            '</tr>';

        if (element.detalleComunicacions.length)
            rows += PrintDetalleCasos(element.detalleComunicacions);
    });

    html += '<tbody>' + rows + '    </tbody> </table>';

    $element.append(html);
}

function PrintDetalleCasos(element) {
    var htmlDetalle = '';
    $.each(element, function (index, element) {

        if (element.fkRefArrendadorLocal != null)
            htmlDetalle += PrintDetalleCasosRefLocal(element.fkRefArrendadorLocalNavigation, element.descripcion);

        else if (element.fkRefArrendadorVivienda != null)
            htmlDetalle += PrintDetalleCasosRefVivienda(element.fkRefArrendadorViviendaNavigation, element.descripcion)

        else if (element.fkRefFamiliar != null)
            htmlDetalle += PrintDetalleCasosRefFamiliar(element.fkRefFamiliarNavigation, element.descripcion);

        else if (element.fkRefProveedor != null)
            htmlDetalle += PrintDetalleCasosRefProveedor(element.fkRefProveedorNavigation, element.descripcion);
        else
            htmlDetalle += PrintDetalleCasosComentario(element)

    });


    return '<tr><td colspan="5"><center>' + htmlDetalle + '</center></td></tr>';

}

function PrintDetalleCasosRefFamiliar(data, comentario) {
    var html = '' +
        '<table style="width:80%">' +
        '<tr><td style="width:50%">Comentario:</td><td>' + comentario + '</td></tr>' +
        '<tr><td style="width:50%">Me confirma su nombre completo, por favor</td><td>' + data.confirmacionNombre + '</td></tr>' +
        '<tr><td style="width:50%">¿Qué parentesco tiene con el señor (a)?</td><td>' + data.parentezco + '</td></tr>' +
        '<tr><td style="width:50%">¿Qué estado civil tiene el señor (a)?</td><td>' + data.estadoCivil + '</td></tr>' +
        '<tr><td style="width:50%">¿Cómo se llama el cónyuge de él (ella)?</td><td>' + data.nombreConyuge + '</td></tr>' +
        '<tr><td style="width:50%">¿Cuántos hijos tiene él (ella)?</td><td>' + data.cantidadHijos + '</td></tr>' +
        '<tr><td style="width:50%">¿Con quién vive él (ella)?</td><td>' + data.quienVive + '</td></tr>' +
        '<tr><td style="width:50%">¿A qué se dedica su familiar?</td><td>' + data.actividad + '</td></tr>' +
        '<tr><td style="width:50%">¿Cuánto tiempo lleva con el negocio?</td><td>' + data.tiempoNegocio + '</td></tr>' +
        '<tr><td style="width:50%">¿En qué barrio o sector tiene el negocio?</td><td>' + data.barrioNegocio + '</td></tr>' +
        '<tr><td style="width:50%">¿Cuántos empleados tiene en el negocio?</td><td>' + data.cantidadEmpleados + '</td></tr>' +
        '<tr><td style="width:50%">¿Cuál es su dirección y teléfono?</td><td>' + data.direccionTelefono + '</td></tr>' +
        '<tr><td style="width:50%">¿Qué concepto tiene usted del señor (a)?</td><td>' + data.concepto + '</td></tr>' +
        '</table>';

    return html;
}
function PrintDetalleCasosRefLocal(data, comentario) {
    var html = '' +
        '<table style="width:80%">' +
        '<tr><td  style="width:50%">Comentario:</td><td>' + comentario + '</td></tr>' +
        '<tr><td  style="width:50%">¿Qué le arrienda al señor (a)?</td><td>' + data.queArrienda + '</td>    </tr>' +
        '<tr><td  style="width:50%">¿Hace cuánto le tiene arrendado?</td><td>' + data.tiempoArriendo + '</td>    </tr>' +
        '<tr><td  style="width:50%">¿Qué tipo de negocio tiene el señor (a)</td><td>' + data.tipoNegocio + '</td>    </tr>' +
        '<tr><td  style="width:50%">¿Cuántos empleados tiene en el negocio?</td><td>' + data.cantidadEmpleados + '</td>    </tr>' +
        '<tr><td  style="width:50%">¿Cuál es la dirección del inmueble que le arrienda?</td><td>' + data.direccionLocal + '</td>    </tr>' +
        '<tr><td  style="width:50%">¿Con quién vive él (ella)?</td><td>' + data.quienVive + '</td>    </tr>' +
        '<tr><td  style="width:50%">¿Qué estado civil tiene el señor (a)?</td><td>' + data.estadoCivil + '</td>    </tr>' +
        '<tr><td  style="width:50%">¿Cómo se llama el/la esposo (a) de su inquilino?</td><td>' + data.nombreConyuge + '</td>    </tr>' +
        '<tr><td  style="width:50%">¿Cuánto es el canon del arriendo? </td><td>' + data.canonArriendo + '</td>    </tr>' +
        '<tr><td  style="width:50%">¿Incluye servicios el canon de arriendo o son aparte?</td><td>' + data.incluyeServicios + '</td>    </tr>' +
        '<tr><td  style="width:50%">¿Ha sido puntual y responsable con el pago del arriendo?</td><td>' + data.puntualResponsable + '</td>    </tr>' +
        '<tr><td  style="width:50%">¿Qué concepto tiene usted del señor (a)?</td><td>' + data.concepto + '</td>    </tr>' +
        '</table>';
    return html;
}
function PrintDetalleCasosRefVivienda(data, comentario) {

    var html = '' +
        '<table style="width:80%">' +
        '<tr><td style="width:50%">Comentario:</td><td>' + comentario + '</td></tr>' +
        '<tr><td style="width:50%">¿Qué le arrienda al señor (a)?</td><td> ' + data.queArrienda + '</td></tr>' +
        '<tr><td style="width:50%">¿Con quién vive él (ella)?</td><td> ' + data.quienVive + '</td></tr>' +
        '<tr><td style="width:50%">¿Qué estado civil tiene el señor (a)?</td><td> ' + data.estadoCivil + '</td></tr>' +
        '<tr><td style="width:50%">¿Cómo se llama el/la esposo (a) de su inquilino?</td><td> ' + data.nombreConyuge + '</td></tr>' +
        '<tr><td style="width:50%">¿A qué se dedica su inquilino?</td><td> ' + data.actividad + '</td></tr>' +
        '<tr><td style="width:50%">¿Hace cuánto le tiene arrendado?</td><td> ' + data.tiempoArriendo + '</td></tr>' +
        '<tr><td style="width:50%">¿Cuál es la dirección de esa vivienda?</td><td> ' + data.direccionVivienda + '</td></tr>' +
        '<tr><td style="width:50%">¿Cuánto es el canon del arriendo? </td><td> ' + data.canonArriendo + '</td></tr>' +
        '<tr><td style="width:50%">¿Incluye servicios el canon de arriendo o son aparte?</td><td> ' + data.incluyeServicios + '</td></tr>' +
        '<tr><td style="width:50%">¿Ha sido puntual y responsable con el pago del arriendo?</td><td> ' + data.puntualResponsable + '</td></tr>' +
        '<tr><td style="width:50%">¿Qué concepto tiene usted del señor (a)?</td><td> ' + data.concepto + '</td></tr>' +
        '</table>';
    return html;
}
function PrintDetalleCasosRefProveedor(data, comentario) {

    var html = '' +
        '<table style="width:80%">' +
        '<tr><td  style="width:50%">Comentario:</td><td>' + comentario + '</td></tr>' +
        '<tr><td  style="width:50%">Me confirma su nombre completo, por favor<</td><td>' + data.confirmarNombre + '</td></tr>' +
        '<tr><td  style="width:50%">¿Cuál es su cargo?</td><td>' + data.cargo + '</td></tr>' +
        '<tr><td  style="width:50%">¿Cuál es la dirección de su negocio/empresa?</td><td>' + data.direccionProveedor + '</td></tr>' +
        '<tr><td  style="width:50%">Me confirma el teléfono fijo de su negocio o empresa</td><td>' + data.telefonoProveedor + '</td></tr>' +
        '<tr><td  style="width:50%">¿Cuánto tiempo lleva vendiéndole al señor (a)? <</td><td>' + data.tiempoVenta + '</td></tr>' +
        '<tr><td  style="width:50%">¿Qué productos le vende Ud. a él (a)</td><td>' + data.productoVenta + '</td></tr>' +
        '<tr><td  style="width:50%">¿Le Compra Diario -Semanal -Quincenal -Mensual? <</td><td>' + data.frecuenciaCompra + '</td></tr>' +
        '<tr><td  style="width:50%">¿Cuánto es el promedio en dinero de compra?</td><td>' + data.promedioCompra + '</td></tr>' +
        '<tr><td  style="width:50%">¿De cuánto fue la última compra?</td><td>' + data.valorUltimaCompra + '</td></tr>' +
        '<tr><td  style="width:50%">¿Le vende de contado o a crédito?</td><td>' + data.contadoCredito + '</td></tr>' +
        '<tr><td  style="width:50%">¿Qué plazo le da para pagar el crédito?</td><td>' + data.plazoCredito + '</td></tr>' +
        '<tr><td  style="width:50%">¿De cuánto es el cupo de crédito?</td><td>' + data.cupoCredito + '</td></tr>' +
        '<tr><td  style="width:50%">¿Le cancela en cheque, efectivo o tarjeta?</td><td>' + data.pagoCredito + '</td></tr>' +
        '<tr><td  style="width:50%">¿Qué concepto tiene usted del señor (a)</td><td>' + data.concepto + '</td></tr>' +
        '</table>';
    return html;
}
function PrintDetalleCasosComentario(data) {

    var html = '' +
        '<table style="width:80%">' +
        '<tr><td  style="width:50%">Comentario:</td><td>' + data.descripcion.split("|")[1] + '</td></tr>' +
        '</table>';
    return html;
}