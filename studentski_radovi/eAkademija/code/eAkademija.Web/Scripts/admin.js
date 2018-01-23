$(function () {

    $('.btn-category').click(function () {
        var subCategoryId = '#podkategorija_' + this.id
        if ($(subCategoryId).css("display") === 'none') {
          
            $(this).text('Sakrij Podkategorije')
            $(subCategoryId).show()

        } else {
            $(subCategoryId).hide()
            $(this).text('Prikazi Podkategorije')
        }
    })
})