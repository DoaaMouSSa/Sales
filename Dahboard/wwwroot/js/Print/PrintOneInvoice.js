function printTbl(tblName) {
    var tbl = document.getElementById(tblName);
    var row = tbl.rows;
    if (row.length >= 2 && $("#txtName1").val() !="") {
         //Removing the column at index(1).       
        for (var j = 0; j < row.length; j++) {
            //hide the ith cell of each row.
            tbl.rows[j].cells[5].style.display = "none";
        }
        var opt = {
            margin: 1,
            filename: 'myfile.pdf',
            image: { type: 'jpeg', quality: 1 },
            html2canvas: { scale: 2 },
            bodyStyles: { font: "Amiri" },
            jsPDF: { unit: 'in', format: 'letter', orientation: '1' }
        };
        // New Promise-based usage:
        html2pdf().set(opt).from(tbl).toPdf().get('pdf').then(function (pdf) {
            var totalPages = pdf.internal.getNumberOfPages();
            //pdf.text(pdf.internal.pageSize.getWidth() / 2, .7, 'Reports', { lang: 'ar' });
            for (var i = 1; i <= totalPages; i++) {
                pdf.setPage(i);
                pdf.setFontSize(10);
                //for fading colors-increase number to fade color
                pdf.setTextColor(150);
                //divided by 2 to go center
                pdf.text('page ' + i + ' of ' + totalPages, pdf.internal.pageSize.getWidth() / 2,
                pdf.internal.pageSize.getHeight() / 2);

            }
            window.open(pdf.output('bloburl'), '_blank');
            for (var j = 0; j < row.length; j++) {
                //show the ith cell of each row.
                tbl.rows[j].cells[5].style.display = "block";
            }
        });
    } else {
        alert('لا يوجد بيانات للطباعه');
    }
}

   