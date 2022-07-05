let selectedFile;
document.getElementById("SelectedExcel").addEventListener("change", (event) => {
    selectedFile = event.target.files[0];
})
document.getElementById("btnImport").addEventListener("click", () => {
    if (selectedFile) {
        let fileReader = new FileReader();
        fileReader.readAsBinaryString(selectedFile);
        fileReader.onload = (event) => {
            console.log(event.target.result);
        }
    }
})

