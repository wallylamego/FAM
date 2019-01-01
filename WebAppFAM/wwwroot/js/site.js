// Write your Javascript code.
function cleanSpaces(inputString) {
    //Remove all Line Breaks
    var newStr = inputString.replace(/(\r\n|\n|\r)/gm, "");
    //Remove all double spaces and replace with one space
    var newStr1 = newStr.replace(/\s+/g, " ");
    //Remove leading and trailing spaces
    var newStr2 = $.trim(newStr1);
    return newStr2;
}
function FormatToDecimal(inputString) {
    var newStr1 = parseFloat(inputString);
    newStr2 = newStr1.toLocaleString(
        undefined, // leave undefined to use the browser's locale,
        // or use a string like 'en-US' to override it.
        { 'minimumFractionDigits': 2, 'maximumFractionDigits': 2 });
                    //minimumFractionDigits
    return newStr2;
}
