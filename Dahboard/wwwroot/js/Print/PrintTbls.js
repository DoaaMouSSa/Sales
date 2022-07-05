function printTbl(tblName) {
    var tbl = document.getElementById(tblName);
    var row = tbl.rows;
    if (row.length > 1) {
        // Removing the column at index(1).  
        var i = 6;
        for (var j = 0; j < row.length; j++) {
            //hide the ith cell of each row.
            tbl.rows[j].cells[i].style.display = "none";
        }
        
        var opt = {
            margin: 1,
            filename: 'myfile.pdf',
            image: { type: 'jpeg', quality: 1 },
            html2canvas: { scale: 2 },
            bodyStyles: { font: "Amiri" },
            jsPDF: { unit: 'in', format: 'letter', orientation: 'portrait' }
        };

        // New Promise-based usage:
        html2pdf().set(opt).from(tbl).toPdf().get('pdf').then(function (pdf) {
            var totalPages = pdf.internal.getNumberOfPages();
            pdf.text(pdf.internal.pageSize.getWidth() / 2, .7, 'Reports', { lang: 'ar' });
            for (var i = 1; i <= totalPages; i++) {
                pdf.setPage(i);
                pdf.setFontSize(10);
                //for fading colors-increase number to fade color
                pdf.setTextColor(150);
                //divided by 2 to go center
                pdf.text('page ' + i + ' of ' + totalPages, pdf.internal.pageSize.getWidth() / 2,
                    pdf.internal.pageSize.getHeight() / 2);
                //divided by 2 to go center
               
                //add-image
                //pdf.addImage(myImage, 'JPEG', 10, 30, 150, 76);

            }
            window.open(pdf.output('bloburl'), '_blank');
            var i = 6;
            for (var j = 0; j < row.length; j++) {
                //show the ith cell of each row.
                tbl.rows[j].cells[i].style.display = "block";
            }
        });
    } else {
        alert('لا يوجد بيانات للطباعه');
    }
    // Old monolithic-style usage:
    //html2pdf(element, opt);
    //var specialElementHandlers = {
    //    '.no-export': function (element, renderer) {
    //        return true;
    //    }
    //};
    //var doc = new jsPDF('p', 'pt', 'a4');
    //doc.setFont('NafeesNastaleeq '); // set font
    //doc.setLanguage("ar")
    //    var source = document.getElementById('tbl').innerHTML;
    //    var margins = {
    //        top: 10,
    //        bottom: 10,
    //        left: 10,
    //        width: 595
    //    };

    //doc.fromHTML(source,
    //    margins.left,
    //    margins.top, {
    //    'width': margins.width,
    //    'elementHandlers': specialElementHandlers
    //},

    //    function (dispose) {
    //        doc.output('dataurlnewwindow');
    //    }, margins);
    //var pdf = new jsPDF('p', 'pt', 'letter');
    //let dataSrc = pdf.output("datauristring");
    //let win = window.open("", "myWindow");
    //win.document.write("<html><head><title>jsPDF</title></head><body><h1>تقرير</h1><embed src=" +
    //    dataSrc + "></embed></body></html>");

    //var doc = new jsPDF('p', 'pt', 'letter');
    //var htmlstring = '';
    //var tempVarToCheckPageHeight = 0;
    //var pageHeight = 0;
    //pageHeight = doc.internal.pageSize.height;
    //specialElementHandlers = {
    //    // element with id of "bypass" - jQuery style selector  
    //    '#bypassme': function (element, renderer) {
    //        // true = "handled elsewhere, bypass text extraction"  
    //        return true
    //    }
    //};
    //margins = {
    //    top: 150,
    //    bottom: 60,
    //    left: 40,
    //    right: 40,
    //    width: 600
    //};
    //var y = 20;
    //doc.setLineWidth(2);
    //doc.text(200, y = y + 30, "TOTAL MARKS OF STUDENTS");
    //doc.autoTable({
    //    html: '#tbl',
    //    startY: 70,
    //    theme: 'grid',
    //    columnStyles: {
    //        0: {
    //            cellWidth: 180,
    //        },
    //        1: {
    //            cellWidth: 180,
    //        },
    //        2: {
    //            cellWidth: 180,
    //        }
    //    },
    //    styles: {
    //        minCellHeight: 40
    //    }
    //})
    //doc.save('Marks_Of_Students.pdf');
}
    //const input = document.getElementById('divIdToPrint');
    //html2canvas(input)
    //    .then((canvas) => {
    //        var doc = new jsPDF()
    //        doc.addPage();
    //        doc.text(20, 20, 'تقرير!');
    //        doc.text(20, 30, 'This is client-side Javascript, pumping out a PDF.');

    //        // Making Data URI
    //        var out = doc.output();
    //        var url = 'data:application/pdf;base64,' + btoa(out);

    //        var iframe = "<iframe width='100%' height='100%' src='" + url + "'></iframe>"
    //        var x = window.open();
    //        x.document.open();
    //        x.document.write(iframe);
    //        x.document.close();

    //        document.location.href = url;
    //    });
 
    //var doc = new jsPDF();
    //doc.setProperties({
    //    title: "new Report"
    //});
    //doc.output('dataurlnewwindow');

        //var pdf = new jsPDF('p', 'pt', 'letter');
        //// source can be HTML-formatted string, or a reference
        //// to an actual DOM element from which the text will be scraped.
        //source = $('#tbl')[0];

        //// we support special element handlers. Register them with jQuery-style 
        //// ID selector for either ID or node name. ("#iAmID", "div", "span" etc.)
        //// There is no support for any other type of selectors 
        //// (class, of compound) at this time.
        //specialElementHandlers = {
        //    // element with id of "bypass" - jQuery style selector
        //    '#bypassme': function (element, renderer) {
        //        // true = "handled elsewhere, bypass text extraction"
        //        return true
        //    }
        //};
        //margins = {
        //    top: 80,
        //    bottom: 60,
        //    left: 40,
        //    width: 522
        //};
        //// all coords and widths are in jsPDF instance's declared units
        //// 'inches' in this case
        //pdf.fromHTML(
        //    source, // HTML string or DOM elem ref.
        //    margins.left, // x coord
        //    margins.top, {// y coord
        //    'width': margins.width, // max width of content on PDF
        //    'elementHandlers': specialElementHandlers
        //},
        //    function (dispose) {
        //        // dispose: object with X, Y of the last line add to the PDF 
        //        //          this allow the insertion of new lines after html
        //        //pdf.save('Test.pdf');
        //      pdf.output('dataurlnewwindow');
        //    }
        //    , margins);
    
//}