var array;
var arrayStore;
var dataSource;
var dataGrid;

$(async function () {
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
            // console.log(data);
            return data;
        } catch (error) {
            console.error('Error fetching data:', error);
            throw error;
        }
    }
    
    // array - data
    const array = await fetchData();

    // array store
    const arrayStore = new DevExpress.data.ArrayStore({
        data: array,
        key: 'Id',
    });

    // data source
    const dataSource = new DevExpress.data.DataSource({
        store: arrayStore,
    });
 
    DevExpress.localization.loadMessages({
        en: {
            currency: {
                style: 'currency',
                currency: 'USD',
            },
        },
    });

    DevExpress.localization.locale('en');

    function getOrderDay(rowData) {
        return new Date(rowData.BirthDate).getDay();
    }

    // dx data grid
    const dataGrid = $('#dataGrid')
        .dxDataGrid({
            dataSource,
            filterPanel: {
                visible: true,
            },
            filterBuilder: {
                enabled: true,
                customOperations: [
                    {
                        name: 'weekends',
                        caption: 'Weekends',
                        dataTypes: ['date'],
                        icon: 'check',
                        hasValue: false,
                        calculateFilterExpression(value, fields) {
                            return [
                                [getOrderDay, '=', 0],
                                'or',
                                [getOrderDay, '=', 6],
                            ];
                        },
                    },
                    {
                        name: 'containsNot',
                        caption: 'Custom Contains not',
                        icon: 'check',
                        calculateFilterExpression: function (
                            filterValue,
                            selectedFilterOperation,
                        ) {
                            if (selectedFilterOperation === 'containsNot') {
                                return [
                                    '!',
                                    ['UserName', 'contains', , filterValue],
                                ];
                            }
                            return [
                                'UserName',
                                selectedFilterOperation,
                                filterValue,
                            ];
                        },
                    },
                ],
                allowHierarchicalFields: true,
            },
            filterSyncEnabled: true,
            sorting: {
                mode: 'multiple',
                showSortIndexes: false,
            },
            grouping: {
                allowColumnDragging: true,
                autoExpandAll: false,
                contextMenuEnabled: true,
                expandMode: 'rowClick',
                calculateGroupValue: function (data) {
                    console.log(data)
                    const birthDate = new Date(data.BirthDate);
                    return birthDate.getFullYear();
                },
            },
            groupPanel: {
                allowColumnDragging: true,
                emptyPanelText: 'Drag Column here to group',
                visible: true,
            },
            searchPanel: {
                visible: true,
                width: 240,
                placeholder: 'Search...',
            },
            columns: [
                {
                    dataField: 'Id',
                    dataType: 'number',
                    allowEditing: false,
                    allowFiltering: true, // false
                    allowHeaderFiltering: true,
                    filterType: 'exclude',
                    filterOperations: ['='],
                    alignment: 'left',
                    width: 100,
                    sortOrder: 'asc',
                    sortIndex: 0,
                    headerFilter: {
                        visible: true,
                        searchTimeout: 100,
                        groupInterval: 100,
                        texts: {},
                        height: '400px',
                        width: '300px',
                    },
                },
                {
                    dataField: 'UserName',
                    //sortOrder: "asc",
                    allowFiltering: true,
                    filterOperations: ['contains', 'containsNot', '='],
                },
                {
                    dataField: 'BirthDate',
                    dataType: 'date',
                    groupIndex : 0  
                },
                {
                    dataField: 'Gender',
                    dataType: 'string',
                },
                {
                    dataField: 'Salary',
                    caption: 'Salary',
                    dataType: 'number',
                    cellTemplate: function (element, data) {
                        element.append(
                            $('<span>', {
                                text: `${DevExpress.localization.formatNumber(
                                    data.value,
                                    {
                                        type: 'currency',
                                    },
                                )}`,
                            }),
                        );
                    },
                    headerFilter: {
                        visible: true,
                        allowSearch: false,
                        dataSource: [
                            {
                                text: 'Less than $10000',
                                value: ['', '<', 10000],
                            },
                            {
                                text: '$10000 - $49999',
                                value: [
                                    ['Salary', '>=', 10000],
                                    ['Salary', '<', 10000],
                                ],
                            },
                            {
                                text: '$50000 - $100000',
                                value: [
                                    ['Salary', '>=', 50000],
                                    ['Salary', '<=', 100000],
                                ],
                            },
                            {
                                text: 'Above than $100000',
                                value: ['Salary', '>', 100000],
                            },
                        ],
                    },
                },
            ],
            headerFilter: {
                visible: true,
                searchTimeout: 100,
                texts: {},
                height: '400px',
                width: '300px',
            },
            filterRow: {
                visible: true,
                applyFilter: 'auto', // onClick
            },
        })
        .dxDataGrid('instance');
});

