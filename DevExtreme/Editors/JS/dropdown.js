$(async () => {
    const fetchData = async () =>
        $.ajax({
            type: 'GET',
            url: 'https://6666d488a2f8516ff7a5223a.mockapi.io/userData',
            dataType: 'JSON',
            success: function (response) {
                return response;
            },
        });

    DevExpress.ui.dxDropDownBox.defaultOptions({
        device: { deviceType: 'desktop' },
        options: {
            width: '40%',
            dataSource: await fetchData(),
        },
    });
    var BasicDropDown = $('#basicDropDown')
        .dxDropDownBox({
            acceptCustomValue: true, // if true then it will allow value provided by user
            name: 'userList',
            placeHolder: 'select user',
            deferRendering: true, // If false, the content is rendered immediately.
            openOnFieldClick: true,
            valueExpr: 'id',
            displayExpr: 'userName',
            useItemTextAsTitle: true,
            noDataText: 'no data found',
            showDropDownButton: true,
            text : "Basic Drop Down",
            //readOnly : true,
            valueChangeEvent: 'blur',
            opened : false, // if true it will be opened when loaded
            inputAttr: {
                'aria-label': 'basic-dropDown',
            },
            dropDownButtonTemplate() {
                return $('<img>', {
                  alt: 'Custom icon',
                  src: "https://cdn1.iconfinder.com/data/icons/basic-ui-elements-color/700/09_search-512.png",
                  class: 'custom-icon',
                  style : "width : 100%"
                });
              },
            // custom list template
            contentTemplate(e) {
                const $list = $('<div>').dxList({
                    dataSource: e.component.option('dataSource'),
                    displayExpr: 'userName',
                    selectionMode: 'single',
                    onSelectionChanged: function (arg) {
                        console.log(arg.addedItems[0].id);
                        e.component.option('value', arg.addedItems[0].id);
                        e.component.close();
                    },
                });

                return $list;
            },
            onValueChanged: (e) => {
                BasicDropDown.option('displayValueFormatter', (value) => {
                    //console.log(value);
                    let val = value[0].split(' ');
                    if(val[0].includes('.')){
                        return val[1]
                    }
                    return val[0]
                });
                console.log('value changed to ' + e.value);
            },
            onInput: () => {
                console.log('data');
            },
            onOpened: () => {
                console.log('open');
            },
            onClosed: () => {
                console.log('closed');
            },
        })
        .dxDropDownBox('instance');
});
