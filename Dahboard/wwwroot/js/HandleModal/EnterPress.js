function PressEnter(event,modalId) {
    if (event.keyCode === 13) {
        event.preventDefault();
        document.getElementById(modalId).click();
    }
}
function PressEnterEditCat(event, btn, id) {
    if (event.keyCode === 13) {
        EditCat(id);
        document.getElementById(btn).onclick(id);
    }
}
function PressEnterEditCustomer(event, btn, id) {
    if (event.keyCode === 13) {
        EditCustomer(id);
        document.getElementById(btn).onclick(id);
    }
}
function PressEnterEditSubCat(event, btn, id) {
    if (event.keyCode === 13) {
        EditSubCat(id);
        document.getElementById(btn).onclick(id);
    }
}
