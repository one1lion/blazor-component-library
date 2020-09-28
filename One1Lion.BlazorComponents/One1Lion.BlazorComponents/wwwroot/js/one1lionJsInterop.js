window.one1lionJsFunctions = {
  setFocusById: function (elementId, autoSelectText) {
    return one1lionJsFunctions.setFocus(document.getElementById(elementId), autoSelectText);
  },
  setFocus: function (element, autoSelectText) {
    if (element) {
      if (autoSelectText && element.value.length > 0) {
        element.setSelectRange(0, element.value.length);
      }
      return element.focus();
    }
    return null;
  },
  positionElementById: function (elementId, top = "unset", right = "unset", bottom = "unset", left = "unset") {
    return one1lionJsFunctions.positionElement(document.getElementById(elementId), top, right, bottom, left);
  },
  positionElement: function (element, top = "unset", right = "unset", bottom = "unset", left = "unset") {
    if (element) {
      if (top !== "unset") { element.style.top = top + "px"; }
      if (right !== "unset") { element.style.right = right + "px"; }
      if (bottom !== "unset") { element.style.bottom = bottom + "px"; }
      if (left !== "unset") { element.style.left = left + "px"; }
      return element;
    }
    return null;
  },
  saveAsFile: function (filename, bytesBase64) {
    if (navigator.msSaveBlob) {
      //Download document in Edge browser
      var data = window.atob(bytesBase64);
      var bytes = new Uint8Array(data.length);
      for (var i = 0; i < data.length; i++) {
        bytes[i] = data.charCodeAt(i);
      }
      var blob = new Blob([bytes.buffer], { type: "application/octet-stream" });
      navigator.msSaveBlob(blob, filename);
    }
    else {
      var link = document.createElement('a');
      link.download = filename;
      link.href = "data:application/octet-stream;base64," + bytesBase64;
      document.body.appendChild(link); // Needed for Firefox
      link.click();
      document.body.removeChild(link);
    }
  },
  hyphenate: function (a, b, c) {
    return b + "-" + c.toLowerCase();
  },
  getStyle: function (target, prop) {
    if (window.getComputedStyle) { // gecko and webkit
      prop = prop.replace(/([a-z])([A-Z])/, one1lionJsFunctions.hyphenate);  // requires hyphenated, not camel
      return window.getComputedStyle(target, null).getPropertyValue(prop);
    }
    if (target.currentStyle) {
      return target.currentStyle[prop];
    }
    return target.style[prop];
  },
  getUnitsById: function (elementId, prop) {
    return one1lionJsFunctions.getUnits(document.getElementById(elementId), prop);
  },
  getUnits: function (target, prop) {
    var baseline = 100;  // any number serves 
    var item;  // generic iterator

    var map = {  // list of all units and their identifying string
      pixel: "px",
      percent: "%",
      inch: "in",
      cm: "cm",
      mm: "mm",
      point: "pt",
      pica: "pc",
      em: "em",
      ex: "ex"
    };

    var factors = {};  // holds ratios
    var units = {};  // holds calculated values

    var value = one1lionJsFunctions.getStyle(target, prop);  // get the computed style value

    var numeric = value.match(/\d+/);  // get the numeric component
    if (numeric === null) {  // if match returns null, throw error...  use === so 0 values are accepted
      throw "Invalid property value returned";
    }
    numeric = numeric[0];  // get the string

    var unit = value.match(/\D+$/);  // get the existing unit
    unit = (unit == null) ? map.pixel : unit[0]; // if its not set, assume px - otherwise grab string

    var activeMap;  // a reference to the map key for the existing unit
    for (item in map) {
      if (map[item] == unit) {
        activeMap = item;
        break;
      }
    }
    if (!activeMap) { // if existing unit isn't in the map, throw an error
      throw "Unit not found in map";
    }

    var temp = document.createElement("div");  // create temporary element
    temp.style.overflow = "hidden";  // in case baseline is set too low
    temp.style.visibility = "hidden";  // no need to show it

    target.parentElement.appendChild(temp); // insert it into the parent for em and ex  

    for (item in map) {  // set the style for each unit, then calculate it's relative value against the baseline
      temp.style.width = baseline + map[item];
      factors[item] = baseline / temp.offsetWidth;
    }

    for (item in map) {  // use the ratios figured in the above loop to determine converted values
      units[item] = numeric * (factors[item] * factors[activeMap]);
    }

    target.parentElement.removeChild(temp);  // clean up

    return units;  // returns the object with converted unit values...
  },
  getBoundingBoxById: function (elementId) {
    return one1lionJsFunctions.getBoundingBox(document.getElementById(id));
  },
  getBoundingBox: function (element) {
    if (element === window) {
      return {
        x: 0,
        y: 0,
        widht: window.innerWidth,
        height: window.innerHeight,
        top: 0,
        right: 0,
        bottom: 0,
        left: 0
      };
    }

    return element.getBoundingClientRect();
  },
  scrollElementIntoViewById: function (id) {
    one1lionJsFunctions.scrollElementIntoView(document.getElementById(id));
  },
  scrollElementIntoView: function (element) {
    element.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
  },
  setInputValueById: function (elementId, val) {
    return one1lionJsFunctions.setInputValue(document.getElementById(elementId));
  },
  setInputValue: function (element, val) {
    if (element.value === undefined) { return; }
    element.value = val;
  },
  showPrintDialog: function () {
    window.print();
  }
};
