let selectedFile;
console.log(window.XLSX);
document.getElementById('inputExcel').addEventListener("change", (event) => {
    selectedFile = event.target.files[0];
})

//let data = [{
//    "name": "jayanth",
//    "data": "scd",
//    "abc": "sdef"
//}]

document.getElementById('btnExcel').addEventListener("click", () => {
    //XLSX.utils.json_to_sheet(data, 'out.xlsx');
    if (selectedFile) {
        let fileReader = new FileReader();
        fileReader.readAsBinaryString(selectedFile);
        fileReader.onload = (event) => {
            let data = event.target.result;
            let workbook = XLSX.read(data, { type: "binary" });
            var sheet_name = workbook.SheetNames;
            let rowObject = workbook.Sheets[sheet_name[0]];
            console.log(rowObject[]);
            if (rowObject.length > 0) {
                for (var row = 0; row < rowObject.length; row++) {
                    for (var col = 0; col < rowObject[row].length; col++) {
                        console.log(rowObject[row][col]);
                    }
                }
            }
              //document.getElementById("jsondata").innerHTML = JSON.stringify(rowObject, undefined, 1)
         
     }
    }
});