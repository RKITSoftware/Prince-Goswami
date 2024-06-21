$(function () {
    DevExpress.ui.dxTextArea.defaultOptions({
        device: { deviceType: 'desktop' },
        options: {
            // Here go the TextBox properties
            width: '60%',
        },
    });

    const dataItems = [
        { text: 'Low', color: 'grey' },
        { text: 'Normal', color: 'green' },
        { text: 'Urgent', color: 'yellow' },
        { text: 'High', color: 'red' },
    ];

    let basicRadioGroupInstance = $('#basicRadioGroup')
        .dxRadioGroup({
            accessKey: 'a',
            activeStateEnabled: true,
            dataSource: dataItems,
            disabled: false,
            displayExpr: 'text',
            elementAttr: {},
            focusStateEnabled: true,
            height: undefined,
            hoverStateEnabled: true,
            isValid: true,
            layout: 'horizontal',
            name: '',
            onContentReady: function (e) {
                console.log('content ready');
            },
            onInitialized: function (e) {
                console.log('content initialized');
            },
            onValueChanged: function (e) {
                console.log(e.value);
            },
            itemTemplate(itemData, _, itemElement) {
                return '<div style=\"background-color :'+itemData.color+'\" >'+itemData.text+'</div>'
            },            
            readOnly: false,
            rtlEnabled: true,
            value: dataItems[0],
            valueExpr: 'text',
            visible: true,
            width: undefined,
        })
        .dxRadioGroup('instance');
});
