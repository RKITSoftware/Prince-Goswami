var root;
var gridContainer;
var gridWidget;
var array;
var arrayStore;
var dataSource;

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

    const array = await fetchData();
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

    const gridWidget = $('#dataGrid')
        .dxDataGrid({
            dataSource,
            columns: [
                {
                    dataField: 'Id',
                    allowSorting: false, // t
                },
                {
                    dataField: 'UserName',
                    sortingMethod: function (value1, value2) {
                        if (!value1 && value2) return -1;
                        if (!value1 && !value2) return 0;
                        if (value1 && !value2) return 1;

                        return value1.localeCompare(value2);
                    },
                    sortOrder: 'asc', // ↑
                    sortIndex: 0,
                },
                {
                    dataField: 'BirthDate',
                    allowSorting: true,
                },
                {
                    dataField: 'Salary',
                    allowSorting: true,
                },
            ],
            sorting: {
                ascendingText: 'Ascending order',
                descendingText: 'Descending order',
                clearText: 'Clear',
                showSortIndex: true,
                mode: 'multiple',
            },
            selection: {
                allowSelectAll: true,
                mode: 'multiple',
                selectAllMode: 'allPages',
                deferred: false,
            },
        })
        .dxDataGrid('instance');
});
