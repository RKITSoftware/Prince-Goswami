var root;
var gridContainer;
var gridWidget;
var array;
var arrayStore;
var dataSource;

$(async function () {
    const root = $('#root');
    window.root = root;

    // Example function to fetch paged data from your API
    async function fetchData() {
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

    const gridContainer = $('<div>', { id: 'gridContainer' });
    window.gridContainer = gridContainer;

    const array = $.extend(true, [], await fetchData());
    window.array = array;

    const arrayStore = new DevExpress.data.ArrayStore({
        data: array,
        key: 'Id',
    });
    window.arrayStore = arrayStore;

    const dataSource = new DevExpress.data.DataSource({
        store: arrayStore,
    });
    window.dataSource = dataSource;

    function calculateAge(dob) {
        const birthDate = new Date(dob);
        const today = new Date();

        let age = today.getFullYear() - birthDate.getFullYear();
        const monthDiff = today.getMonth() - birthDate.getMonth();
        console.log(age);
        // Check if the birth month has not occurred yet this year, or it's the birth month and the day has not occurred yet.
        if (
            monthDiff < 0 ||
            (monthDiff === 0 && today.getDate() < birthDate.getDate())
        ) {
            age--;
        }

        return age;
    }

    const gridWidget = gridContainer
        .dxDataGrid({
            dataSource,
            showBorders: true,
            height: 'auto',
            //toolbar: {
            //    items: [,],
            //},
            rowDragging: {
                allowReordering: true,
                autoScroll: true,
                onReorder: function (e) {
                    array.splice(e.toIndex, 0, array.splice(e.fromIndex, 1)[0]);
                    console.log('row dragged and dropped', e);
                    e.component.refresh();
                },
                onRemove: function (e) {
                    console.log('row removed ', e);
                },
                container: '#draggable-row',
                cursorOffset: {
                    x: 100,
                    y: 100,
                },
                data: {
                    customInfo: 'Hello',
                },
                scrollSensitivity: 10,
                scrollSpeed: 100,
                showDragIcons: true,
                //boundary: "#gridContainer"
            },
            onToolbarPreparing: function (e) {
                let toolbars = e.toolbarOptions.items;
                toolbars.push(
                    {
                        location: 'after',
                        widget: 'dxButton',
                        options: {
                            icon: 'refresh',
                            onClick() {
                                gridWidget.refresh();
                            },
                        },
                    },
                    {
                        location: 'after',
                        widget: 'dxButton',
                        options: {
                            icon: 'clear',
                            onClick() {
                                var columns = gridWidget.getVisibleColumns();
                                columns.forEach(function (column) {
                                    if (column.sortOrder) {
                                        gridWidget.columnOption(
                                            column.index,
                                            'sortOrder',
                                            null,
                                        );
                                    }
                                });
                                gridWidget.clearFilter();
                                gridWidget.pageIndex(0);
                            },
                        },
                    },
                );
            },
            columns: [
                {
                    dataField: 'Id',
                    calculateFilterExpression: function (
                        filterValue,
                        selectedFilterOperation,
                        target,
                    ) {
                        if (selectedFilterOperation === '>=') {
                            return [this.dataField, '>=', filterValue];
                        }
                    },
                },
                {
                    caption: 'User',
                    columns: [
                        {
                            dataField: 'UserName',
                            alignment: 'right',
                            allowEditing: false, // t
                            allowExporting: false, // t
                            allowFiltering: false, // t
                            allowFixing: true, // t
                            allowGrouping: false, // t
                            allowHeaderFiltering: true, // t
                            allowHiding: false, // t
                            allowReordering: false, // t
                            allowResizing: false, // t
                            allowSearch: false, // t  searchPanel: t,
                            allowSorting: false, // t
                            autoExpandGroup: false, // t
                        },
                        {
                            caption: 'Address',
                            dataField: 'Address',
                            setCellValue: function (
                                newRowData,
                                value,
                                rowData,
                            ) {
                                rowData.fullName = value; // Update the age when the custom input changes
                                $.extend(true, newRowData, rowData);
                            },
                            cellTemplate: function (container, options) {
                                container.append(
                                    $('<span>', {
                                        class: 'redbg font-white',
                                        text: options.value,
                                    }),
                                );
                            },
                        },
                    ],
                },

                {
                    dataField: 'BirthDate',
                    //groupIndex: 0,
                    calculateGroupValue: function (rowData) {
                        // console.log(rowData)
                        const date = new Date(rowData.BirthDate);
                        const dayOfWeek = date.getDay();
                        if (dayOfWeek === 0 || dayOfWeek === 6) {
                            // 0 is Sunday, 6 is Saturday
                            return 'Weekend';
                        } else {
                            return 'Weekday';
                        }
                    },
                },
                {
                    caption: 'Age',
                    calculateCellValue: function (rowData) {
                        // console.log(calculateAge(rowData.BirthDate))
                        return calculateAge(rowData.BirthDate);
                    },
                },
                {
                    dataField: 'Salary',
                    customizeText(cellInfo) {
                        return '$' + cellInfo.value;
                    },
                    //calculateCellValue: function (rowData) {
                    //    return "$" + rowData.salary;
                    //}
                },
                {
                    caption: 'Actions',
                    width: 'auto',
                    type: 'buttons',
                    buttons: [
                        'edit',
                        'delete',
                        {
                            hint: 'Clone',
                            icon: 'copy',
                            visible: function (e) {
                                // console.log("visible", e);
                                return !e.row.isEditing;
                            },
                            disabled: function (e) {
                                return e.row.data.position != 'Engineer';
                            },
                            onClick: function (e) {
                                const clonedRow = $.extend(
                                    true,
                                    {},
                                    e.row.data,
                                    { id: nextIncId++ },
                                );

                                array.splice(e.row.rowIndex + 1, 0, clonedRow);
                                gridWidget.refresh(true);
                                e.event.preventDefault();
                            },
                        },
                    ],
                },
            ],
            stateStoring: {
                enabled: true,
                type: 'localStorage',
                storageKey: 'myDataGridState',
            },
            allowColumnReordering: true,
            allowColumnResizing: true,
            searchPanel: {
                visible: true,
            },
            sorting: {
                mode: 'multiple',
            },
            grouping: {
                contextMenuEnabled: true,
            },
            groupPanel: {
                visible: true,
            },
            columnFixing: {
                enabled: true,
            },
            editing: {
                allowUpdating: true,
                //allowDeleting: true,
            },
            filterRow: {
                visible: true,
            },
            headerFilter: {
                visible: true,
            },
            onCellPrepared: function (e) {
                if (
                    e.rowType == 'data' &&
                    e.column.caption == 'Age' &&
                    e.value < 50
                ) {
                    // console.log(e);
                    e.cellElement.addClass('redbg font-white');
                }
            },
        })
        .dxDataGrid('instance');
    window.gridWidget = gridWidget;

    root.append([gridContainer]);
});
