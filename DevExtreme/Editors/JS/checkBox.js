$(() => {
    // Set default options for all dxCheckBox instances
    DevExpress.ui.dxCheckBox.defaultOptions({
        device: { deviceType: 'desktop' }, // Target desktop devices
        options: {
            width: '100px', // Default width of 100px
            // Alternative: height: '50px' // Set default height if needed
        },
    });

    // Initialize checkbox for 'Checked' state
    $('#checkBox').dxCheckBox({
        value: undefined, // Initial value is undefined (Alternative: value: true/false/null)
        label: "use 'ALT + A' to access this checkBox", // Access key label
        text: 'undefined', // Initial text displayed
        accessKey: 'A', // Keyboard shortcut (Alternative: accessKey: 'B')
        onValueChanged: (e) => {
            let CB = $('#checkBox').dxCheckBox('instance');
            CB.option('text', CB.option('value') ? 'True' : 'False'); // Update text based on value
        },
        onHoverStateDisabled: false, // Disable hover state (Alternative: onHoverStateDisabled: true)
        elementAttr: {
            'aria-label': 'Checked', // Accessibility label
        },
    });

    // Register key handler for the 'space' key
    $('#checkBox')
        .dxCheckBox('instance')
        .registerKeyHandler('space', (e) => {
            let CB = $('#checkBox').dxCheckBox('instance');
            console.log(CB.option('value'));
            CB.option('value', !CB.option('value')); // Toggle value on space key press
        });

    // Initialize disabled checkbox
    const disabledCheckbox = $('#disabled')
        .dxCheckBox({
            value: null, // No initial value
            focusStateEnabled: false, // Disable focus state (Alternative: focusStateEnabled: true)
            disabled: true, // Checkbox is disabled
            elementAttr: {
                'aria-label': 'Disabled', // Accessibility label
            },
        })
        .dxCheckBox('instance'); // Get instance of the checkbox

    // Enable Right-To-Left state checkbox
    $('#rtl').dxCheck;
    $('#rtl').dxCheckBox({
        value: true, // Initial value is true (Alternative: value: false)
        text: 'leftToRight', // Initial text displayed
        tabIndex: 1, // Tab index (Alternative: tabIndex: 0)
        hoverStateEnabled: false, // Disable hover state (Alternative: hoverStateEnabled: true)
        onValueChanged: (e) => {
            $('#rtl')
                .dxCheckBox('instance')
                .option({
                    rtlEnabled: e.value, // Enable/disable RTL based on value
                    text: e.value ? 'leftToRight' : 'rightToLeft', // Update text based on value
                });
            var cb = $('#checkBox').dxCheckBox('instance');
            console.log(cb); // Log checkbox instance
        },
    });

    // Checkbox with event handlers for initialization, content ready, and disposing
    $('#onDesposing').dxCheckBox({
        hint: 'click it to Dispose it', // Tooltip hint
        onContentReady: () => console.log('Content Ready...!!!'), // Log content ready
        onInitialized: () => console.log('Content Initialized...!!!'), // Log initialization
        onDisposing: () => console.log('Content Disposed...!!!'), // Log disposal
    });
    $('#onDesposing').dxCheckBox('option'); // Get options (not used effectively here)

    // Function to handle value change
    var ValueChanged = (e) => {
        $('#onDesposing').dxCheckBox('dispose'); // Dispose the checkbox
        console.log('value change'); // Log value change
    };

    var isClicked = true; // Track button click state
    var checkBox = $('#onDesposing').dxCheckBox('instance'); // Get instance of the checkbox

    // Button to toggle disposing of the checkbox
    $('#desposeButton').dxButton({
        text: 'on checkbox Disposing', // Initial button text
        length: 100, // Button length (should be 'width' instead of 'length')
        onClick: () => {
            var disposeButton = $('#desposeButton').dxButton('instance');
            if (isClicked) {
                disposeButton.option('text', 'off checkbox Disposing'); // Update button text
                checkBox.on('valueChanged', ValueChanged); // Attach event handler
                isClicked = false; // Update state
                console.log(isClicked); // Log state
                return;
            }
            checkBox.repaint(); // Repaint the checkbox (Alternative: checkBox.reset())
            disposeButton.option('text', 'on checkbox Disposing'); // Update button text
            checkBox.off('valueChanged'); // Detach event handler
            isClicked = true; // Update state
            console.log(isClicked); // Log state
        },
    });
});
