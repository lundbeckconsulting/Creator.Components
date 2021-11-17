/*
    @Author     Stein Lundbeck
    @Date       29.08.2020
 */

const LogTypes = {
    Default: "DEFAULT",
    Info: "INFO",
    Warn: "WARN",
    Error: "ERROR"
};

const DateTypes = {
    Current: "CURRENT",
    Log: "LOG",
    Short: "SHORT",
    Long: "LONG"
};

const GetTypes = {
    Name: "NAME",
    Id: "ID",
    Tag: "TAG",
    Query: "QUERY",
    QueryAll: "QUERYALL"
};

const Effects = {
    FadeIn: "FADEIN",
    FadeOut: "FADEOUT",
    SlideIn: "SLIDEIN",
    SlideOut: "SLIDEOUT",
    ScaleIn: "SCALEIN",
    ZoomHover: "ZOOMHOVER",
    PointerHover: "POINTERHOVER",
    Show: "SHOW",
    Hide: "HIDE"
};


/*
// gets elements based on type and elm value
const get = (type, elm) => {
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

// adds an effect to sn element
const effectElement = (type, elm, effect) => {
    switch (effect) {
        case Effects.FadeIn:
            addClass(type, elm, "fadeIn");
            removeClass(type, elm, "hide");
            removeClass(type, elm, "fadeOut");
            break;

        case Effects.FadeOut:
            addClass(type, elm, "fadeOut");
            removeClass(type, elm, "show");
            removeClass(type, elm, "fadeIn");
            break;

        case Effects.SlideIn:
            addClass(type, elm, "slideIn");
            removeClass(type, elm, "hide");
            removeClass("slideOut");
            break;

        case Effects.SlideOut:
            addClass(type, elm, "slideOut");
            removeClass(type, elm, "show");
            removeClass(type, elm, "slideIn");
            break;

        case Effects.ScaleIn:
            addClass(type, elm, "scaleIn");
            removeClass(type, elm, "hide");
            break;

        case Effects.ZoomHover:
            addClass(type, elm, "zoomHover");
            break;

        case Effects.PointerHover:
            addClass(type, elm, "pointerHover");
            break;

        case Effects.Show:
            addClass(type, elm, "show");
            removeClass(type, elm, "hide");
            break;

        case Effects.Hide:
            addClass(type, elm, "hide");
            removeClass(type, elm, "show");
            break;

        default:
            throw new Error(effect + " not supported");
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
const append = (type, typeStr, elm) => {
    let result = get(type, typeStr);

    if (result) {
        result.appendChild(elm);
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
const isJSON = (str) => {
    var result = false;

    try {
        JSON.parse(str);
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
                return false;
            }
        } else {
            if (val !== compare) {
                return false;
            }
        }
    });

    return result;
};

// converts the first letter to upper case
const capitalize = (str) => upper(str.charAt(0)) + lower(str.slice(1));

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
const commaListToArray = (list, trimItems = true) => {
    var result = [];

    for (var str of list.split(",")) {
        const tmp = trimItems ? trim(str) : str;

        result.push(tmp);
    }

    return result;
};

// invokes return function when element changes
const onChange = (type, str, callback) => {
    get(type, str).addEventListener('change', (event) => {
        callback(event);
    });
};

// returns str as stringifyed JSON
const JSON = (str) => JSON.stringify(str);

// adds a name to the class attribute of an element
const addClass = (type, elm, className) => {
    get(type, elm).classList.add(className);
};

// removes a name from the class attribute of an element
const removeClass = (type, elm, className) => {
    get(type, elm).classList.remove(className);
};
*/


	

﻿const toggle = (tag) => {
    if (tag.classList.contains("hide")) {
        tag.classList.remove("hide");
        tag.classList.add("show");
    }
    else {
        tag.classList.remove("show");
        tag.classList.add("hide");
    }
};


	

	//# sourceMappingUrl=Creator.Components.js.map
