// knockout-jqAutocomplete 0.4.3 | (c) 2015 Ryan Niemeyer |  http://www.opensource.org/licenses/mit-license
; (function (factory) {
    if (typeof define === "function" && define.amd) {
        // AMD anonymous module
        define(["knockout", "jquery", "jquery-ui/autocomplete"], factory);
    } else {
        // No module loader - put directly in global namespace
        factory(window.ko, jQuery);
    }
})(function (ko, $) {
    //jqAuto -- main binding (should contain additional options to pass to autocomplete)
    //jqAutoSource -- the array to populate with choices (needs to be an observableArray)
    //jqAutoQuery -- function to return choices (if you need to return via AJAX)
    //jqAutoValue -- where to write the selected value
    //jqAutoSourceLabel -- the property that should be displayed in the possible choices
    //jqAutoSourceInputValue -- the property that should be displayed in the input box
    //jqAutoSourceValue -- the property to use for the value
    var JqAuto = function () {
        var self = this,
            unwrap = ko.utils.unwrapObservable; //support older KO versions that did not have ko.unwrap

        //binding's init function
        this.init = function (element, valueAccessor, allBindings, data, context) {
            var existingSelect, existingChange,
                options = unwrap(valueAccessor()),
                config = {},
                filter = typeof options.filter === "function" ? options.filter : self.defaultFilter;

            //extend with global options
            ko.utils.extend(config, self.options);
            //override with options passed in binding
            ko.utils.extend(config, options.options);

            //get source from a function (can be remote call)
            if (typeof options.source === "function" && !ko.isObservable(options.source)) {
                config.source = function (request, response) {
                    //provide a wrapper to the normal response callback
                    var callback = function (data) {
                        self.processOptions(valueAccessor, null, data, request, response);
                    };

                    //call the provided function for retrieving data
                    options.source.call(context.$data, request.term, callback);
                };
            }
            else {
                //process local data
                config.source = self.processOptions.bind(self, valueAccessor, filter, options.source);
            }

            //save any passed in select/change calls
            existingSelect = typeof config.select === "function" && config.select;
            existingChange = typeof config.change === "function" && config.change;

            //handle updating the actual value
            config.select = function (event, ui) {
                if (ui.item && ui.item.actual) {
                    options.value(ui.item.actual);

                    if (ko.isWriteableObservable(options.dataValue)) {
                        options.dataValue(ui.item.data);
                    }
                }

                if (existingSelect) {
                    existingSelect.apply(this, arguments);
                }
            };


            //user made a change without selecting a value from the list
            config.change = function (event, ui) {
                //WHAT AM I DOING
                if (options.ignoreBlur !== true) {
                    if (!ui.item || !ui.item.actual) {
                        options.value(event.target && event.target.value);

                        if (ko.isWriteableObservable(options.dataValue)) {
                            options.dataValue(null);
                        }
                    }

                    if (existingChange) {
                        existingChange.apply(this, arguments);
                    }
                }
            };

            //initialize the widget
            var widget = $(element).autocomplete(config).data("ui-autocomplete");

            //render a template for the items
            if (options.template) {
                widget._renderItem = self.renderItem.bind(self, options.template, context);
            }

            //destroy the widget if KO removes the element
            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                if (widget && typeof widget.destroy === "function") {
                    widget.destroy();
                    widget = null;
                }
            });
        };

        //the binding's update function. keep value in sync with model
        this.update = function (element, valueAccessor) {
            var propNames, sources,
                options = unwrap(valueAccessor()),
                value = unwrap(options && options.value);

            if (!value && value !== 0) {
                value = "";
            }

            // find the appropriate value for the input
            sources = unwrap(options.source);
            propNames = self.getPropertyNames(valueAccessor);

            // if there is local data, then try to determine the appropriate value for the input
            if ($.isArray(sources) && propNames.value) {
                value = ko.utils.arrayFirst(sources, function (opt) {
                    return opt[propNames.value] == value;
                }
                ) || value;
            }

            if (propNames.input && value && typeof value === "object") {
                element.value = value[propNames.input];
            }
            else {
                element.value = value;
            }
        };

        //if dealing with local data, the default filtering function
        this.defaultFilter = function (item, term) {
            term = term && term.toLowerCase();
            return (item || item === 0) && ko.toJSON(item).toLowerCase().indexOf(term) > -1;
        };

        //filter/map options to be in a format that autocomplete requires
        this.processOptions = function (valueAccessor, filter, data, request, response) {
            var item, index, length,
                items = unwrap(data) || [],
                results = [],
                props = this.getPropertyNames(valueAccessor);

            //filter/map items
            for (index = 0, length = items.length; index < length; index++) {
                item = items[index];

                if (!filter || filter(item, request.term)) {
                    results.push({
                        label: props.label ? item[props.label] : item.toString(),
                        value: props.input ? item[props.input] : item.toString(),
                        actual: props.value ? item[props.value] : item,
                        data: item
                    });
                }
            }

            //call autocomplete callback to display list
            response(results);
        };

        //if specified, use a template to render an item
        this.renderItem = function (templateName, context, ul, item) {
            var $li = $("<li></li>").appendTo(ul),
                itemContext = context.createChildContext(item.data);

            //apply the template binding
            ko.applyBindingsToNode($li[0], { template: templateName }, itemContext);

            //clean up
            $li.one("remove", ko.cleanNode.bind(ko, $li[0]));

            return $li;
        };

        //retrieve the property names to use for the label, input, and value
        this.getPropertyNames = function (valueAccessor) {
            var options = ko.toJS(valueAccessor());

            return {
                label: options.labelProp || options.valueProp,
                input: options.inputProp || options.labelProp || options.valueProp,
                value: options.valueProp
            };
        };

        //default global options passed into autocomplete widget
        this.options = {
            autoFocus: true,
            delay: 50
        };
    };

    ko.bindingHandlers.jqAuto = new JqAuto();

})

//jqAuto -- main binding (should contain additional options to pass to autocomplete)
//jqAutoSource -- the array to populate with choices (needs to be an observableArray)
//jqAutoQuery -- function to return choices (if you need to return via AJAX)
//jqAutoValue -- where to write the selected value
//jqAutoSourceLabel -- the property that should be displayed in the possible choices
//jqAutoSourceInputValue -- the property that should be displayed in the input box
//jqAutoSourceValue -- the property to use for the value
ko.bindingHandlers.jqAuto2 = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var options = valueAccessor() || {},
            allBindings = allBindingsAccessor(),
            unwrap = ko.utils.unwrapObservable,
            modelValue = allBindings.jqAutoValue,
            source = allBindings.jqAutoSource,
            query = allBindings.jqAutoQuery,
            valueProp = allBindings.jqAutoSourceValue,
            inputValueProp = allBindings.jqAutoSourceInputValue || valueProp,
            labelProp = allBindings.jqAutoSourceLabel || inputValueProp,
            // New setting to allow /disallow a user to enter values that aren't in the autocomplete list.
            forceSelection = allBindings.jqAutoForceSelection;

        //function that is shared by both select and change event handlers
        function writeValueToModel(valueToWrite) {
            if (ko.isWriteableObservable(modelValue)) {
                modelValue(valueToWrite);
            } else {  //write to non-observable
                if (allBindings['_ko_property_writers'] && allBindings['_ko_property_writers']['jqAutoValue'])
                    allBindings['_ko_property_writers']['jqAutoValue'](valueToWrite);
            }
        }

        //on a selection write the proper value to the model
        options.select = function (event, ui) {
            writeValueToModel(ui.item ? ui.item.actualValue : null);
        };

        //on a change, make sure that it is a valid value or clear out the model value
        options.change = function (event, ui) {
            var currentValue = $(element).val();
            // Start New Code
            if (!forceSelection) {
                writeValueToModel(currentValue);
                return;
            }
            // End New Code
            var matchingItem = ko.utils.arrayFirst(unwrap(source), function (item) {
                return unwrap(item[inputValueProp]) === currentValue;
            });

            if (!matchingItem) {
                writeValueToModel(null);
            }
        }

        //hold the autocomplete current response
        var currentResponse = null;

        //handle the choices being updated in a DO, to decouple value updates from source (options) updates
        var mappedSource = ko.dependentObservable({
            read: function () {
                mapped = ko.utils.arrayMap(unwrap(source), function (item) {
                    var result = {};
                    result.label = labelProp ? unwrap(item[labelProp]) : unwrap(item).toString();  //show in pop-up choices
                    result.value = inputValueProp ? unwrap(item[inputValueProp]) : unwrap(item).toString();  //show in input box
                    result.actualValue = valueProp ? unwrap(item[valueProp]) : item;  //store in model
                    return result;
                });
                return mapped;
            },
            write: function (newValue) {
                source(newValue);  //update the source observableArray, so our mapped value (above) is correct
                if (currentResponse) {
                    currentResponse(mappedSource());
                }
            }
        });

        if (query) {
            options.source = function (request, response) {
                currentResponse = response;
                query.call(this, request.term, mappedSource);
            }
        } else {
            //whenever the items that make up the source are updated, make sure that autocomplete knows it
            mappedSource.subscribe(function (newValue) {
                $(element).autocomplete("option", "source", newValue);
            });

            options.source = mappedSource();
        }

        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).autocomplete("destroy");
        });


        //initialize autocomplete
        $(element).autocomplete(options);

    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        //update value based on a model change
        var allBindings = allBindingsAccessor(),
            unwrap = ko.utils.unwrapObservable,
            modelValue = unwrap(allBindings.jqAutoValue) || '',
            valueProp = allBindings.jqAutoSourceValue,
            inputValueProp = allBindings.jqAutoSourceInputValue || valueProp;

        //if we are writing a different property to the input than we are writing to the model, then locate the object
        if (valueProp && inputValueProp !== valueProp) {
            var source = unwrap(allBindings.jqAutoSource) || [];
            var modelValue = ko.utils.arrayFirst(source, function (item) {
                return unwrap(item[valueProp]) === modelValue;
            }) || {};
        }

        //update the element with the value that should be shown in the input
        $(element).val(modelValue && inputValueProp !== valueProp ? unwrap(modelValue[inputValueProp]) : modelValue.toString());
    }
};
//<input data-bind="datepicker: myDate, datepickerOptions: { minDate: new Date() }" />

ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        //initialize datepicker with some optional options
        var options = allBindingsAccessor().datepickerOptions || {};

        var funcOnSelectdate = function () {
            var observable = valueAccessor();
            observable($(element).datepicker("getDate"));
        }
        options.onSelect = funcOnSelectdate;

        $(element).datepicker(options);

        //handle the field changing
        ko.utils.registerEventHandler(element, "change", funcOnSelectdate);

        //handle disposal (if KO removes by the template binding)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).datepicker("destroy");
        });

    },
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());

        //handle date data coming via json from Microsoft
        if (String(value).indexOf('/Date(') == 0) {
            value = new Date(parseInt(value.replace(/\/Date\((.*?)\)\//gi, "$1")));
        }

        var current = $(element).datepicker("getDate");

        if (value - current !== 0) {
            $(element).datepicker("setDate", value);
        }
    }
};

if (!Array.prototype.forEach) {
    Array.prototype.forEach = function (fn, scope) {
        'use strict';
        var i, len;
        for (i = 0, len = this.length; i < len; ++i) {
            if (i in this) {
                fn.call(scope, this[i], i, this);
            }
        }
    };
}



ko.bindingHandlers.dialog = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        var options = ko.utils.unwrapObservable(valueAccessor()) || {};
        //do in a setTimeout, so the applyBindings doesn't bind twice from element being copied and moved to bottom
        setTimeout(function () {
            options.close = function () {
                allBindingsAccessor().dialogVisible(false);
            };

            $(element).dialog(options);
        }, 0);

        //handle disposal (not strictly necessary in this scenario)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).dialog("destroy");
        });
    },
    update: function (element, valueAccessor, allBindingsAccessor) {
        var shouldBeOpen = ko.utils.unwrapObservable(allBindingsAccessor().dialogVisible);
        if ($(element).data('uiDialog')) {
            $(element).dialog(shouldBeOpen ? "open" : "close");
        }

    }
};

//custom binding to initialize a jQuery UI dialog
ko.bindingHandlers.jqDialog = {
    init: function (element, valueAccessor) {
        var options = ko.utils.unwrapObservable(valueAccessor()) || {};

        //handle disposal
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).dialog("destroy");
        });

        //dialog is moved to the bottom of the page by jQuery UI. Prevent initial pass of ko.applyBindings from hitting it
        setTimeout(function () {
            $(element).dialog(options);
        }, 0);
    }
};

//custom binding handler that opens/closes the dialog
ko.bindingHandlers.openDialog = {
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        if ($(element).data('uiDialog')) {
            if (value) {
                $(element).dialog("open");
            } else {
                $(element).dialog("close");
            }
        }

    }
};

//custom binding to initialize a jQuery UI button
ko.bindingHandlers.jqButton = {
    init: function (element, valueAccessor) {
        var options = ko.utils.unwrapObservable(valueAccessor()) || {};

        //handle disposal
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).button("destroy");
        });

        $(element).button(options);
    }
};


/*global ko, document*/

// Github repository: https://github.com/One-com/knockout-select-on-focus
// License: standard 3-clause BSD license https://raw.github.com/One-com/knockout-dragdrop/master/LICENCE

/**
* This binding selects the text in a input field or a textarea when the
* field get focused.
*
* Usage:
*     <input type="text" data-bind="selectOnFocus: true">
*     Selects all text when the element is focused.
*
*     <input type="text" data-bind="selectOnFocus: /^[^\.]+/">
*     Selects all text before the first period when the element is focused.
*
*     <input type="text" data-bind="selectOnFocus: { pattern: /^[^\.]+/, onlySelectOnFirstFocus: true }">
*     Only select the pattern on the first focus.
*/
(function (factory) {
    if (typeof define === "function" && define.amd) {
        // AMD anonymous module with hard-coded dependency on "knockout"
        define(["knockout"], factory);
    } else {
        // <script> tag: use the global `ko` and `jQuery`
        factory(ko);
    }
})(function (ko) {
    function getOptions(valueAccessor) {
        var options = ko.utils.unwrapObservable(valueAccessor());
        if (options.pattern) {
            return options;
        } else {
            return { pattern: options };
        }
    }

    function selectText(field, start, end) {
        if (field.createTextRange) {
            var selRange = field.createTextRange();
            selRange.collapse(true);
            selRange.moveStart('character', start);
            selRange.moveEnd('character', end);
            selRange.select();
            field.focus();
        } else if (field.setSelectionRange) {
            field.focus();
            field.setSelectionRange(start, end);
        } else if (typeof field.selectionStart !== 'undefined') {
            field.selectionStart = start;
            field.selectionEnd = end;
            field.focus();
        }
    }

    ko.bindingHandlers.selectOnFocus = {
        init: function (element, valueAccessor) {
            var firstFocus = true;

            function selectContentAsync() {
                setTimeout(function () {
                    var options = getOptions(valueAccessor);
                    var pattern = ko.utils.unwrapObservable(options.pattern);
                    var onlySelectOnFirstFocus = ko.utils.unwrapObservable(options.onlySelectOnFirstFocus);

                    if (!onlySelectOnFirstFocus || firstFocus) {
                        if (Object.prototype.toString.call(pattern) === '[object RegExp]') {
                            var matchInfo = pattern.exec(element.value);
                            if (matchInfo) {
                                var startOffset = matchInfo.index,
                                endOffset = matchInfo.index + matchInfo[0].length;
                                selectText(element, startOffset, endOffset);
                            }
                        } else {
                            element.select();
                        }
                        firstFocus = false;
                    }
                }, 1);
            }

            if (document.activeElement === element) {
                selectContentAsync();
            }

            ko.utils.registerEventHandler(element, 'focus', function (e) {
                selectContentAsync();
            });
        }
    };
});