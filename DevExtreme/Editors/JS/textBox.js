$(() => {
    var cardNumberInstance = $('#CardNumber')
        .dxTextBox({
            name: 'CardNumber',
            placeholder: 'enter card number ',
            showMaskMode: 'onFocus',
            mask: '0000-0000-0000-0000',
            maskChar: '0',
            width: '60%',
            maskInvalidMessage: 'invalid Card Number Format',
        })
        .dxTextBox('instance');

    var expDateInstance = $('#ExpiryDate')
        .dxTextBox({
            name: 'Expiry Date',
            placeholder: 'Select Expiry Date',
            mask: 'M0/00',
            showMaskMode: 'onFocus',
            maskChar: '0',
            maskRules: {
                M: function (char) {
                    return char == 0 || char == 1; // Allow months from 01 to 12
                },
            },
            width: '60%',
            maskInvalidMessage: 'Invalid Expiry Date Format',
        })
        .dxTextBox('instance');
});
