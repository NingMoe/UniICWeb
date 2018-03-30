function GetFee2(uintFee,uintTime,totalTime){
    var vUintfee = parseFloat(uintFee);
    var vUintTime = parseFloat(uintTime);
    var vTotalTime = parseFloat(totalTime);
    return toDecimal(vUintfee * vTotalTime / vUintTime);
}
function GetFeeYuan2(uintFee, uintTime, totalTime) {
    var vUintfee = parseFloat(uintFee);
    var vUintTime = parseFloat(uintTime);
    var vTotalTime = parseFloat(totalTime);
    var vRes = vUintfee * vTotalTime / vUintTime;
    return toDecimal(vRes/100);
}
function toDecimal(x) {
    var f = parseFloat(x);
    if (isNaN(f)) {
        return;
    }
    f = Math.round(x * 100) / 100;
    return f;
}
