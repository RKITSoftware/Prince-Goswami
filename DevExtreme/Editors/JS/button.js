
$(function () {
    DevExpress.ui.dxButton.defaultOptions({
        options: {
            height : '50px',
            elementAttr: {
                style: 'margin : 0 0 0 10px'
            },
        },
    });

    $('#basicButton').dxButton({
        text: 'Basic',
        hint: "Press 'Alt + A' to click this button",
        activeStateEnabled: true,
        focusStateEnabled: true, // if false it wont let you focus it through keyboard
       
        //height : '10%',
        hoverStateEnabled: true, // if false it wont show hint on hover
        icon: 'check',
        type: 'success',
        rtlEnabled: true,
        visible: true, // flase to hide
        width: 150,
        height: 50,
        tabIndex: 2,
        onContentReady: () => console.log('content Ready...!!!'),
        onInitialized: () => console.log('content Initialized...!!!'),
    });

    var initializeDisposeButton = () => {
        return $('#disposeButton').dxButton({
            text: 'ready to be disposed',
            type: 'danger',
            tabIndex: 1,
            stylingMode: 'outlined',
            icon: 'trash',
            rtlEnabled: true,
            width : '250px',
            template: (data, index) => {
                var button = $('<div>')
                    .append(
                        $('<span>').addClass('dx-icon dx-icon-' + data.icon),
                    )
                    .append(
                        $('<span>').text(' ' + data.text.toUpperCase() + ' '),
                    )
                    .append(
                        $('<span>').addClass('dx-icon dx-icon-' + data.icon),
                    );
                index.append(button);
            },
            useSubmitBehavior: true, //If the onClick event handler is specified, it is executed before validation and form submission.
            onClick: () => {
                $('#disposeButton').dxButton('dispose');
                console.log('button disposed');
                buttonInstance.focus();
            },
        });
    };

    var buttonInstance = $('#basicButton').dxButton('instance');
    var disposeInstance = initializeDisposeButton().    dxButton('instance');
    buttonInstance.on('contentReady', () =>
        console.log('content Ready from handler...!!!'),
    );
    buttonInstance.on('initialized', () =>
        console.log('content Initialized from handler...!!!'),
    );

    // buttonInstance.on('optionChanged', () =>
    //     console.log('content Option change from handler...!!!'),
    // );

    disposeInstance.on('disposing', () =>
        console.log('content Desposing from handler...!!!'),
    );

    buttonInstance.on('click', () => {
        console.log('basic button called');
        //disposeInstance.repaint();
        disposeInstance = DevExpress.ui.dxButton.getInstance(initializeDisposeButton());
        disposeInstance.repaint();
        disposeInstance.focus();
        console.log (disposeInstance.element()); //returns root element
    });

    CB.registerKeyHandler('space', (e) => {
        var BB = $('#basicButton').dxCheckBox('instance');
        console.log(BB.value)
        let cbVal = BB.option("value");
        cbVal.value = !cbVal});

});
