$(document).ready(function () {

    var navListItems = $('div.setup-panel div a'),
            allWells = $('.setup-content'),
            allNextBtn = $('.nextBtn');

    allWells.hide();

    navListItems.click(function (e) {
        e.preventDefault();
        var $target = $($(this).attr('href')),
                $item = $(this);

        if (!$item.hasClass('disabled')) {
            navListItems.removeClass('btn-primary').addClass('btn-default');
            $item.addClass('btn-primary');
            allWells.hide();
            $target.show();
            $target.find('input:eq(0)').focus();
        }
    });

    allNextBtn.click(function () {
        var curStep = $(this).closest(".setup-content"),
            curStepBtn = curStep.attr("id"),
            nextStepWizard = $('div.setup-panel div a[href="#' + curStepBtn + '"]').parent().next().children("a"),
            curInputs = curStep.find("input[type='text'],input[type='url']"),
            isValid = true;

        $(".form-group").removeClass("has-error");
        for (var i = 0; i < curInputs.length; i++) {
            if (!curInputs[i].validity.valid) {
                isValid = false;
                $(curInputs[i]).closest(".form-group").addClass("has-error");
            }
        }

        if (isValid)
            nextStepWizard.removeAttr('disabled').trigger('click');
    });

    $('div.setup-panel div a.btn-primary').trigger('click');
});


$("#ProximoPgamento").click(function () {
    if (!$("#dinheiro").is(":checked") & !$("#cheque").is(":checked") & !$("#debito").is(":checked") & !$("#credito").is(":checked") & !$("#outros").is(":checked")) {
        $('#ModalErroHeaderGenerico').remove();
        $('#ModalErroBodyGenerico').remove();
        $('#ModalErroFooterGenerico').remove();
        $('#ModalErroBody').append('<div class="modal-header" id="ModalErroHeaderGenerico"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h3 class="modal-title" id="myModalLabel">Atenção!</h3></div><div class="modal-body" id="ModalErroBodyGenerico"><div class="alert alert-danger" role="alert">Você deve selecionar ao menos uma forma de pagamento aceita por você!</div></div><div class="modal-footer" id="ModalErroFooterGenerico"><button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button></div>');
        $('#ModalErro').modal('show');
    }
    else
    {
        if ($("#dinheiro").is(":checked"))
        {
            $("#dinheiro").val(true);
        }

        if ($("#cheque").is(":checked")) {
            $("#cheque").val(true);
        }

        if ($("#debito").is(":checked")) {
            $("#debito").val(true);
        }

        if ($("#credito").is(":checked")) {
            $("#credito").val(true);
        }

        if ($("#outros").is(":checked")) {
            $("#outros").val(true);
        }
    }
});

$("#EditarPagamento").click(function () {
    if (!$("#dinheiro").is(":checked") & !$("#cheque").is(":checked") & !$("#debito").is(":checked") & !$("#credito").is(":checked") & !$("#outros").is(":checked")) {
        $('#ModalErroHeaderGenerico').remove();
        $('#ModalErroBodyGenerico').remove();
        $('#ModalErroFooterGenerico').remove();
        $('#ModalErroBody').append('<div class="modal-header" id="ModalErroHeaderGenerico"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h3 class="modal-title" id="myModalLabel">Atenção!</h3></div><div class="modal-body" id="ModalErroBodyGenerico"><div class="alert alert-danger" role="alert">Você deve selecionar ao menos uma forma de pagamento aceita por você!</div></div><div class="modal-footer" id="ModalErroFooterGenerico"><button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button></div>');
        $('#ModalErro').modal('show');
    }
    else {
        if ($("#dinheiro").is(":checked")) {
            $("#dinheiro").val(true);
        }

        if ($("#cheque").is(":checked")) {
            $("#cheque").val(true);
        }

        if ($("#debito").is(":checked")) {
            $("#debito").val(true);
        }

        if ($("#credito").is(":checked")) {
            $("#credito").val(true);
        }

        if ($("#outros").is(":checked")) {
            $("#outros").val(true);
        }
    }
});

$("#ProximolAtividade").click(function () {
    if ($("#NM_NOME_ATIVIDADE1").val() == "" || $("#DS_DESCRICAO_ATIVIDADE").val() == "") {
        $('#ModalErroHeaderGenerico').remove();
        $('#ModalErroBodyGenerico').remove();
        $('#ModalErroFooterGenerico').remove();
        $('#ModalErroBody').append('<div class="modal-header" id="ModalErroHeaderGenerico"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h3 class="modal-title" id="myModalLabel">Atenção!</h3></div><div class="modal-body" id="ModalErroBodyGenerico"><div class="alert alert-danger" role="alert">Você deve cadastrar uma atividade!</div></div><div class="modal-footer" id="ModalErroFooterGenerico"><button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button></div>');
        $('#ModalErro').modal('show');
        return false;
    }
});

$("#cadastrogeral").click(function () {
    
    if (!$("#ACEITE_CONTRATO").is(":checked"))
    {
        $('#ModalErroHeaderGenerico').remove();
        $('#ModalErroBodyGenerico').remove();
        $('#ModalErroFooterGenerico').remove();
        $('#ModalErroBody').append('<div class="modal-header" id="ModalErroHeaderGenerico"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h3 class="modal-title" id="myModalLabel">Atenção!</h3></div><div class="modal-body" id="ModalErroBodyGenerico"><div class="alert alert-danger" role="alert">Para concluir o cadastro você precisa aceitar nossos termos de uso!</div></div><div class="modal-footer" id="ModalErroFooterGenerico"><button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button></div>');
        $('#ModalErro').modal('show');
        //alert("Você precisa estar de acordo com nossos termos de uso para utilizar nossos serviços!");
        return false;
    }

    if (!$("#todo_dia").is(":checked") & !$("#segundasexta").is(":checked") & !$("#segunda").is(":checked") & !$("#terca").is(":checked") & !$("#quarta").is(":checked") & !$("#quinta").is(":checked") & !$("#sexta").is(":checked") & !$("#sabado").is(":checked") & !$("#domingo").is(":checked")) {
        $('#ModalErroHeaderGenerico').remove();
        $('#ModalErroBodyGenerico').remove();
        $('#ModalErroFooterGenerico').remove();
        $('#ModalErroBody').append('<div class="modal-header" id="ModalErroHeaderGenerico"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h3 class="modal-title" id="myModalLabel">Atenção!</h3></div><div class="modal-body" id="ModalErroBodyGenerico"><div class="alert alert-danger" role="alert">Você deve selecionar ao menos um horário de atendimento!</div></div><div class="modal-footer" id="ModalErroFooterGenerico"><button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button></div>');
        $('#ModalErro').modal('show');
        return false;
    }
    else if (!$("#dinheiro").is(":checked") & !$("#cheque").is(":checked") & !$("#debito").is(":checked") & !$("#credito").is(":checked") & !$("#outros").is(":checked")) {
        $('#ModalErroHeaderGenerico').remove();
        $('#ModalErroBodyGenerico').remove();
        $('#ModalErroFooterGenerico').remove();
        $('#ModalErroBody').append('<div class="modal-header" id="ModalErroHeaderGenerico"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h3 class="modal-title" id="myModalLabel">Atenção!</h3></div><div class="modal-body" id="ModalErroBodyGenerico"><div class="alert alert-danger" role="alert">Você deve selecionar ao menos uma forma de pagamento aceita por você!</div></div><div class="modal-footer" id="ModalErroFooterGenerico"><button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button></div>');
        $('#ModalErro').modal('show');
        return false;
    }
    else if ($("#NM_NOME_ATIVIDADE").val() == "" || $("#DS_DESCRICAO_ATIVIDADE").val() == "")
    {
        $('#ModalErroHeaderGenerico').remove();
        $('#ModalErroBodyGenerico').remove();
        $('#ModalErroFooterGenerico').remove();
        $('#ModalErroBody').append('<div class="modal-header" id="ModalErroHeaderGenerico"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h3 class="modal-title" id="myModalLabel">Atenção!</h3></div><div class="modal-body" id="ModalErroBodyGenerico"><div class="alert alert-danger" role="alert">Você deve cadastrar uma atividade!</div></div><div class="modal-footer" id="ModalErroFooterGenerico"><button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button></div>');
        $('#ModalErro').modal('show');
        return false;
    }
});

$("#cadastrogeralForn").click(function () {
    if (!$("#ACEITE_CONTRATO").is(":checked"))
    {
        $('#ModalErroHeaderGenerico').remove();
        $('#ModalErroBodyGenerico').remove();
        $('#ModalErroFooterGenerico').remove();
        $('#ModalErroBody').append('<div class="modal-header" id="ModalErroHeaderGenerico"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h3 class="modal-title" id="myModalLabel">Atenção!</h3></div><div class="modal-body" id="ModalErroBodyGenerico"><div class="alert alert-danger" role="alert">Você precisa estar de acordo com nossos termos de uso para utilizar nossos serviços!</div></div><div class="modal-footer" id="ModalErroFooterGenerico"><button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button></div>');
        $('#ModalErro').modal('show');
        return false;
    }
});


$("#ProximoHora").click(function () {

    if (!$("#todo_dia").is(":checked") & !$("#segundasexta").is(":checked") & !$("#segunda").is(":checked") & !$("#terca").is(":checked") & !$("#quarta").is(":checked") & !$("#quinta").is(":checked") & !$("#sexta").is(":checked") & !$("#sabado").is(":checked") & !$("#domingo").is(":checked")) {
        $('#ModalErroHeaderGenerico').remove();
        $('#ModalErroBodyGenerico').remove();
        $('#ModalErroFooterGenerico').remove();
        $('#ModalErroBody').append('<div class="modal-header" id="ModalErroHeaderGenerico"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h3 class="modal-title" id="myModalLabel">Atenção!</h3></div><div class="modal-body" id="ModalErroBodyGenerico"><div class="alert alert-danger" role="alert">Você deve selecionar ao menos um horário de atendimento!</div></div><div class="modal-footer" id="ModalErroFooterGenerico"><button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button></div>');
        $('#ModalErro').modal('show');
        return false;
    }
    else if ((parseInt($("#segunda_fim").val()) < parseInt($("#segunda_inicio").val())) || (parseInt($("#terca_fim").val()) < parseInt($("#terca_inicio").val())) || (parseInt($("#quarta_fim").val()) < parseInt($("#quarta_inicio").val())) || (parseInt($("#quinta_fim").val()) < parseInt($("#quinta_inicio").val())) || (parseInt($("#sexta_fim").val()) < parseInt($("#sexta_inicio").val())) || (parseInt($("#sabado_fim").val()) < parseInt($("#sabado_inicio").val())) || (parseInt($("#domingo_fim").val()) < parseInt($("#domingo_inicio").val())) || (parseInt($(".segundasextafim").val()) < parseInt($(".segundasextainicio").val()))) {
        $('#ModalErroHeaderGenerico').remove();
        $('#ModalErroBodyGenerico').remove();
        $('#ModalErroFooterGenerico').remove();
        $('#ModalErroBody').append('<div class="modal-header" id="ModalErroHeaderGenerico"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h3 class="modal-title" id="myModalLabel">Atenção!</h3></div><div class="modal-body" id="ModalErroBodyGenerico"><div class="alert alert-danger" role="alert">A hora de início não pode ser maior ou igual a hora de fim!</div></div><div class="modal-footer" id="ModalErroFooterGenerico"><button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button></div>');
        $('#ModalErro').modal('show');
        return false;
    }
    else if ($("#segunda_fim").val() == "" & $("#segunda_inicio").val() != "" || $("#terca_fim").val() == "" & $("#terca_inicio").val() != "" || $("#quarta_fim").val() == "" & $("#quarta_inicio").val() != "" || $("#quinta_fim").val() == "" & $("#quinta_inicio").val() != "" || $("#sexta_fim").val() == "" & $("#sexta_inicio").val() != "" || $("#sabado_fim").val() == "" & $("#sabado_inicio").val() != "" || $("#domingo_fim").val() == "" & $("#domingo_inicio").val() != "" || $(".segundasextafim").val() == "" & $(".segundasextainicio").val() != "") {
        $('#ModalErroHeaderGenerico').remove();
        $('#ModalErroBodyGenerico').remove();
        $('#ModalErroFooterGenerico').remove();
        $('#ModalErroBody').append('<div class="modal-header" id="ModalErroHeaderGenerico"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h3 class="modal-title" id="myModalLabel">Atenção!</h3></div><div class="modal-body" id="ModalErroBodyGenerico"><div class="alert alert-danger" role="alert">O campo de fim também deve ser preenchido!</div></div><div class="modal-footer" id="ModalErroFooterGenerico"><button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button></div>');
        $('#ModalErro').modal('show');
        return false;
    }
    else if ($("#segunda_inicio").val() == "" & $("#segunda_fim").val() != "" || $("#terca_inicio").val() == "" & $("#terca_fim").val() != "" || $("#quarta_inicio").val() == "" & $("#quarta_fim").val() != "" || $("#quinta_inicio").val() == "" & $("#quinta_fim").val() != "" || $("#sexta_inicio").val() == "" & $("#sexta_fim").val() != "" || $("#sabado_inicio").val() == "" & $("#sabado_fim").val() != "" || $("#domingo_inicio").val() == "" & $("#domingo_fim").val() != "" || $(".segundasextainicio").val() == "" & $(".segundasextafim").val() != "") {
        $('#ModalErroHeaderGenerico').remove();
        $('#ModalErroBodyGenerico').remove();
        $('#ModalErroFooterGenerico').remove();
        $('#ModalErroBody').append('<div class="modal-header" id="ModalErroHeaderGenerico"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h3 class="modal-title" id="myModalLabel">Atenção!</h3></div><div class="modal-body" id="ModalErroBodyGenerico"><div class="alert alert-danger" role="alert">O campo de inicio também deve ser preenchido!</div></div><div class="modal-footer" id="ModalErroFooterGenerico"><button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button></div>');
        $('#ModalErro').modal('show');
        return false;
    }
    else if ($("#segundasexta").is(":checked")) {
        $("#todo_dia").val(false);
        $("#segunda_inicio").prop("disabled", false);
        $("#segunda_fim").prop("disabled", false);
        $("#terca_inicio").prop("disabled", false);
        $("#terca_fim").prop("disabled", false);
        $("#quarta_inicio").prop("disabled", false);
        $("#quarta_fim").prop("disabled", false);
        $("#quinta_inicio").prop("disabled", false);
        $("#quinta_fim").prop("disabled", false);
        $("#sexta_inicio").prop("disabled", false);
        $("#sexta_fim").prop("disabled", false);
        $("#sabado_inicio").prop("disabled", false);
        $("#sabado_fim").prop("disabled", false);
        $("#domingo_inicio").prop("disabled", false);
        $("#domingo_fim").prop("disabled", false);

        $("#segunda_inicio").val($(".segundasextainicio").val());
        $("#segunda_fim").val($(".segundasextafim").val());

        $("#terca_inicio").val($(".segundasextainicio").val());
        $("#terca_fim").val($(".segundasextafim").val());

        $("#quarta_inicio").val($(".segundasextainicio").val());
        $("#quarta_fim").val($(".segundasextafim").val());

        $("#quinta_inicio").val($(".segundasextainicio").val());
        $("#quinta_fim").val($(".segundasextafim").val());

        $("#sexta_inicio").val($(".segundasextainicio").val());
        $("#sexta_fim").val($(".segundasextafim").val());
    }
    else if ($("#todo_dia").is(":checked")) {

        $("#segunda_inicio").prop("disabled", false);
        $("#segunda_fim").prop("disabled", false);
        $("#terca_inicio").prop("disabled", false);
        $("#terca_fim").prop("disabled", false);
        $("#quarta_inicio").prop("disabled", false);
        $("#quarta_fim").prop("disabled", false);
        $("#quinta_inicio").prop("disabled", false);
        $("#quinta_fim").prop("disabled", false);
        $("#sexta_inicio").prop("disabled", false);
        $("#sexta_fim").prop("disabled", false);
        $("#sabado_inicio").prop("disabled", false);
        $("#sabado_fim").prop("disabled", false);
        $("#domingo_inicio").prop("disabled", false);
        $("#domingo_fim").prop("disabled", false);


        $(".segundasextainicio").val("");
        $(".segundasextafim").val("");

        $("#segunda_inicio").val(0);
        $("#segunda_fim").val(23);

        $("#terca_inicio").val(0);
        $("#terca_fim").val(23);

        $("#quarta_inicio").val(0);
        $("#quarta_fim").val(23);

        $("#quinta_inicio").val(0);
        $("#quinta_fim").val(23);

        $("#sexta_inicio").val(0);
        $("#sexta_fim").val(23);

        $("#sabado_inicio").val(0);
        $("#sabado_fim").val(23);

        $("#domingo_inicio").val(0);
        $("#domingo_fim").val(23);
    }
    if ($("#domingo_inicio").val() == "" || $("#domingo_fim").val() == "") {
        $("#domingo_inicio").val(0);
        $("#domingo_fim").val(0);
    }
    if ($("#sabado_inicio").val() == "" || $("#sabado_fim").val() == "") {
        $("#sabado_inicio").val(0);
        $("#sabado_fim").val(0);
    }
});

$("#EditarHorario").click(function () {

    if (!$("#todo_dia").is(":checked") & !$("#segundasexta").is(":checked") & !$("#segunda").is(":checked") & !$("#terca").is(":checked") & !$("#quarta").is(":checked") & !$("#quinta").is(":checked") & !$("#sexta").is(":checked") & !$("#sabado").is(":checked") & !$("#domingo").is(":checked")) {
        $('#ModalErroHeaderGenerico').remove();
        $('#ModalErroBodyGenerico').remove();
        $('#ModalErroFooterGenerico').remove();
        $('#ModalErroBody').append('<div class="modal-header" id="ModalErroHeaderGenerico"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h3 class="modal-title" id="myModalLabel">Atenção!</h3></div><div class="modal-body" id="ModalErroBodyGenerico"><div class="alert alert-danger" role="alert">Você deve selecionar ao menos um horário de atendimento!</div></div><div class="modal-footer" id="ModalErroFooterGenerico"><button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button></div>');
        $('#ModalErro').modal('show');
        return false;
    }
    else if ((parseInt($("#segunda_fim").val()) < parseInt($("#segunda_inicio").val())) || (parseInt($("#terca_fim").val()) < parseInt($("#terca_inicio").val())) || (parseInt($("#quarta_fim").val()) < parseInt($("#quarta_inicio").val())) || (parseInt($("#quinta_fim").val()) < parseInt($("#quinta_inicio").val())) || (parseInt($("#sexta_fim").val()) < parseInt($("#sexta_inicio").val())) || (parseInt($("#sabado_fim").val()) < parseInt($("#sabado_inicio").val())) || (parseInt($("#domingo_fim").val()) < parseInt($("#domingo_inicio").val())) || (parseInt($(".segundasextafim").val()) < parseInt($(".segundasextainicio").val()))) {
        $('#ModalErroHeaderGenerico').remove();
        $('#ModalErroBodyGenerico').remove();
        $('#ModalErroFooterGenerico').remove();
        $('#ModalErroBody').append('<div class="modal-header" id="ModalErroHeaderGenerico"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h3 class="modal-title" id="myModalLabel">Atenção!</h3></div><div class="modal-body" id="ModalErroBodyGenerico"><div class="alert alert-danger" role="alert">A hora de início não pode ser maior ou igual a hora de fim!</div></div><div class="modal-footer" id="ModalErroFooterGenerico"><button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button></div>');
        $('#ModalErro').modal('show');
        return false;
    }
    else if ($("#segunda_fim").val() == "" & $("#segunda_inicio").val() != "" || $("#terca_fim").val() == "" & $("#terca_inicio").val() != "" || $("#quarta_fim").val() == "" & $("#quarta_inicio").val() != "" || $("#quinta_fim").val() == "" & $("#quinta_inicio").val() != "" || $("#sexta_fim").val() == "" & $("#sexta_inicio").val() != "" || $("#sabado_fim").val() == "" & $("#sabado_inicio").val() != "" || $("#domingo_fim").val() == "" & $("#domingo_inicio").val() != "" || $(".segundasextafim").val() == "" & $(".segundasextainicio").val() != "") {
        $('#ModalErroHeaderGenerico').remove();
        $('#ModalErroBodyGenerico').remove();
        $('#ModalErroFooterGenerico').remove();
        $('#ModalErroBody').append('<div class="modal-header" id="ModalErroHeaderGenerico"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h3 class="modal-title" id="myModalLabel">Atenção!</h3></div><div class="modal-body" id="ModalErroBodyGenerico"><div class="alert alert-danger" role="alert">O campo de fim também deve ser preenchido!</div></div><div class="modal-footer" id="ModalErroFooterGenerico"><button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button></div>');
        $('#ModalErro').modal('show');
        return false;
    }
    else if ($("#segunda_inicio").val() == "" & $("#segunda_fim").val() != "" || $("#terca_inicio").val() == "" & $("#terca_fim").val() != "" || $("#quarta_inicio").val() == "" & $("#quarta_fim").val() != "" || $("#quinta_inicio").val() == "" & $("#quinta_fim").val() != "" || $("#sexta_inicio").val() == "" & $("#sexta_fim").val() != "" || $("#sabado_inicio").val() == "" & $("#sabado_fim").val() != "" || $("#domingo_inicio").val() == "" & $("#domingo_fim").val() != "" || $(".segundasextainicio").val() == "" & $(".segundasextafim").val() != "") {
        $('#ModalErroHeaderGenerico').remove();
        $('#ModalErroBodyGenerico').remove();
        $('#ModalErroFooterGenerico').remove();
        $('#ModalErroBody').append('<div class="modal-header" id="ModalErroHeaderGenerico"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h3 class="modal-title" id="myModalLabel">Atenção!</h3></div><div class="modal-body" id="ModalErroBodyGenerico"><div class="alert alert-danger" role="alert">O campo de inicio também deve ser preenchido!</div></div><div class="modal-footer" id="ModalErroFooterGenerico"><button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button></div>');
        $('#ModalErro').modal('show');
        return false;
    }
    else if ($("#segundasexta").is(":checked")) {
        $("#todo_dia").val(false);
        $("#segunda_inicio").prop("disabled", false);
        $("#segunda_fim").prop("disabled", false);
        $("#terca_inicio").prop("disabled", false);
        $("#terca_fim").prop("disabled", false);
        $("#quarta_inicio").prop("disabled", false);
        $("#quarta_fim").prop("disabled", false);
        $("#quinta_inicio").prop("disabled", false);
        $("#quinta_fim").prop("disabled", false);
        $("#sexta_inicio").prop("disabled", false);
        $("#sexta_fim").prop("disabled", false);
        $("#sabado_inicio").prop("disabled", false);
        $("#sabado_fim").prop("disabled", false);
        $("#domingo_inicio").prop("disabled", false);
        $("#domingo_fim").prop("disabled", false);

        $("#segunda_inicio").val($(".segundasextainicio").val());
        $("#segunda_fim").val($(".segundasextafim").val());

        $("#terca_inicio").val($(".segundasextainicio").val());
        $("#terca_fim").val($(".segundasextafim").val());

        $("#quarta_inicio").val($(".segundasextainicio").val());
        $("#quarta_fim").val($(".segundasextafim").val());

        $("#quinta_inicio").val($(".segundasextainicio").val());
        $("#quinta_fim").val($(".segundasextafim").val());

        $("#sexta_inicio").val($(".segundasextainicio").val());
        $("#sexta_fim").val($(".segundasextafim").val());
    }
    else if ($("#todo_dia").is(":checked")) {

        $("#segunda_inicio").prop("disabled", false);
        $("#segunda_fim").prop("disabled", false);
        $("#terca_inicio").prop("disabled", false);
        $("#terca_fim").prop("disabled", false);
        $("#quarta_inicio").prop("disabled", false);
        $("#quarta_fim").prop("disabled", false);
        $("#quinta_inicio").prop("disabled", false);
        $("#quinta_fim").prop("disabled", false);
        $("#sexta_inicio").prop("disabled", false);
        $("#sexta_fim").prop("disabled", false);
        $("#sabado_inicio").prop("disabled", false);
        $("#sabado_fim").prop("disabled", false);
        $("#domingo_inicio").prop("disabled", false);
        $("#domingo_fim").prop("disabled", false);


        $(".segundasextainicio").val("");
        $(".segundasextafim").val("");

        $("#segunda_inicio").val(0);
        $("#segunda_fim").val(23);

        $("#terca_inicio").val(0);
        $("#terca_fim").val(23);

        $("#quarta_inicio").val(0);
        $("#quarta_fim").val(23);

        $("#quinta_inicio").val(0);
        $("#quinta_fim").val(23);

        $("#sexta_inicio").val(0);
        $("#sexta_fim").val(23);

        $("#sabado_inicio").val(0);
        $("#sabado_fim").val(23);

        $("#domingo_inicio").val(0);
        $("#domingo_fim").val(23);

    }

    if($("#domingo_inicio").val() == "" || $("#domingo_fim").val() == "")
    {
        $("#domingo_inicio").val(0);
        $("#domingo_fim").val(0);
    }
    if ($("#sabado_inicio").val() == "" || $("#sabado_fim").val() == "")
    {
        $("#sabado_inicio").val(0);
        $("#sabado_fim").val(0);
    }

});

$("#todo_dia").change(function () {
    if ($("#todo_dia").is(":checked")) {
        $(".segundasexta").hide();
        $(".segunda_check").hide();
        $(".terca_check").hide();
        $(".quarta_check").hide();
        $(".quinta_check").hide();
        $(".sexta_check").hide();
        $(".sabado_check").hide();
        $(".domingo_check").hide();
        $("#todo_dia").val(true);
        $("#segundasexta").prop('checked', false);
    }
    else {
        $("#todo_dia").val(false);

        $("#segunda_inicio").prop("disabled", true);
        $("#segunda_fim").prop("disabled", true);
        $("#terca_inicio").prop("disabled", true);
        $("#terca_fim").prop("disabled", true);
        $("#quarta_inicio").prop("disabled", true);
        $("#quarta_fim").prop("disabled", true);
        $("#quinta_inicio").prop("disabled", true);
        $("#quinta_fim").prop("disabled", true);
        $("#sexta_inicio").prop("disabled", true);
        $("#sexta_fim").prop("disabled", true);
        $("#sabado_inicio").prop("disabled", true);
        $("#sabado_fim").prop("disabled", true);
        $("#domingo_inicio").prop("disabled", true);
        $("#domingo_fim").prop("disabled", true);

        $(".segundasexta").show();
        $(".segunda_check").show();
        $(".terca_check").show();
        $(".quarta_check").show();
        $(".quinta_check").show();
        $(".sexta_check").show();
        $(".sabado_check").show();
        $(".domingo_check").show();

    }

});


$("#segundasexta").change(function () {
    if ($("#segundasexta").is(":checked"))
    {
        $("#todo_dia").val(false);
        $("#todo_dia").prop("disabled", true);
        $(".segunda_check").hide();
        $(".terca_check").hide();
        $(".quarta_check").hide();
        $(".quinta_check").hide();
        $(".sexta_check").hide();
        $(".segundasextainicio").prop("disabled", false);
        $(".segundasextafim").prop("disabled", false);
        $("#segundasexta").prop('checked', true);
    }
    else
    {
        $("#todo_dia").prop("disabled", false);
        $(".segundasextainicio").prop("disabled", true);
        $(".segundasextafim").prop("disabled", true);
        $("#segundasexta").prop('checked', false);
        $("#segunda_inicio").prop("disabled", true);
        $("#segunda_fim").prop("disabled", true);
        $("#terca_inicio").prop("disabled", true);
        $("#terca_fim").prop("disabled", true);
        $("#quarta_inicio").prop("disabled", true);
        $("#quarta_fim").prop("disabled", true);
        $("#quinta_inicio").prop("disabled", true);
        $("#quinta_fim").prop("disabled", true);
        $("#sexta_inicio").prop("disabled", true);
        $("#sexta_fim").prop("disabled", true);
        $("#sabado_inicio").prop("disabled", true);
        $("#sabado_fim").prop("disabled", true);
        $("#domingo_inicio").prop("disabled", true);
        $("#domingo_fim").prop("disabled", true);
        $(".segundasexta").show();
        $(".segunda_check").show();
        $(".terca_check").show();
        $(".quarta_check").show();
        $(".quinta_check").show();
        $(".sexta_check").show();
    }
});

$("#segunda").change(function () {
    if($("#segunda").is(":checked")) {
        $("#segunda_inicio").prop("disabled", false);
        $("#segunda_fim").prop("disabled", false);
        $("#segunda_inicio").show();
        $("#segunda_fim").show();
    }
    else {
        $("#segunda_inicio").prop("disabled", true);
        $("#segunda_fim").prop("disabled", true);
        $("#segunda_inicio").hide();
        $("#segunda_fim").hide();
        $("#segunda_inicio").val('');
        $("#segunda_fim").val('');
    }
});


$("#terca").change(function () {
    if ($("#terca").is(":checked")) {
        $("#terca_inicio").prop("disabled", false);
        $("#terca_fim").prop("disabled", false);
        $("#terca_inicio").show();
        $("#terca_fim").show();
    }
    else {
        $("#terca_inicio").prop("disabled", true);
        $("#terca_fim").prop("disabled", true);
        $("#terca_inicio").hide();
        $("#terca_fim").hide();
        $("#terca_inicio").val('');
        $("#terca_fim").val('');
    }
});

$("#quarta").change(function () {
    if ($("#quarta").is(":checked")) {
        $("#quarta_inicio").prop("disabled", false);
        $("#quarta_fim").prop("disabled", false);
        $("#quarta_inicio").show();
        $("#quarta_fim").show();
    }
    else {
        $("#quarta_inicio").prop("disabled", true);
        $("#quarta_fim").prop("disabled", true);
        $("#quarta_inicio").hide();
        $("#quarta_fim").hide();
        $("#quarta_inicio").val('');
        $("#quarta_fim").val('');
    }
});


$("#quinta").change(function () {
    if ($("#quinta").is(":checked")) {
        $("#quinta_inicio").prop("disabled", false);
        $("#quinta_fim").prop("disabled", false);
        $("#quinta_inicio").show();
        $("#quinta_fim").show();
    }
    else {
        $("#quinta_inicio").prop("disabled", true);
        $("#quinta_fim").prop("disabled", true);
        $("#quinta_inicio").hide();
        $("#quinta_fim").hide();
        $("#quinta_inicio").val('');
        $("#quinta_fim").val('');
    }
});

$("#sexta").change(function () {
    if ($("#sexta").is(":checked")) {
        $("#sexta_inicio").prop("disabled", false);
        $("#sexta_fim").prop("disabled", false);
        $("#sexta_inicio").show();
        $("#sexta_fim").show();
    }
    else {
        $("#sexta_inicio").prop("disabled", true);
        $("#sexta_fim").prop("disabled", true);
        $("#sexta_inicio").hide();
        $("#sexta_fim").hide();
        $("#sexta_inicio").val('');
        $("#sexta_fim").val('');
    }
});

$("#sabado").change(function () {
    if ($("#sabado").is(":checked")) {
        $("#sabado_inicio").prop("disabled", false);
        $("#sabado_fim").prop("disabled", false);
        $("#sabado_inicio").show();
        $("#sabado_fim").show();
    }
    else {
        $("#sabado_inicio").prop("disabled", true);
        $("#sabado_fim").prop("disabled", true);
        $("#sabado_inicio").hide();
        $("#sabado_fim").hide();
        $("#sabado_inicio").val('');
        $("#sabado_fim").val('');
    }
});

$("#domingo").change(function () {
    if ($("#domingo").is(":checked")) {
        $("#domingo_inicio").prop("disabled", false);
        $("#domingo_fim").prop("disabled", false);
        $("#domingo_inicio").show();
        $("#domingo_fim").show();
    }
    else {
        $("#domingo_inicio").prop("disabled", true);
        $("#domingo_fim").prop("disabled", true);
        $("#domingo_inicio").hide();
        $("#domingo_fim").hide();
        $("#domingo_inicio").val('');
        $("#domingo_fim").val('');
    }
});

$("#segunda_inicio").val('');
$("#segunda_fim").val('');
$("#segunda_inicio").hide();
$("#segunda_fim").hide();

$("#terca_inicio").val('');
$("#terca_fim").val('');
$("#terca_inicio").hide();
$("#terca_fim").hide();

$("#quarta_inicio").val('');
$("#quarta_fim").val('');
$("#quarta_inicio").hide();
$("#quarta_fim").hide();

$("#quinta_inicio").val('');
$("#quinta_fim").val('');
$("#quinta_inicio").hide();
$("#quinta_fim").hide();


$("#sexta_inicio").val('');
$("#sexta_fim").val('');
$("#sexta_inicio").hide();
$("#sexta_fim").hide();

$("#sabado_inicio").val('');
$("#sabado_fim").val('');
$("#sabado_inicio").hide();
$("#sabado_fim").hide();

$("#domingo_inicio").val('');
$("#domingo_fim").val('');
$("#domingo_inicio").hide();
$("#domingo_fim").hide();


$("#TF_TEL_CEL").mask("(00) 90000-0000");
$("#TF_TEL_FIXO").mask("(00) 0000-0000");
$("#CD_CEP").mask("00000-000");


$("#ACEITE_CONTRATO").change(function () {
    if ($("#ACEITE_CONTRATO").is(":checked")) {
        $("#ACEITE_CONTRATO").attr("checked", true);
    }
    else {
        $("#ACEITE_CONTRATO").attr("checked", false);
    }
});

$("#fechaModalContrato").click(function () {
    if (!$("#ACEITE_CONTRATO").is(":checked"))
    {
        $('#ModalErroHeaderGenerico').remove();
        $('#ModalErroBodyGenerico').remove();
        $('#ModalErroFooterGenerico').remove();
        $('#ModalErroBody').append('<div class="modal-header" id="ModalErroHeaderGenerico"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h3 class="modal-title" id="myModalLabel">Atenção!</h3></div><div class="modal-body" id="ModalErroBodyGenerico"><div class="alert alert-danger" role="alert">Você não aceitou nossos termos de uso!</div></div><div class="modal-footer" id="ModalErroFooterGenerico"><button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button></div>');
        $('#ModalErro').modal('show');
        return false;
    }
    else
    {
        $("#closeModalContrato").trigger("click");
        $("#closeModalContrato").trigger("click");
    }
});

//$("#DS_APELIDO_SITE").attr("placeholder", "SEUSITE").blur();
//document.getElementById("latitude").disabled = true;
//document.getElementById("longitude").disabled = true;

//$("#DS_APELIDO_SITE").focus(function () {
//    $("#nomesite").show("fold", 1000);
//    //$("#DS_APELIDO_SITE").tooltip();
//});

//$("#CD_LONG").focus(function () {
//    $("#nomelong").show("fold", 1000);
//    $("#nomelong").html("NÃO ALTERE ESTE VALOR!");
//});

//$("#CD_LAT").focus(function () {
//    $("#nomelat").show("fold", 1000);
//    $("#nomelat").html("NÃO ALTERE ESTE VALOR!");
//    //$("#DS_APELIDO_SITE").tooltip();
//});

//$("#DS_APELIDO_SITE").keyup(function () {

//    $("#nomesite").html("www.mercadodeservicos.com.br/" + $(this).val());
//})



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
        $('#ModalErroHeaderGenerico').remove();
        $('#ModalErroBodyGenerico').remove();
        $('#ModalErroFooterGenerico').remove();
        $('#ModalErroBody').append('<div class="modal-header" id="ModalErroHeaderGenerico"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h3 class="modal-title" id="myModalLabel">Atenção!</h3></div><div class="modal-body" id="ModalErroBodyGenerico"><div class="alert alert-danger" role="alert">Por favor, digite um CNPJ válido!</div></div><div class="modal-footer" id="ModalErroFooterGenerico"><button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button></div>');
        $('#ModalErro').modal('show');
        aux1 = eval("document." + f + "." + campo + ".focus");
        aux2 = eval("document." + f + "." + campo + ".value = ''");
        return false;

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
        $('#ModalErroHeaderGenerico').remove();
        $('#ModalErroBodyGenerico').remove();
        $('#ModalErroFooterGenerico').remove();
        $('#ModalErroBody').append('<div class="modal-header" id="ModalErroHeaderGenerico"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h3 class="modal-title" id="myModalLabel">Atenção!</h3></div><div class="modal-body" id="ModalErroBodyGenerico"><div class="alert alert-danger" role="alert">Por favor, digite um CPF válido!</div></div><div class="modal-footer" id="ModalErroFooterGenerico"><button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button></div>');
        $('#ModalErro').modal('show');
        aux1 = eval("document." + f + "." + campo + ".focus");
        aux2 = eval("document." + f + "." + campo + ".value = ''");
        return false;
    }
}

//Fim valida cpf ou cnpj