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

    /*Mascara para tela de cadastro de produtos do fornecedor*/
        $('#VALOR_PRODUTO').mask('000000000000000,00', { reverse: true });
    /*Mascara para tela de cadastro de produtos do fornecedor*/
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


//Inicio Valida CPF ou CNPJ
function campo_numerico() {

    if (event.keyCode < 45 || event.keyCode > 57) event.returnValue = false;

}


/*function cnpj_cpf verifica qual das funcoes tem que chamar cpf ou cnpj*/

function cnpj_cpf(campo, documento, f, formi) {

    form = formi;

    for (Count = 0; Count < 2; Count++) {

        if (form.rad[Count].checked)
            break;
    }


    if (Count == 0) {
        mascara_cpf(campo, documento, f);
    }

    else {
        mascara_cnpj(campo, documento, f);
    }
}

function mascara_cnpj(campo, documento, f) {
    var mydata = '';
    mydata = mydata + documento;

    if (mydata.length == 2) {
        mydata = mydata + '.';

        ct_campo = eval("document." + f + "." + campo + ".value = mydata");
        ct_campo;
    }

    if (mydata.length == 6) {
        mydata = mydata + '.';

        ct_campo = eval("document." + f + "." + campo + ".value = mydata");
        ct_campo;
    }

    if (mydata.length == 10) {
        mydata = mydata + '/';

        ct_campo1 = eval("document." + f + "." + campo + ".value = mydata");
        ct_campo1;
    }

    if (mydata.length == 15) {
        mydata = mydata + '-';

        ct_campo1 = eval("document." + f + "." + campo + ".value = mydata");
        ct_campo1;
    }

    if (mydata.length == 18) {

        valida_cnpj(f, campo);
    }
}


function mascara_cpf(campo, documento, f) {
    var mydata = '';
    mydata = mydata + documento;

    if (mydata.length == 3) {
        mydata = mydata + '.';

        ct_campo = eval("document." + f + "." + campo + ".value = mydata");
        ct_campo;
    }

    if (mydata.length == 7) {
        mydata = mydata + '.';

        ct_campo = eval("document." + f + "." + campo + ".value = mydata");
        ct_campo;
    }

    if (mydata.length == 11) {
        mydata = mydata + '-';

        ct_campo1 = eval("document." + f + "." + campo + ".value = mydata");
        ct_campo1;
    }

    if (mydata.length == 14) {

        valida_cpf(f, campo);
    }

}


function valida_cnpj(f, campo) {

    pri = eval("document." + f + "." + campo + ".value.substring(0,2)");
    seg = eval("document." + f + "." + campo + ".value.substring(3,6)");
    ter = eval("document." + f + "." + campo + ".value.substring(7,10)");
    qua = eval("document." + f + "." + campo + ".value.substring(11,15)");
    qui = eval("document." + f + "." + campo + ".value.substring(16,18)");

    var i;
    var numero;
    var situacao = '';

    numero = (pri + seg + ter + qua + qui);

    s = numero;


    c = s.substr(0, 12);
    var dv = s.substr(12, 2);
    var d1 = 0;

    for (i = 0; i < 12; i++) {
        d1 += c.charAt(11 - i) * (2 + (i % 8));
    }

    if (d1 == 0) {
        var result = "falso";
    }
    d1 = 11 - (d1 % 11);

    if (d1 > 9) d1 = 0;

    if (dv.charAt(0) != d1) {
        var result = "falso";
    }

    d1 *= 2;
    for (i = 0; i < 12; i++) {
        d1 += c.charAt(11 - i) * (2 + ((i + 1) % 8));
    }

    d1 = 11 - (d1 % 11);
    if (d1 > 9) d1 = 0;

    if (dv.charAt(1) != d1) {
        var result = "falso";
    }


    if (result == "falso") {
        alert("Por favor, digite um CNPJ válido!");
        aux1 = eval("document." + f + "." + campo + ".focus");
        aux2 = eval("document." + f + "." + campo + ".value = ''");

    }
}


function valida_cpf(f, campo) {

    pri = eval("document." + f + "." + campo + ".value.substring(0,3)");
    seg = eval("document." + f + "." + campo + ".value.substring(4,7)");
    ter = eval("document." + f + "." + campo + ".value.substring(8,11)");
    qua = eval("document." + f + "." + campo + ".value.substring(12,14)");

    var i;
    var numero;

    numero = (pri + seg + ter + qua);

    s = numero;
    c = s.substr(0, 9);
    var dv = s.substr(9, 2);
    var d1 = 0;

    for (i = 0; i < 9; i++) {
        d1 += c.charAt(i) * (10 - i);
    }

    if (d1 == 0) {
        var result = "falso";
    }

    d1 = 11 - (d1 % 11);
    if (d1 > 9) d1 = 0;

    if (dv.charAt(0) != d1) {
        var result = "falso";
    }

    d1 *= 2;
    for (i = 0; i < 9; i++) {
        d1 += c.charAt(i) * (11 - i);
    }

    d1 = 11 - (d1 % 11);
    if (d1 > 9) d1 = 0;

    if (dv.charAt(1) != d1) {
        var result = "falso";
    }


    if (result == "falso") {
        alert("Por favor, digite um CPF válido!");
        aux1 = eval("document." + f + "." + campo + ".focus");
        aux2 = eval("document." + f + "." + campo + ".value = ''");

    }
}

//Fim valida cpf ou cnpj