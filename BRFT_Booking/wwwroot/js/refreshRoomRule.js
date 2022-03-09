//Reusable refresh routine for SelectLists
//Parameters: ddl_ID - ID of the Select to refresh
//  uri - /controller/action/... to call to get a new SelcectList as JSON
// showNoDataMsg - Boolean if you want to clear all options and show NO DATA message
//      if no data is returned.  Otherwise it leaves the original values in place.
//  addDefault - Boolean indicating if you want to add a default/prompt 
//      option at the top of the list.
//  defaultText - string value to display as the default/prompt value
// fadeOutIn - Boolean for visual effect after refresh
function refreshRoomRule(ddl_ID, URL, showNoDataMsg, noDataMsg, addDefault, defaultText, fadeOutIn) {
    var theDDL = $("#" + ddl_ID);
    $(function () {
        $.getJSON(URL, function (data) {
            if (data !== null && !jQuery.isEmptyObject(data)) {
                theDDL.empty();
                if (addDefault) {
                    if (defaultText == null || jQuery.isEmptyObject(defaultText)) {
                        defaultText = 'input'
                    };
                    theDDL.append($('<option/>', {
                        value: "",
                        text: defaultText
                    }));
                }
                $.each(data, function (index, item) {
                    theDDL.append($('', {
                        value: item.value,
                        text: item.text,
                    }));
                });
                theDDL.trigger("chosen:updated");
            } else {
                if (showNoDataMsg) {
                    theDDL.empty();
                    if (noDataMsg == null || jQuery.isEmptyObject(noDataMsg)) {
                        noDataMsg = 'No Matching Data'
                    };
                    theDDL.append($('<option/>', {
                        value: null,
                        text: noDataMsg
                    }));
                    theDDL.trigger("chosen:updated");
                }
            }
        });
    });
    if (fadeOutIn) {
        theDDL.fadeToggle(400, function () {
            theDDL.fadeToggle(400);
        });
    }
    return;
}