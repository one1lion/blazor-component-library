// TODO: Change the object registered to window approach to use 
//       export function instead
//export function showPrompt(message) {
//  return prompt(message, 'Type anything here');
//}


window.o1lJsFunctions = {
  addCssClass: function (selector, cssClass) {
    let elems = document.querySelectorAll(selector);
    if (elems) {
      for (let i = 0; i < elems.length; i++) {
        if (!elems[i].classList.contains(cssClass)) {
          elems[i].classList.add(cssClass);
        }
      }
    }
  },
  removeCssClass: function (selector, cssClass) {
    let elems = document.querySelectorAll(selector);
    if (elems) {
      for (let i = 0; i < elems.length; i++) {
        if (elems[i].classList.contains(cssClass)) {
          elems[i].classList.remove(cssClass);
        }
      }
    }
  },
  dynamicallyLoadScript: function (url) {
    // from https://stackoverflow.com/questions/950087/how-do-i-include-a-javascript-file-in-another-javascript-file
    var script = document.createElement("script");  // create a script DOM node
    script.src = url;  // set its src to the provided URL

    document.body.appendChild(script);
  },
  dynamicallyUnloadScript: function (url) {
    // from https://stackoverflow.com/questions/950087/how-do-i-include-a-javascript-file-in-another-javascript-file
    var script = document.createElement("script");  // create a script DOM node
    script.src = url;  // set its src to the provided URL

    document.body.appendChild(script);
  },
  elementExists: function (elementId) {
    return document.getElementById(elementId) ? true : false;
  },
  getBoundingBoxById: function (elementId) {
    return o1lJsFunctions.getBoundingBox(document.getElementById(id));
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
  getElement: function (elemOrId) {
    let elem = (typeof elemOrId === 'string') ? document.getElementById(elemOrId) : elemOrId;

    if (!elem.getAttribute && elem.id) {
      // It is possible the passed in value is an ElementRef from c#
      elem = document.getElementById(elem.id);
    }
    return elem;
  },
  getStyle: function (target, prop) {
    if (window.getComputedStyle) { // gecko and webkit
      prop = prop.replace(/([a-z])([A-Z])/, o1lJsFunctions.hyphenate);  // requires hyphenated, not camel
      return window.getComputedStyle(target, null).getPropertyValue(prop);
    }
    if (target.currentStyle) {
      return target.currentStyle[prop];
    }
    return target.style[prop];
  },
  getUnitsById: function (elementId, prop) {
    return o1lJsFunctions.getUnits(document.getElementById(elementId), prop);
  },
  getUnits: function (target, prop) {
    let baseline = 100;  // any number serves 
    let item;  // generic iterator

    let map = {  // list of all units and their identifying string
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

    let factors = {};  // holds ratios
    let units = {};  // holds calculated values

    let value = o1lJsFunctions.getStyle(target, prop);  // get the computed style value

    let numeric = value.match(/\d+/);  // get the numeric component
    if (numeric === null) {  // if match returns null, throw error...  use === so 0 values are accepted
      throw "Invalid property value returned";
    }
    numeric = numeric[0];  // get the string

    let unit = value.match(/\D+$/);  // get the existing unit
    unit = (unit == null) ? map.pixel : unit[0]; // if its not set, assume px - otherwise grab string

    let activeMap;  // a reference to the map key for the existing unit
    for (item in map) {
      if (map[item] == unit) {
        activeMap = item;
        break;
      }
    }
    if (!activeMap) { // if existing unit isn't in the map, throw an error
      throw "Unit not found in map";
    }

    let temp = document.createElement("div");  // create temporary element
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
  getUserAgent: function () {
    return navigator.userAgent;
  },
  hyphenate: function (a, b, c) {
    return b + "-" + c.toLowerCase();
  },
  isMobileDevice: function () {
    return /android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini|mobile/i.test(navigator.userAgent);
  },
  moveElementIntoView: function (elemOrId) {
    let targetElem = o1lJsFunctions.getElement(elemOrId);
    let elemBox = o1lJsFunctions.getBoundingBox(targetElem);

    // Provide a buffer for element wiggle room
    let buffer = 2; //px
    // Determine which ancestor would occlude the target element
    // TODO: If the target element's position is set to fixed, then the only bounding parent
    //       that should be used for visible bounds should be window
    let elemParent = targetElem.parentElement;
    let overflowXParent = [], overflowYParent = [], posRelContainer;
    let overflowXParentBox = [], overflowYParentBox = [], posRelContainerBox;
    while (elemParent.parentElement) {
      elemParent = elemParent.parentElement;
      let compStyle = window.getComputedStyle(elemParent);
      let isRelPosition = compStyle.position === "absolute" || compStyle.position === "relative" || compStyle.position === "fixed";
      if ((targetElem.style.position !== "fixed" && compStyle.overflowX !== "visible") || elemParent === document.body) {
        let curObj = {
          elem: elemParent,
          bbox: o1lJsFunctions.getBoundingBox(elemParent)
        };
        overflowXParent.push(curObj);
      }
      if ((targetElem.style.position !== "fixed" && compStyle.overflowY !== "visible") || elemParent === document.body) {
        let curObj = {
          elem: elemParent,
          bbox: o1lJsFunctions.getBoundingBox(elemParent)
        };
        overflowYParent.push(curObj);
      }
      // The nearest posRelContainer should be a .input-group which as position: relative;
      if (!posRelContainer && (isRelPosition || elemParent === document.body)) {
        posRelContainer = {
          elem: elemParent,
          bbox: o1lJsFunctions.getBoundingBox(elemParent)
        };
      }
    }

    // Determine if there is an active element on the page to make sure it does not get covered by the element
    let curFocusedElement = document.activeElement;
    let focusedElemBox;
    if (curFocusedElement) {
      focusedElemBox = o1lJsFunctions.getBoundingBox(curFocusedElement);
    }

    // Find the minimum bounds based on the parentX/Y (whose overflow-x/y is not set to visible)
    // and the document body
    let docWidth = document.body.clientWidth;
    let docHeight = document.body.clientHeight;

    let maxTop = 0, minBottom = 100_000;
    let maxLeft = 0, minRight = 100_000;
    overflowYParent.forEach((p, i) => {
      if (p.bbox.top > maxTop) { maxTop = p.bbox.top; }
      if (p.bbox.bottom < minBottom) { minBottom = p.bbox.bottom; }
    });
    overflowXParent.forEach((p, i) => {
      if (p.bbox.left > maxLeft) { maxLeft = p.bbox.left; }
      if (p.bbox.right < minRight) { minRight = p.bbox.right; }
    });

    let visibleBounds = {
      top: maxTop - buffer,
      right: minRight + buffer,
      bottom: minBottom + buffer,
      left: maxLeft - buffer
    };

    // Check if the state of the element's position
    //  e.g. is it full on-screen, is it partially below the screen, fully outside, etc.
    let posInfo = {};

    // Check the horizontal bounds
    if (elemBox.right < visibleBounds.left) { posInfo.fullyOutsideLeft = true; }
    if (elemBox.left > visibleBounds.right) { posInfo.fullyOutsideRight = true; }
    if (elemBox.left < visibleBounds.left && elemBox.right > visibleBounds.left) {
      if (elemBox.right < visibleBounds.right) {
        posInfo.partiallyOutsideLeft = true;
        // Get the visible %
        posInfo.visibleWidth = elemBox.right - visibleBounds.left + buffer;
        let percVisi = (posInfo.visibleWidth) / elemBox.width;
        posInfo.percentageVisible = {
          horizontal: percVisi
        };
      } else {
        posInfo.wider = true;
      }
    }
    if (elemBox.right > visibleBounds.right && elemBox.left < visibleBounds.right) {
      if (elemBox.left < visibleBounds.left) {
        posInfo.wider = true;
      } else {
        posInfo.partiallyOutsideRight = true;
        // Get the visible %
        posInfo.visibleWidth = visibleBounds.right - buffer - elemBox.left;
        let percVisi = (posInfo.visibleWidth) / elemBox.width;
        posInfo.percentageVisible = {
          horizontal: percVisi
        };
      }
    }

    // Check the vertical bounds
    if (elemBox.bottom < visibleBounds.top) { posInfo.fullyOutsideAbove = true; }
    if (elemBox.top > visibleBounds.bottom) { posInfo.fullyOutsideBelow = true; }
    if (elemBox.top < visibleBounds.top && elemBox.bottom > visibleBounds.top) {
      if (elemBox.bottom < visibleBounds.bottom) {
        posInfo.partiallyOutsideAbove = true;
        // Get the visible %
        posInfo.visibleHeight = elemBox.bottom - visibleBounds.top + buffer;
        let percVisi = (posInfo.visibleHeight) / elemBox.height;
        if (posInfo.percentageVisible) {
          posInfo.percentageVisible.vertical = percVisi;
        } else {
          posInfo.percentageVisible = {
            vertical: percVisi
          };
        }
      } else {
        posInfo.taller = true;
      }
    }
    if (elemBox.bottom > visibleBounds.bottom && elemBox.top < visibleBounds.bottom) {
      if (elemBox.top < visibleBounds.top) {
        posInfo.taller = true;
      } else {
        posInfo.partiallyOutsideBelow = true;
        // Get the visible %
        posInfo.visibleHeight = visibleBounds.bottom - buffer - elemBox.top;
        let percVisi = (posInfo.visibleHeight) / elemBox.height;
        if (posInfo.percentageVisible) {
          posInfo.percentageVisible.vertical = percVisi;
        } else {
          posInfo.percentageVisible = {
            vertical: percVisi
          };
        }
      }
    }

    if (Object.keys(posInfo).length === 0) {
      // The element is fully visible
      return;
    }

    // Find the visible portion
    // We always want to try to see the left and top
    let newPosn = {};
    newPosn.height = elemBox.height;
    newPosn.width = elemBox.width;

    if (posInfo.fullyOutsideLeft || posInfo.wider || posInfo.partiallyOutsideLeft) {
      // Always try to show the left
      newPosn.left = visibleBounds.left;
      newPosn.right = newPosn.left + newPosn.width;
    } else if (posInfo.fullyOutsideRight
      // If the element is partially outside to the left, check if enough of the element is showing
      || (posInfo.partiallyOutsideBelow && (posInfo.percentageVisible.horizontal < .8 || posInfo.visibleWidth < 100))
    ) {
      if ((visibleBounds.right - visibleBounds.left) > elemBox.width) {
        // There is enough space to move the element left so that its right edge is lined up
        // with the right edge of the visible bounds
        newPosn.right = visibleBounds.right;
        newPosn.left = newPosn.right - newPosn.width;
      } else {
        // Too tall to set the right of the element to the right of the bounding element
        // so set the left to 0 by default
        newPosn.left = visibleBounds.left;
        newPosn.right = newPosn.left + newPosn.width;
      }
    } else {
      // Use the current element's position / don't move the horizontal position
      newPosn.left = elemBox.left;
      newPosn.right = newPosn.left + newPosn.width;
    }


    if (posInfo.fullyOutsideAbove || posInfo.taller || posInfo.partiallyOutsideAbove) {
      newPosn.top = visibleBounds.top;
      newPosn.bottom = newPosn.top + newPosn.height;
    } else if (posInfo.fullyOutsideBelow
      // If the element is partially outside below, check if enough of the element is showing
      || (posInfo.partiallyOutsideBelow && (posInfo.percentageVisible.vertical < .8 || posInfo.visibleHeight < 100))
    ) {
      if (visibleBounds.bottom - visibleBounds.top > elemBox.height) {
        // There is enough space to move the element up so that its bottom edge is lined up
        // with the bottom edge of the visible bounds
        newPosn.bottom = visibleBounds.bottom;
        newPosn.top = newPosn.bottom - newPosn.height;
      } else {
        // Too tall to set the bottom of the element to the bottom of the bounding element
        // so set the top to 0 by default
        newPosn.top = visibleBounds.top;
        newPosn.bottom = newPosn.top + newPosn.height;
      }
    } else {
      // Use the current element's position / don't move the horizontal position
      newPosn.top = elemBox.top;
      newPosn.bottom = newPosn.top + newPosn.height;
    }
    // Adjust position vertically around the active item (if there is one) by checking 
    // which direction has the most space
    if (curFocusedElement
      && newPosn.top < focusedElemBox.bottom
      && newPosn.bottom > focusedElemBox.top
      && newPosn.left < focusedElemBox.right
      && newPosn.right > focusedElemBox.left
    ) {
      // The element is partially or fully covering the element that has the focus
      let spaceAbove = focusedElemBox.top - visibleBounds.top;
      let spaceBelow = visibleBounds.bottom - focusedElemBox.bottom;

      if (spaceBelow >= newPosn.height) {
        newPosn.top = focusedElemBox.bottom + buffer;
        newPosn.bottom = newPosn.top + elemBox.height;
      } else if (spaceAbove >= newPosn.height) {
        newPosn.bottom = focusedElemBox.top - buffer;
        newPosn.top = newPosn.bottom - elemBox.height;
      } else if (spaceAbove < newPosn.height || spaceBelow > spaceAbove) {
        newPosn.top = focusedElemBox.bottom + buffer;
        newPosn.bottom = newPosn.top + elemBox.height;
      } else {
        newPosn.bottom = focusedElemBox.top - buffer;
        newPosn.top = newPosn.bottom - elemBox.height;
      }
    }

    // Change the values of the element relative to its original values
    let delta = {
      x: elemBox.left - newPosn.left,
      y: elemBox.top - newPosn.top
    };
    let compVals = window.getComputedStyle(targetElem);
    let styleVals = {
      left: targetElem.style.left,
      top: targetElem.style.top
    };

    // Subtract this value from the current calculated left and top, and use that in the 
    // position method
    newPosn.left = parseFloat(compVals.left) - delta.x;
    newPosn.top = parseFloat(compVals.top) - delta.y;
    newPosn.right = newPosn.left + newPosn.width;
    newPosn.bottom = newPosn.top + newPosn.height;

    // Set the position of the element
    o1lJsFunctions.positionElement(targetElem, newPosn.top, "unset", "unset", newPosn.left);
  },
  positionElementById: function (elementId, top = "unset", right = "unset", bottom = "unset", left = "unset") {
    return o1lJsFunctions.positionElement(document.getElementById(elementId), top, right, bottom, left);
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
      let data = window.atob(bytesBase64);
      let bytes = new Uint8Array(data.length);
      for (let i = 0; i < data.length; i++) {
        bytes[i] = data.charCodeAt(i);
      }
      let blob = new Blob([bytes.buffer], { type: "application/octet-stream" });
      navigator.msSaveBlob(blob, filename);
    }
    else {
      let link = document.createElement('a');
      link.download = filename;
      link.href = "data:application/octet-stream;base64," + bytesBase64;
      document.body.appendChild(link); // Needed for Firefox
      link.click();
      document.body.removeChild(link);
    }
  },
  scrollElementIntoViewById: function (id) {
    o1lJsFunctions.scrollElementIntoView(document.getElementById(id));
  },
  scrollElementIntoView: function (element) {
    element.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
  },
  setFocusById: function (elementId, autoSelectText) {
    return o1lJsFunctions.setFocus(document.getElementById(elementId), autoSelectText);
  },
  setFocus: function (element, autoSelectText) {
    if (element) {
      if (autoSelectText && element.value.length > 0) {
        element.setSelectionRange(0, element.value.length);
      }
      return element.focus();
    }
    return null;
  },
  setInputValueById: function (elementId, val) {
    return o1lJsFunctions.setInputValue(document.getElementById(elementId), val);
  },
  setInputValue: function (element, val) {
    if (element?.checked !== undefined && (val === true || val === false)) {
      element.checked = val;
      return true;
    }
    if (element?.value) {
      element.value = val;
      return true;
    }
    return false
  },
  setPreventSelectOnDoubleClickById: function (elementId, on = true) {
    o1lJsFunctions.setPreventSelectOnDoubleClick(document.getElementById(elementId), on);
  },
  setPreventSelectOnDoubleClick: function (element, on = true) {
    let elem;
    if (element.getAttribute) {
      elem = element;
    } else {
      // This is an ElementRef from c#
      elem = document.getElementById(element.id);
    }
    if (elem.getAttribute("preventSelectOnDoubleClick") && on) { return; } // the attribute is already set to true

    if (elem.getAttribute("preventSelectOnDoubleClick") && !on) { // the attribute is set and true, but we are unsetting it
      if (elem.removeEventListener) {
        elem.removeEventListener("mousedown", preventSelectOnDoubleClick);
      } else {
        elem.detachEvent("mousedown", preventSelectOnDoubleClick)
      }
    } else if (on) { // the attribute is not set or is set to false and we want it to be true
      if (elem.addEventListener) {
        elem.addEventListener("mousedown", preventSelectOnDoubleClick);
      } else {
        elem.attachEvent("mousedown", preventSelectOnDoubleClick);
      }
    }
    elem.setAttribute("preventSelectOnDoubleClick", on);
  },
  showPrintDialog: function () {
    window.print();
  }
};

function preventSelectOnDoubleClick(e) {
  if (e.detail > 1) {
    e.preventDefault();
  }
}
