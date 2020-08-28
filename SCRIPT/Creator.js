// log types
export const LogTypes = {
    Default: "DEFAULT",
    Info: "INFO",
    Warn: "WARN",
    Error: "ERROR"
};

// date types
export const DateTypes = {
    Current: "CURRENT",
    Log: "LOG",
    Short: "SHORT",
    Long: "LONG"
};

// supported types by the get function
export const GetTypes = {
    Name: "NAME",
    Id: "ID",
    Tag: "TAG", 
    Query: "QUERY",
    QueryAll: "QUERYALL"
};

// gets elements based on type and elm value
export const get = (type, elm) => {
    let result = null;

    switch (upper(type)) {
        case GetTypes.Name:
            result = document.getElementsByName(elm);
            break;

        case GetTypes.Id:
            result = document.getElementById(elm);
            break;

        case GetTypes.Tag:
            result = document.getElementsByTagName(elm);
            break;

        case GetTypes.Query:
            result = document.querySelector(elm).elements;
            break;

        case GetTypes.QueryAll:
            result = document.querySelectorAll(elm);
            break;

        default:
            throw new Error(type + " not supported");
    }

    return result;
};

// makes an element visible
const show = (type, elm) => {
    let result = get(type, elm);

    if (!Null(result)) {
        result.style.transition = "all 100ms";
        result.style.opacity = 1;
    }
};

// hides an element
const hide = (type, elm) => {
    let result = get(type, elm);

    if (!Null(result)) {
        result.style.transition = "all 100ms";
        result.style.opacity = 0;
    }
};

// gets date in different formats
const getDate = (type = DateTypes.Short) => {
    var result = null;
    const current = new Date();

    if (equal(type, DateTypes.Current)) {
        result = current;
    } else if (equal(type, DateTypes.Log)) {
        result = "[" + current.getUTCHours() + ":" + current.getUTCMinutes() + "." + current.getUTCSeconds() + "." + current.getUTCMilliseconds() + " - " + current.getUTCDay() + "." + current.getUTCMonth() + "." + current.getUTCFullYear() + "]";
    } else if (equal(type, DateTypes.Short)) {
        result = current.getUTCDay() + "." + current.getUTCMonth() + "." + current.getUTCFullYear();
    } else if (equal(type, DateTypes.Long)) {
        result = getDate(DateTypes.Short) + " " + current.getUTCHours() + ":" + current.getUTCMinutes() + "." + current.getUTCSeconds();
    }

    return result;
};

// appends the element to the found node based on type and typeStr
const append = (type, typeStr, element) => {
    let result = get(type, typeStr);

    if (result) {
        result.appendChild(element);
    }

    return result;
};

// performs log to console
const log = (type = LogTypes.Default, entry = EmptyString) => {
    if (equal(type, LogTypes.Default)) {
        console.log(getDate(DateTypes.Log), entry);
    } else if (equal(type, LogTypes.Info)) {
        console.info(getDate(DateTypes.Log), entry);
    } else if (equal(type, LogTypes.Warn)) {
        console.warn(getDate(DateTypes.Log), entry);
    } else if (equal(type, LogTypes.Error)) {
        console.error(getDate(DateTypes.Log), entry);
    }
    else {
        throw new Error(type + " not supported");
    }
};

// trims both start and end
const trim = (str) => str.trimStart().trimEnd();

// return true if obj is null or undefined
const Null = (obj) => typeof obj === null || typeof obj === "undefined";

// returns true if value is json
const isJSON = (json) => {
    var result = false;

    try {
        JSON.parse(json);
        result = true;
    } catch (e) {
        result = false;
    }

    return result;
};

// returns true if str is string
const isString = (str) => typeof str === "string";

// returns true if str is a number
const isNumeric = (str) => typeof str === "number";

// returns true if str is of type boolean
const isBool = (str) => typeof str === "boolean";

// turns str to upper case
const upper = (str) => str.toUpperCase();

// turns str to lower case
const lower = (str) => str.toLowerCase();

// returns true if val and compare are equal
const equal = (val, compare) => equals(val, compare);

// returns true if all values in compares are equal to val
const equals = (val, ...compares) => {
    var result = true;

    compares.map(compare => {
        if (isString(val)) {
            if (upper(val) !== upper(compare)) {
                result = false;
                break;
            }
        } else {
            if (val !== compare) {
                result = false;
                break;
            }
        }
    });

    return result;
};

// converts the first letter to upper case
const capitalize = (str) => str.charAt(0).toUpperCase() + str.slice(1);

const camelCase = (str) => {
    var result = "";
    var count = -1;

    for (var s of str.split(" ")) {
        if (count > 0) {
            result += " ";
        }

        result += capitalize(s);

        count++;
    }

    return result;
};

// returns an array based on the list
const commaListToArray = (list, trimElement) => {
    var result = [];

    for (var str of list.split(",")) {
        const tmp = trimElement ? trim(str) : str;
        result.push(tmp);
    }

    return result;
};

// invokes return function when element changes
const onChange = (type, str, function (event) {
    get(type, str).addEventListener('change', (event) => {
        return event;
    });
});
