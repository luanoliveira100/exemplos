
//Cria o calendário
$(document).ready(function () {
    $("#calendar").fullCalendar({
        dayNames: ['Domingo', 'Segunda-feira', 'Terça-feira', 'Quarta-feira', 'Quinta-feira', 'Sexta-feira', 'Sábado'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb'],
        monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outrubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Maio', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
        allDayText: 'Dia inteiro',
        buttonText: {
            today: 'Hoje',
            month: 'Mês',
            week: 'Semana',
            day: 'Dia'
        },

        //Busca os eventos
        events: "/Evento/ListarEventos",

        //Cria o evento
        dayClick: function (date, jsEvent, view) {
            $(this).css('background-color', '#ff2727');
            if (date < moment()) {
                bootbox.alert("Datas anteriores à data atual não podem ser reservadas!", function () {
                        location.reload();                    
                })                
            } else {
            bootbox.confirm("Reservar?", function (confirma) {
                if (confirma) {
                    var converte = date.format();
                    var data = converte.toString();
                    $.ajax({
                        url: "/Evento/CriarEvento",
                        type: "GET",
                        data: { data },
                        success: function (response) {
                            location.reload();
                        },
                        error: function (response) {
                            document.getElementById("erro").innerHTML = response;
                        }
                    })
                }
                else {
                    location.reload();
                }
                })
            }
        },

        // Deleta o evento
        eventClick: function (calEvent, jsEvent, view) {
            $(this).css('border-color', '#ff2727');
            if (calEvent.start < moment()) {
                bootbox.alert("Reservas anteriores à data atual não podem ser deletadas!", function () {
                })
            } else {
                bootbox.confirm("Deletar?", function (confirma) {
                    if (confirma) {
                        var id = calEvent.id;
                        $.ajax({
                            url: "/Evento/DeletarEvento",
                            type: "GET",
                            data: { id },
                            success: function (response) {
                                location.reload();
                            },
                            error: function (response) {
                                document.getElementById("erro").innerHTML = response;
                            }
                        })
                    }
                })
            }
        }
    });
});


