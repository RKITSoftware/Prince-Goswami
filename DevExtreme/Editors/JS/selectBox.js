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

    DevExpress.ui.dxSelectBox.defaultOptions({
        device: { deviceType: 'desktop' },
        options: {
            width: '40%',
            dataSource: await fetchData(),
        },
    });

    let dataArray = await fetchData();
    var selectBox = $('#selectBox')
        .dxSelectBox({
            dataSource: new DevExpress.data.DataSource({
                store: dataArray,
                key: 'id',
                group: 'gender',
            }),
            acceptCustomValue: true, // if true then it will allow value provided by user
            name: 'userList',
            placeHolder: 'select user',
            deferRendering: true, // If false, the content is rendered immediately.
            displayExpr: 'userName',
            valueExpr: 'id',
            grouped: true,
            openOnFieldClick: false,
            searchEnabled: true,
            searchExpr: ['userName', 'id'],
            searchMode: 'contains', // startsWith
            searchTimeout: 100, // default : 500
            showClearButton: true,
            showDropDownButton: true,
            showSelectionControls: true,
            showDataBeforeSearch: true,
            spellcheck: true,
            wrapItemText: true, // if text exceeds the drop-down list width should be wrapped.
            useItemTextAsTitle: true,
            groupTemplate(data) {
                return $(
                    `<div class='custom-icon'><span class='dx-icon-user'></span> ${data.key}</div>`,
                );
            },
            noDataText: 'no data found',
            onValueChanged: (e) => {
                console.log('value changed to ' + e.value);
                selectBox.blur();
                selectBox.close();
                itemsBox.focus();
                itemsBox.open();
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
            onSelectionChanged: () => {
                console.log('selection changed');
            },
        })
        .dxSelectBox('instance');

    var itemsBox = $('#itemsBox')
        .dxSelectBox({
            items: await fetchData(),
            value: dataArray[1].id,
            displayExpr: 'userName',
            valueExpr: 'id',
            acceptCustomValue: true, // if true then it will allow value provided by user
            name: 'userName',
            placeHolder: 'select avatar',
            onCustomItemCreating: (e) => {
                let newItem = {
                    id: dataArray.length + 1,
                    userName: e.text,
                    avatar: 'avatar',
                    gender: 'male',
                };
                dataArray.push(newItem);
                console.log(e);
                e.component.option('dataSource', dataArray);
                e.customItem = newItem;
            },
            fieldTemplate(data, container) {
                const result = $(
                    `<div class='custom-item' style = "display : flex; flex-direction : row; margin : 10px"><img alt='Product name' width="20%" style = " border-radius : 100%" src='${
                        data ? data.avatar : ''
                    }' /><div class='product-name'></div></div>`,
                );
                result.find('.product-name').dxTextBox({
                    value: data && data.userName,
                    readOnly: true,
                    inputAttr: { 'aria-label': 'Name' },
                });
                container.append(result);
            },
            itemTemplate(data) {
                return `<div style="display : flex; flex-direction : row; justify-content : space-around"><img width="50px" height="auto" alt='Product name' src='${data.avatar}' /><div class='product-name'>${data.userName}</div></div>`;
            },
            onValueChanged: function () {
            },
        })
        .dxSelectBox('instance');

    var btnGetValue = $('#getValue')
        .dxButton({
            text: 'get Value',
            onClick: () => {
                let val = selectBox.option('displayValue');
                let selectedItem = selectBox.option('selectedItem');

                console.log(val);
                console.log(selectedItem);
            },
        })
        .dxButton('instance');
        $('#content').html( $('#itemsBox').dxSelectBox('instance').content());

});
