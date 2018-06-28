$(document).ready(function () {
    debugger;
    var url = window.location.pathname;
    if (url.indexOf("Edit")>0) {
        var id = $("#Id").val();
        ListarItens(id);
    }
});




    function SalvarItens() {
        debugger;
        var quantidade = $("#Quantidade").val();
        var produto = $("#Produto").val();
        var valorunitario = $("#ValorUnitario").val();
        var idPedido = $("#idPedido").val();

        var url = "/NotaFiscalItens/SalvarItens";

        $.ajax({
            url: url
            , data: { Quantidade: quantidade, Produto: produto, Valorunitario: valorunitario, idPedido: idPedido }
            , type: "GET"
            , datatype: "html"
            , success: function (data) {
                if (data.Resultado > 0) {
                    ListarItens(idPedido);
                }
            }
        });
    }


    function SalvarPedido() {

        debugger;
        //Data
        var data = $("#Data").val();

        //Cliente
        var numDaNota = $("#NumeroDaNota").val();

        //Valor
        var valor = $("#Valor").val();

        var token = $('input[name="__RequestVerificationToken"]').val();
        var tokenadr = $('form[action="/Pedido/Create"] input[name="__RequestVerificationToken"]').val();
        var headers = {};
        var headersadr = {};
        headers['__RequestVerificationToken'] = token;
        headersadr['__RequestVerificationToken'] = tokenadr;

        //Gravar
        var url = "/NotaFiscals/Create";

        $.ajax({
            url: url
            , type: "POST"
            , datatype: "json"
            , headers: headersadr
            , data: { Id: 0, Data: data, NumeroDaNota: numDaNota, Valor: valor, __RequestVerificationToken: token }
            , success: function (data) {           
                if (data.Resultado > 0) {
                    debugger;
                    ListarItens(data.Resultado);
                }
            }
        });
    }

    function ListarItens(idPedido) {
        debugger;
        var url = "/NotaFiscalItens/ListarItens";

        $.ajax({
            url: url
            , type: "GET"
            , data: { id: idPedido }
            , datatype: "html"
            , success: function (data) {
                var divItens = $("#divItens");
                divItens.empty();
                divItens.show();
                divItens.html(data);
            }
        });

    }

    function ExcluirItem(id ,id1) {

        debugger;
        var url = "/NotaFiscalItens/Excluir"

        $.ajax({
            url: url,
            data: { id: id },
            datatype: "json",
            type: "POST",            
            success: function (data) {
                if (data.Resultado) {
                    debugger;
                    ListarItens(id1
                        );
                    var linha = "#" + id;
                    $(linha).fadeOut(500);                   
                }
           
            }
        });
    }
  
