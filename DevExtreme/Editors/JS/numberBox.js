$(function () {
    // Initialize the NumberBox with various options
    const numberBox = $('#number-box')
        .dxNumberBox({
            accessKey: 'a',
            activeStateEnabled: true,
            buttons: [
                {
                    name: 'increment',
                    location: 'before',
                    options: {
                        icon: 'plus',
                        stylingMode: 'text',
                        onClick: function () {
                            var currentValue = numberBox.option('value');
                            numberBox.option('value', currentValue + 10);
                        },
                    },
                },
                {
                    name: 'clear',
                    location: 'after',
                    options: {
                        icon: 'clear',
                        stylingMode: 'text',
                        onClick: function () {
                            numberBox.option('value', null);
                        },
                    },
                },
            ],
            showSpinButtons: true,
            showClearButton: true,
            placeholder: 'Enter a number',
            width: 200,
            useLargeSpinButtons: true,
            showDropDownButton: true,
            // onChange: (e) => {
            focusStateEnabled: true,
            format: '#,##0.##',
            hint: 'use Alt + A to focus',
            hoverStateEnabled: true, // false to hide hint
            inputAttr: {
                name: 'number Box',
            },
            invalidValueMessage: 'The value must be a number',
            max: 100,
            min: 0,
            mode: 'number',
            value: null,
            step: 2,
            text: '123',
            onValueChanged: function (e) {
                if (e.value >= numberBox.option('max'))
                    logEvent('Maximum limit reached');
                else logEvent('Value Chnaged', e.value);
            },
            onFocusIn: function () {
                logEvent('Focus in');
            },
            onFocusOut: function () {
                logEvent('Focus out');
            },
            onPaste: () => {
                if (!numberBox.options('isValid')) {
                    logEvent('Invalid input');
                }
            },
            // onKeyDown: () => {
            //     logEvent("Key down", e.event.key);
            // },
            // onKeyUp: () => {
            //     logEvent("Key up", e.event.key);
            // },
            onInitialized: () => {
                logEvent('Initialized');
            },
            onContentReady: () => {
                logEvent('content Ready');
            },
            onDisposing: () => {
                logEvent('Disposing');
            },
            onCut: () => {
                var currentValue = $('#number-box')
                    .dxNumberBox('instance')
                    .option('value');
                logEvent(currentValue + ' was cut and added to clipboard ');
            },
            onCopy: () => {
                var currentValue = $('#number-box')
                    .dxNumberBox('instance')
                    .option('value');
                logEvent(currentValue + ' was added to clipboard ');
            },
            onEnterKey: function (e) {
                logEvent('Value Submitted', e.value);
            },
        })
        .dxNumberBox('instance');

    var isEnable = true;

    // Get value button click event
    $('#get-value').click(function () {
        if (isEnable) {
            var value = numberBox.option('value');
            logEvent('Get value', value);
        } else {
            logEvent('number Box is disabled');
        }
    });

    // Set value button click event
    $('#set-value').click(function () {
        if (isEnable) {
            numberBox.option('value', 50);
            logEvent('Set value to 50');
        } else {
            logEvent('number Box is disabled');
        }
    });

    // Reset button click event
    $('#reset').click(function () {
        if (isEnable) {
            numberBox.reset();
            logEvent('Reset to default values');
        } else {
            logEvent('number Box is disabled');
        }
    });

    // Disable button click event
    $('#disable').click(function () {
        if (isEnable) {
            isEnable = false;
            numberBox.option('disabled', true);
            logEvent('NumberBox disabled');
        } else {
            logEvent('number Box is disabled');
        }
    });

    // Enable button click event
    $('#enable').click(function () {
        if (!isEnable) {
            isEnable = true;
            numberBox.option('disabled', false);
            logEvent('NumberBox enabled');
        } else {
            logEvent('number Box is allready enabled');
        }
    });

    var isReadOnly = false;
    // Enable button click event
    $('#readOnly').click(function () {
        if (!isReadOnly) {
            isReadOnly = true;
            logEvent('NumberBox changed to readOnly');
            numberBox.option('readOnly', true);
        } else {
            isReadOnly = false;
            numberBox.option('readOnly', false);
            logEvent('NumberBox changed to normal');
        }
    });

    function logEvent(eventName, value) {
        var log = $('#log');
        var newLog = $('<div>').text(
            eventName + (value !== undefined ? ': ' + value : ''),
        );
        log.prepend(newLog);
    }
});
