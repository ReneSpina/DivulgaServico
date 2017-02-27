/*Início Jquery Home*/

$(document).ready(function () {
    $('#myCarousel').carousel({
        interval: 4000
    });

    var clickEvent = false;
    $('#myCarousel').on('click', '.nav a', function () {
        clickEvent = true;
        $('.nav li').removeClass('active');
        $(this).parent().addClass('active');
    }).on('slid.bs.carousel', function (e) {
        if (!clickEvent) {
            var count = $('.nav').children().length - 1;
            var current = $('.nav li.active');
            current.removeClass('active').next().addClass('active');
            var id = parseInt(current.data('slide-to'));
            if (count == id) {
                $('.nav li').first().addClass('active');
            }
        }
        clickEvent = false;
    });
});

/*Fim Jquery Home*/

/*Início do Jquary de validação da tela de manutenção de perfil do prestador de serviço (EditarPerfil)*/

$('#salvarPerfil').click(function () {

    if ($("#NM_NOME_PESSOA").val() == "" || $("#NM_NOME_PESSOA").val() == null || $("#CD_CNPJ").val() == "" || $("#CD_CNPJ").val() == null || $("#Email").val() == "" || $("#Email").val() == null)
    {
        alert("Os campos com * não podem ser vazios!");
        return false;
    }
});
    
/*Fim do Jquary de validação da tela de manutenção de perfil do prestador de serviço (EditarPerfil)*/

$('#VALOR_PRODUTO').mask('000000000000000,00', { reverse: true });