$(() => {
    // Set default options for all dxCheckBox instances
    DevExpress.ui.dxDateBox.defaultOptions({
        device: { deviceType: 'desktop' }, // Target desktop devices
        options: {
            width: '60%', // Default width of 100px
            // Alternative: height: '50px' // Set default height if needed
        },
    });

    var dateBox = $('#dateBox')
        .dxDateBox({
            acceptCustomValue: true,
            accessKey: undefined,
            activeStateEnabled: true,
            adaptivityEnabled: false,
            applyButtonText: 'OK',
            applyValueMode: 'instantly',
            // calendarOptions: {},
            cancelButtonText: 'Cancel',
            dateOutOfRangeMessage: 'Value is out of range',
            dateSerializationFormat: 'yyyy-MM-dd', // Format for date serialization
            displayFormat: 'dd-MM-yyyy', // Format for displaying the date
            deferRendering: true,

            invalidDateMessage: 'Value must be a date or time',
            placeholder: 'select date',
            openOnFieldClick: true,
            pickerType: 'rollers', // Use calendar picker
            showClearButton: false,
            showDropDownButton: true,
            stylingMode: 'underlined',
            tabIndex: 1,
            todayButtonText: 'Today',
            type: 'date',
        })
        .dxDateBox('instance');

    var dateBox2 = $('#dateTimeBox')
        .dxDateBox({
            acceptCustomValue: true,
            accessKey: undefined,
            activeStateEnabled: true,
            adaptivityEnabled: false,
            applyButtonText: 'OK',
            applyValueMode: 'instantly',
            // calendarOptions: {},
            cancelButtonText: 'Cancel',
            dateOutOfRangeMessage: 'Value is out of range',
            dateSerializationFormat: 'yyyy-MM-ddTHH:mm:ss',
            deferRendering: true,
            displayFormat: 'EEEE, d of MMM, yyyy HH:mm',
            disabledDates: (args) => {
                var dayOfWeek = args.date.getDay();
                var dayOfMonth = args.date.getMonth();
                var isWeekend =
                    args.view == 'month' && (dayOfWeek == 0 || dayOfWeek == 6);
                var isDecember = args.view == 'year' && dayOfMonth == 11;

                return isWeekend || isDecember;
            },
            invalidDateMessage: 'Value must be a date or time',
            placeholder: 'select date time',
            openOnFieldClick: true,
            pickerType: 'calendar',
            showAnalogClock: true,
            showClearButton: false,
            stylingMode: 'filled',
            todayButtonText: 'Today',
            type: 'dateTime',
        })
        .dxDateBox('instance');

    var dateBox3 = $('#TimeBox')
        .dxDateBox({
            acceptCustomValue: true,
            activeStateEnabled: true,
            applyButtonText: 'OK',
            applyValueMode: 'instantly',
            // calendarOptions: {},
            cancelButtonText: 'Cancel',
            interval: 30,
            invalidDateMessage: 'Value must be a date or time',
            placeholder: 'select time',
            openOnFieldClick: true,
            pickerType: 'list',
            showAnalogClock: true,
            showDropDownButton: true,
            stylingMode: 'outlined',
            // tabIndex: 0,
            //todayButtonText: 'Today',
            type: 'time',
        })
        .dxDateBox('instance');
});
