function applyValidations() {
    jQuery('form').removeData('validator');
    jQuery('form').removeData('unobtrusiveValidation');
    jQuery.validator.unobtrusive.parse("form");
}

function showMessageHideModalRebindGrid(response, gridId, modelId) {
    if (response.IsSuccess == true) {
        $("#" + modelId).modal('hide');
        $("#" + gridId).data().kendoGrid.dataSource.read();
    }

    showNotificationMessage(response.Message, response.Type);
}

function showMessageRebindGrid(response, gridId) {
    if (response.IsSuccess == true) {
        $("#" + gridId).data().kendoGrid.dataSource.read();
    }

    showNotificationMessage(response.Message, response.Type);
}

function highlightGridRow(gridId, searchText, isLastColumnHighlight) {
    isLastColumnHighlight = isLastColumnHighlight || false;

    if (!isLastColumnHighlight) {
        $("#" + gridId + " td").not(":nth-last-child(1)").highlight(searchText);
    }
    else {
        $("#" + gridId + " td").highlight(searchText);
    }
}

function isRadioButtonChecked(fieldName) {
    return jQuery("input:radio[name='" + fieldName + "']:checked").val() == 'True';
}

function isCheckboxChecked(fieldName) {
    return jQuery("input:checkbox[name='" + fieldName + "']:checked").val() == 'true';
}

function addRequiredRule(fieldName) {
    jQuery("#" + fieldName).rules("add", { required: true, messages: { required: fieldName + " is required" } });
}

function removeRequiredRule(fieldName) {
    jQuery("#" + fieldName).rules("remove", "required");
}

function hideShowFields(checkfield, fieldName) {
    var isYesSelected = isRadioButtonChecked(checkfield);
    var hideShowField = jQuery("#" + fieldName);
    if (isYesSelected) {
        addRemoveRequiredClasses(fieldName, true);
        enableDisableFields(fieldName, false);
        hideShowField.show();
    }
    else {
        resetFormFields(fieldName);
        enableDisableFields(fieldName, true);
        addRemoveRequiredClasses(fieldName, false);
        hideShowField.hide();
    }
}

function addRemoveRequiredClasses(fieldName, isAddClass) {
    jQuery('#' + fieldName).find(':input').each(function () {
        switch (this.type) {
            case 'password':
            case 'select-multiple':
            case 'select-one':
            case 'text':
            case 'textarea':
                if (isAddClass && jQuery(this).attr("data-val-required") !== undefined) {
                    jQuery(this).addClass("input-validation-error");
                }
                else {
                    jQuery(this).removeClass("input-validation-error");
                }
        }
    });
}

function resetFormFields(formId) {
    jQuery('#' + formId).find(':input').each(function () {
        switch (this.type) {
            case 'password':
            case 'select-multiple':
            case 'select-one':
            case 'text':
            case 'textarea':
            case 'number':
                jQuery(this).val('');
                break;
            case 'checkbox':
            case 'radio':
                this.checked = false;
        }
    });
}

function enableDisableFields(formId, isDisabled) {
    jQuery('#' + formId).find(':input').each(function () {
        switch (this.type) {
            case 'password':
            case 'select-multiple':
            case 'select-one':
            case 'number':
            case 'checkbox':
            case 'radio':
                this.disabled = isDisabled;
                break;

            case 'text':
            case 'textarea':
                this.disabled = isDisabled;
                if (isDisabled) {
                    jQuery(this).attr('style', function (i, s) { return 'background : #dddddd !important;' });
                }
                else {
                    jQuery(this).attr('style', function (i, s) { return 'background : none !important;' });
                }
                break;
        }
    });
}

function assignDefaultValue(id) {
    jQuery('#' + id).find(':input').each(function () {
        if (jQuery(this).attr("data-val-number") != undefined) {
            jQuery(this).val(0);
        }
    });
}

function blockUI(selector, message) {
    if (message == undefined) {
        message = "Please Wait...";
    }

    jQuery(selector).block({
        message: "<div id='divNotify' style='font-size: 23px; font-weight: bold; margin-top: 7px;'>" + message + "<br/></div>",
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            'border-radius': '10px',
            opacity: .5,
            color: '#fff'
        }
    });
}

function addGridPageSizes() {
    return { refresh: true, pageSizes: [10, 20, 50, 100, "ALL"] };
}

function kendoReadDataSource(readUrl) {
    readUrl = readUrl.replace(/&amp;/g, '&');
    return {
        transport: { type: "get", dataType: "jsonp", read: readUrl },
        schema: { data: "Data", total: "Total" },
        serverPaging: true,
        serverSorting: true,
        serverFiltering: true,
        pageSize: 10,
        page: 1
    }
}

function filterQuery(query) {
    if (query.toLowerCase() == "yes") {
        query = "true";
    }
    else if (query.toLowerCase() == "no") {
        query = "false";
    }

    return query;
}

function parseFloatValue(value) {
    if (value == '') {
        return 0;
    }

    var result = parseFloat(value);
    if (isNaN(result)) {
        return 0;
    }

    return result;
}

String.format = function () {
    // The string containing the format items (e.g. "{0}")
    // will and always has to be the first argument.
    var theString = arguments[0];

    // start with the second argument (i = 1)
    for (var i = 1; i < arguments.length; i++) {
        // "gm" = RegEx options for Global search (more than one instance)
        // and for Multiline search
        var regEx = new RegExp("\\{" + (i - 1) + "\\}", "gm");
        theString = theString.replace(regEx, arguments[i]);
    }

    return theString;
};

function getInfoInPopup(myMappings) {
    var info = "";
    for (var i = 0; i < myMappings.length; i++) {
        var value = myMappings[i].Value == null ? "" : myMappings[i].Value;
        info += String.format("<label>{0}</label> : <label>{1}</label> <br/>", myMappings[i].Name, value);
    }
    return info;
}

function getInfoIcon(Info) {
    return String.format("<a href='javascript:;' data-toggle='popover' title='Info' class='btn btn-info btn-sm' data-content='{0}'><span class='glyphicon glyphicon-zoom-in'></span></a> ", Info);
}
function getEditButton(functionName, Id) {
    return String.format("<a href='javascript:;' onclick='{0}' class='k-button k-button-icontext'><span class='k-icon k-edit'></span>Edit</a>", functionName, Id);
}
function getDeleteButton(functionName) {
    return String.format("<a href='javascript:;' onclick='{0}' class='k-button k-button-icontext'><span class='k-icon k-delete'></span>Delete</a>", functionName);
}

function showNotificationMessage(message, type, time) {
    if (time == undefined || time == '') {
        time = 2000;
    }
    noty({ text: message, type: type, timeout: time });
}

function showSuccessMessage(message) {
    noty({ text: message, type: 'success', timeout: 2000 });
}

function showErrorMessage(message) {
    noty({ text: message, type: 'error', timeout: 2000 });
}

function showWarningMessage(message) {
    noty({ text: message, type: 'warning', timeout: 2000 });
}

function showInformationMessage(message) {
    noty({ text: message, type: 'information', timeout: 2000 });
}

function deleteAllFiles() {
    $(".btn[data-dismiss='modal']").click(function () {
        $.get('/FileUpload/DeleteAllFiles');
    });
}

