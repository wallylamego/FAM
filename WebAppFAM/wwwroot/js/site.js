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
