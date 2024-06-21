$(() => {
    var addressInstance = $('#Address')
        .dxTextArea({
            name: 'Description',
            placeholder: 'enter description ',
            width: '60%',
            height: '90',
            displayExpr: 'title',

            autoResizeEnabled: false,
        })
        .dxTextArea('instance');

    var addressInstance = $('#Description')
        .dxTextArea({
            name: 'Description',
            value: 'longText',
            placeholder: 'enter description ',
            width: '60%',
            maxHeight : 160,
            minHeight : 10,
            maxWidth : 160,
            autoResizeEnabled: true,
        })
        .dxTextArea('instance');
});
