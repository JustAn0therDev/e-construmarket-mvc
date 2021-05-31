$(document).ready(function () {
    $.ajax({
        url: `${window.location.href}produto`,
        cache: true,
        async: true,
        success: function (result) {
            exibeOuEscondeMensagemDeErro();
            const retornoEmJSON = typeof result === "string";
            if (retornoEmJSON) {
                populaProdutoTable(JSON.parse(result));
            } else {
                populaProdutoTable(result);
            }
        },
        error: function (err) {
            exibeOuEscondeMensagemDeErro(err);
        }
    });

    $("#input-nome-produto").keyup(function () {

        function mostrarApenasMensagemDaTabela() {
            linhasDaTabela.each(function() { $(this).hide(); });
            linhasDaTabela.first().show();
        }
        
        const search = $(this).val();
        const linhasDaTabela = $("#table-produtos tbody tr");
        let contagemDeLinhasExibidas = 0;
        
        if (!search || search.length < 3) {
            mostrarApenasMensagemDaTabela();
            return;
        }

        linhasDaTabela.each(function () {
            const linhaAtual = $(this);
            const nomeDoProdutoEmCaixaAlta = linhaAtual.find("td:first").text().toUpperCase();
            const conteudoDaPesquisaEmCaixaAlta = search.toUpperCase();

            if (nomeDoProdutoEmCaixaAlta.includes(conteudoDaPesquisaEmCaixaAlta)) {
                linhaAtual.show();
                contagemDeLinhasExibidas++;
            } else {
                linhaAtual.hide();
            }
        });

        const nenhumResultadoFoiEncontrado = contagemDeLinhasExibidas === 0;

        if (nenhumResultadoFoiEncontrado) {
            mostrarApenasMensagemDaTabela();
        }
    });
});

function populaProdutoTable(produtosArray) {

    function elementoNaoEncontrado(el) {
        return !el || el.length === 0;
    }

    const table = $("#table-produtos");

    if (elementoNaoEncontrado(table)) {
        return;
    }

    const tableBody = table.find("tbody");

    if (elementoNaoEncontrado(tableBody)) {
        return;
    }

    produtosArray.forEach(produto => {
        const valorFormatadoEmReais = Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(produto.preco);
        tableBody.append(`<tr style="display:none;"><td>${produto.nome}</td><td>${valorFormatadoEmReais}</td><td>${produto.marca.nome}</td><td>${produto.marca.codigoMarca}</td></tr>`);
    });
}

function exibeOuEscondeMensagemDeErro(err) {
    if (err) {
        $("#div-mensagem-erro").show();
        $("#div-mensagem-erro").text(`Ocorreu um erro durante o resgate dos dados. Erro: ${err}`);
    } else {
        $("#div-mensagem-erro").hide();
        $("#div-messagem-erro").text("");
    }
}
