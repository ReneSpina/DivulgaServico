

$("#fechaModalPagamento").click(function () {
    if (!$("#dinheiro").is(":checked") & !$("#cheque").is(":checked") & !$("#debito").is(":checked") & !$("#credito").is(":checked") & !$("#outros").is(":checked")) {
        alert("Você deve selecionar ao menos uma forma de pagamento aceita por você!");
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

        $("#closePagamento").trigger("click");
        $("#closePagamento").trigger("click");
    }
});

$("#fechaModalAtividade").click(function () {
    if ($("#NM_NOME_ATIVIDADE1").val() == "" || $("#DS_DESCRICAO_ATIVIDADE").val() == "") {
        alert("Você deve cadastrar uma atividade");
    }
    else
    {
        $("#closeAtividade").trigger("click");
        $("#closeAtividade").trigger("click");
    }
});

$("#cadastrogeral").click(function () {
    if (!$("#todo_dia").is(":checked") & !$("#segundasexta").is(":checked") & !$("#segunda").is(":checked") & !$("#terca").is(":checked") & !$("#quarta").is(":checked") & !$("#quinta").is(":checked") & !$("#sexta").is(":checked") & !$("#sabado").is(":checked") & !$("#domingo").is(":checked")) {
        alert("Você deve selecionar ao menos um horário de atendimento!");
        return false;
    }
    else if (!$("#dinheiro").is(":checked") & !$("#cheque").is(":checked") & !$("#debito").is(":checked") & !$("#credito").is(":checked") & !$("#outros").is(":checked")) {
        alert("Você deve selecionar ao menos uma forma de pagamento aceita por você!");
        return false;
    }
    else if ($("#NM_NOME_ATIVIDADE").val() == "" || $("#DS_DESCRICAO_ATIVIDADE").val() == "")
    {
        alert("Você deve cadastrar uma atividade");
        return false;
    }
});


$("#fechaModal").click(function () {

    if (!$("#todo_dia").is(":checked") & !$("#segundasexta").is(":checked") & !$("#segunda").is(":checked") & !$("#terca").is(":checked") & !$("#quarta").is(":checked") & !$("#quinta").is(":checked") & !$("#sexta").is(":checked") & !$("#sabado").is(":checked") & !$("#domingo").is(":checked")) {
        alert("Você deve selecionar ao menos um horário de atendimento!");
    }
    else if ((parseInt($("#segunda_fim").val()) < parseInt($("#segunda_inicio").val())) || (parseInt($("#terca_fim").val()) < parseInt($("#terca_inicio").val())) || (parseInt($("#quarta_fim").val()) < parseInt($("#quarta_inicio").val())) || (parseInt($("#quinta_fim").val()) < parseInt($("#quinta_inicio").val())) || (parseInt($("#sexta_fim").val()) < parseInt($("#sexta_inicio").val())) || (parseInt($("#sabado_fim").val()) < parseInt($("#sabado_inicio").val())) || (parseInt($("#domingo_fim").val()) < parseInt($("#domingo_inicio").val())) || (parseInt($(".segundasextafim").val()) < parseInt($(".segundasextainicio").val()))) {
        alert("A hora de início não pode ser maior ou igual a hora de fim!");
    }
    else if ($("#segunda_fim").val() == "" & $("#segunda_inicio").val() != "" || $("#terca_fim").val() == "" & $("#terca_inicio").val() != "" || $("#quarta_fim").val() == "" & $("#quarta_inicio").val() != "" || $("#quinta_fim").val() == "" & $("#quinta_inicio").val() != "" || $("#sexta_fim").val() == "" & $("#sexta_inicio").val() != "" || $("#sabado_fim").val() == "" & $("#sabado_inicio").val() != "" || $("#domingo_fim").val() == "" & $("#domingo_inicio").val() != "" || $(".segundasextafim").val() == "" & $(".segundasextainicio").val() != "") {
        alert("O campo de fim também deve ser preenchido!");
    }
    else if ($("#segunda_inicio").val() == "" & $("#segunda_fim").val() != "" || $("#terca_inicio").val() == "" & $("#terca_fim").val() != "" || $("#quarta_inicio").val() == "" & $("#quarta_fim").val() != "" || $("#quinta_inicio").val() == "" & $("#quinta_fim").val() != "" || $("#sexta_inicio").val() == "" & $("#sexta_fim").val() != "" || $("#sabado_inicio").val() == "" & $("#sabado_fim").val() != "" || $("#domingo_inicio").val() == "" & $("#domingo_fim").val() != "" || $(".segundasextainicio").val() == "" & $(".segundasextafim").val() != "") {
        alert("O campo de inicio também deve ser preenchido!");
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
        
        $('.close').trigger('click');
        $('.close').trigger('click');
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

        $('.close').trigger('click');
        $('.close').trigger('click');

    } else{
        $('.close').trigger('click');
        $('.close').trigger('click');
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
        $("#segunda_inicio").val(1);
        $("#segunda_fim").val(1);
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
        $("#terca_inicio").val(1);
        $("#terca_fim").val(1);
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
        $("#quarta_inicio").val(1);
        $("#quarta_fim").val(1);
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
        $("#quinta_inicio").val(1);
        $("#quinta_fim").val(1);
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
        $("#sexta_inicio").val(1);
        $("#sexta_fim").val(1);
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
        $("#sabado_inicio").val(1);
        $("#sabado_fim").val(1);
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
        $("#domingo_inicio").val(1);
        $("#domingo_fim").val(1);
    }
});

$("#segunda_inicio").val(1);
$("#segunda_fim").val(1);
$("#segunda_inicio").hide();
$("#segunda_fim").hide();

$("#terca_inicio").val(1);
$("#terca_fim").val(1);
$("#terca_inicio").hide();
$("#terca_fim").hide();

$("#quarta_inicio").val(1);
$("#quarta_fim").val(1);
$("#quarta_inicio").hide();
$("#quarta_fim").hide();

$("#quinta_inicio").val(1);
$("#quinta_fim").val(1);
$("#quinta_inicio").hide();
$("#quinta_fim").hide();


$("#sexta_inicio").val(1);
$("#sexta_fim").val(1);
$("#sexta_inicio").hide();
$("#sexta_fim").hide();

$("#sabado_inicio").val(1);
$("#sabado_fim").val(1);
$("#sabado_inicio").hide();
$("#sabado_fim").hide();

$("#domingo_inicio").val(1);
$("#domingo_fim").val(1);
$("#domingo_inicio").hide();
$("#domingo_fim").hide();


$("#TF_TEL_CEL").mask("(00) 90000-0000");
$("#TF_TEL_FIXO").mask("(00) 0000-0000");
$("#CD_CEP").mask("00000-000");
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

$("#cadastrogeral").click(function () {
    

});