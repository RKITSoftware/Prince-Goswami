var array;
var arrayStore;
var dataSource;
var dataGrid;

$(async function () {
    // build ui template
    const root = $('#root');
    const container = $('<div>', { id: 'container' });
    // Example function to fetch paged data from your API
    async function fetchData(page = 1, pageSize = 10) {
        const url = `https://6666d488a2f8516ff7a5223a.mockapi.io/users`;

        try {
            const response = await fetch(url, {
                method: 'GET',
                headers: { 'content-type': 'application/json' },
            });

            if (!response.ok) {
                throw new Error('Failed to fetch data');
            }
            const data = await response.json();
            console.log(data);
            return data;
        } catch (error) {
            console.error('Error fetching data:', error);
            throw error;
        }
    }

    // array - data
    const array = await fetchData();
    window.array = array;

    // array store
    const arrayStore = new DevExpress.data.ArrayStore({
        data: array,
        key: 'id',
    });
    window.arrayStore = arrayStore;

    // data source
    const dataSource = new DevExpress.data.DataSource({
        store: arrayStore,
    });
    window.dataSource = dataSource;

    const genderArray = [ 'male','female'
        // { Name: 'male' },
        // { Name: 'female' },
    ];

    let currentEditingRow;
    // dx data grid
    const dataGrid = container
        .dxDataGrid({
            dataSource,
            columnAutoWidth: true,
            columns: [
                {
                    dataField: 'id',
                    allowEditing: false,
                },
                {
                    dataField: 'userName',
                    sortOrder: 'asc',
                    validationRules: [
                        {
                            type: 'required',
                            message: 'User Name is required',
                        },
                    ],
                },
                {
                    dataField: 'BirthDate',
                    dataType: 'date',
                },
                {
                    dataField: 'age',
                    caption: 'Salary',
                    dataType: 'number',
                },
                {
                    dataField: 'Gender',
                    caption : 'Gender',
                    lookup: {
                        dataSource: genderArray,
                        // valueExpr: 'Name',
                        // displayExpr: 'Name',
                        allowClearing: true,
                    },
                },
            ],
            editing: {
                newRowPosition: 'top', // Ensure new row is added at the top
                mode: 'popup',
                allowUpdating: true,
                allowDeleting: true,
                allowAdding: true,
                confirmDelete: true,
                editColumnName: null, // cell or batch edit, it contains col name
                refreshMode: 'reload',
                selectTextOnEditStart: true,
                startEditAction: 'dblClick', // click
                useIcons: true,
                texts: {
                    addRow: 'Add entry', // hint for add button (editing.allowAdding: true)
                    cancelAllChanges: 'Undo all changes', // hint for discard button (editing.mode: batch)
                    cancelRowChanges: 'Undo row changes', // hint for row discard button (editing.allowUpdating: true and editing. mode: row)
                    confirmDeleteMessage:
                        'This will delete the record, do you want to proceed?',
                    confirmDeleteTitle: 'Delete',
                    deleteRow: 'Delete this row', // hint for delete button on row (editing.allowDeleting: true)
                    editRow: 'Edit this row', // hint for edit button on row (editing.allowUpdating: true)
                    saveAllChanges: 'Save every changes', // hint for save changes button (editing.mode: batch)
                    saveRowChanges: 'Save row changes', // hint for save button of row (editing.allowUpdating: ture and editing.mode: row)
                    undeleteRow: 'undelete', // hit for undelete button on row (allowDeleting: true and editing.mode: batch)
                    validationCancelChanges: 'Validation cancel chenges', // hint for  button that cancels changes in a cell
                },
                popup: {
                    title: 'Popup - add or edit',
                    showTitle: true,
                    resizeEnabled: true,
                    toolbarItems: [
                        {
                            locateInMenu: 'always',
                            zwidget: 'dxButton',
                            toolbar: 'top',
                            options: {
                                text: 'More info',
                                onClick(e) {
                                    const message = `More info about ${currentEditingRow.firstName} ${currentEditingRow.lastName}`;
                                    DevExpress.ui.notify(
                                        {
                                            message,
                                            position: {
                                                my: 'center top',
                                                at: 'center top',
                                            },
                                        },
                                        'success',
                                        3000,
                                    );
                                },
                            },
                        },
                        {
                            widget: 'dxButton',
                            toolbar: 'bottom',
                            location: 'before',
                            options: {
                                text: 'Save',
                                onClick: function (e) {
                                    dataGrid.saveEditData();
                                },
                            },
                        },
                        {
                            widget: 'dxButton',
                            toolbar: 'bottom',
                            location: 'after',
                            options: {
                                text: 'Cancel',
                                onClick: function (e) {
                                    dataGrid.cancelEditData();
                                },
                            },
                        },
                    ],
                },
                form: {},
            },
            onEditingStart: function (e) {
                currentEditingRow = e.data;
                console.log(e);
            },
            repaintChangesOnly: true,
        })
        .dxDataGrid('instance');
    window.dataGrid = dataGrid;

    const radioGroupArray = [
        { text: 'Row' },
        { text: 'Cell' },
        { text: 'Batch' },
        { text: 'Popup' },
        { text: 'Form' },
    ];

    const radioGroupMode = $('<div>').dxRadioGroup({
        items: radioGroupArray,
        value: radioGroupArray[3],
        layout: 'horizontal',
        onValueChanged: function (e) {
            dataGrid.option('editing.mode', e.value.text.toLowerCase());
        },
    });

    const radioGroupRMArray = [
        { text: 'reload' },
        { text: 'reshape' },
        { text: 'repaint' },
    ];
    const radioGroupRefreshMode = $('<div>').dxRadioGroup({
        items: radioGroupRMArray,
        value: radioGroupRMArray[0],
        layout: 'horizontal',
        onValueChanged: function (e) {
            dataGrid.option('editing.refreshMode', e.value.text.toLowerCase());
        },
    });

    const buttonEmptyUnderlyingArray = $('<div>').dxButton({
        text: 'Clear underlying array',
        label: 'Empty underlying array?',
        onClick: function () {
            while (window.array.length > 0) {
                window.array.pop();
            }
        },
    });

    root.append([
        radioGroupMode,
        radioGroupRefreshMode,
        buttonEmptyUnderlyingArray,
        container,
    ]);
});
