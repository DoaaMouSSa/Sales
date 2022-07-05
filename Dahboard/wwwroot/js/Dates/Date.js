function GetDateOfNow() {
    $('.dateOfNow').val(new Date().toJSON().slice(0,19));
} GetDateOfNow();
function GetDateOfMonthBefore() {
    var d = new Date();
    //d.toLocaleTimeString([], {
    //    hourCycle: 'h23',
    //    hour: '2-digit',
    //    minute: '2-digit'
    //});
    d.setMonth(d.getMonth()-3);
     $('.dateOfMonthBefore').val(d.toJSON().slice(0, 19));
} GetDateOfMonthBefore();
function GetDateOfNowWitoutHrs() {
    var d = new Date();
    //d.setHours(d.getHours()+2);
    //alert(d.getHours()+2);
    $('.dateOfNowOutHrs').val(d.toJSON().slice(0, 10));
} GetDateOfNowWitoutHrs();