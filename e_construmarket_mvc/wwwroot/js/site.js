$(document).ready(function () {
    $.ajax({
        url: `${window.location.href}produto`,
        cache: true,
        async: true,
        success: function (result) {
            populaProdutoTable(JSON.parse(result));
        },
        error: function (err) {
            console.error(err);
        }
    });


    // TODO: a pesquisa deve encontrar um produto de cada marca e ordenar os produtos do menor ao maior preco
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
            
            if (linhaAtual.find("td:first").text().toUpperCase().includes(search.toUpperCase())) {
                linhaAtual.show();
                contagemDeLinhasExibidas++;
            } else {
                linhaAtual.hide();
            }
        });

        if (contagemDeLinhasExibidas === 0) {
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
        tableBody.append(`<tr style="display:none"><td>${produto.nome}</td><td>${valorFormatadoEmReais}</td><td>${produto.marca.nome}</td><td>${produto.marca.codigoMarca}</td></tr>`);
    });
}
