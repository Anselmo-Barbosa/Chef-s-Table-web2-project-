function trocarCapa(url) {
    const capa = document.getElementById("capaImagem");
    capa.src = url;
}

function abrirModal(url) {
    document.getElementById("modalFoto").style.display = "flex";
    document.getElementById("imgModal").src = url;
}

function fecharModal() {
    document.getElementById("modalFoto").style.display = "none";
}