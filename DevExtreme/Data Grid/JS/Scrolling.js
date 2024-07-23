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
            customizeColumns(columns) {
                columns[0].width = 70;
            },
            loadPanel: {
              enabled: true,
            },
            scrolling: {
              mode: 'virtual',
            },
            sorting: {
              mode: 'none',
            },
            paging: {
              pageSize: 5,
            },
            onContentReady(e) {
              e.component.option('loadPanel.enabled', false);
            },
            onInitNewRow(data){
              console.log(data)
            }
        })
        .dxDataGrid('instance');

    $('#scrollMode').dxSelectBox({
        items: [
            { text: "Virtual Scrolling", value: 'virtual' },
            { text: "Infinite Scrolling", value: 'infinite' },
        ],
        displayExpr: 'text',
        inputAttr: { 'aria-label': 'Display Mode' },
        valueExpr: 'value',
        value: 'virtual',
        onValueChanged(data) {
            dataGrid.option('scrolling.mode', data.value);
            //navButtons.option('disabled', data.value === 'compact');
        },
    });
    // $('#showInfo')
    //     .dxCheckBox({
    //         text: 'Show Info Text',
    //         value: true,
    //         onValueChanged(data) {
    //             dataGrid.option('pager.showInfo', data.value);
    //         },
    //     })
    //     .dxCheckBox('instance');
    // const navButtons = $('#showNavButtons')
    //     .dxCheckBox({
    //         text: 'Show Navigation Buttons',
    //         value: true,
    //         onValueChanged(data) {
    //             dataGrid.option('pager.showNavigationButtons', data.value);
    //         },
    //     })
    //     .dxCheckBox('instance');
});
