$(async () => {
    // Example function to fetch paged data from your API
    async function fetchPagedData(page = 1, pageSize = 10) {
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

    const dataGrid = $('#dataGrid')
        .dxDataGrid({
            dataSource: await fetchPagedData(),
            keyExpr: 'id',
            showBorders: true,
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
                    caption: 'Gender',
                   
                },
            ],
            loadPanel: {
                enabled: true,
            },
            scrolling: {
                mode: 'virtual',
            },
            sorting: {
                mode: 'none',
            },
            grouping: {
                autoExpandAll: false,
                //allowCollapsing: false,
                allowColumnDragging : true,
                contextMenuEnabled: true,
                columns: ["Gender"],
                expandMode: 'rowClick', // buttonClick
                texts: {
                    groupByThisColumn: 'GBTC',
                    groupContinuedMessage: 'GCM',
                    groupContinuesMessage: 'GCsM',
                    ungroup: 'ungroup',
                    ungroupAll: 'ungroup all',
                },
            },
            groupPanel: {
                allowColumnDragging: true,
                emptyPanelText: 'Drag Column here to group',
                visible: true,
            },
            paging: {
                pageSize: 5,
            },
            onContentReady(e) {
                e.component.option('loadPanel.enabled', false);
            },
            onInitNewRow(data) {
                console.log(data);
            },
        })
        .dxDataGrid('instance');
});
