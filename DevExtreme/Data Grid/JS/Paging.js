$(async () => {
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
            console.log(data)
            return data;
        } catch (error) {
            console.error('Error fetching data:', error);
            throw error;
        }
    }

    const dataGrid = $('#dataGrid').dxDataGrid({
      dataSource: await fetchData(),
      keyExpr: 'id',
      showBorders: true,
      customizeColumns(columns) {
        columns[0].width = 70;
      },
      scrolling: {
        rowRenderingMode: 'virtual',
      },
      paging: {
        pageSize: 10,
      },
      pager: {
        visible: true,
        allowedPageSizes: [5, 10, 'all'],
        showPageSizeSelector: true,
        showInfo: true,
        showNavigationButtons: true,
      },
    }).dxDataGrid('instance');
  
    $('#displayMode').dxSelectBox({
      items: [{ text: "Display Mode 'full'", value: 'full' }, { text: "Display Mode 'compact'", value: 'compact' }],
      displayExpr: 'text',
      inputAttr: { 'aria-label': 'Display Mode' },
      valueExpr: 'value',
      value: 'full',
      onValueChanged(data) {
        dataGrid.option('pager.displayMode', data.value);
        navButtons.option('disabled', data.value === 'compact');
      },
    });
    $('#showPageSizes').dxCheckBox({
      text: 'Show Page Size Selector',
      value: true,
      onValueChanged(data) {
        dataGrid.option('pager.showPageSizeSelector', data.value);
      },
    });
    $('#showInfo').dxCheckBox({
      text: 'Show Info Text',
      value: true,
      onValueChanged(data) {
        dataGrid.option('pager.showInfo', data.value);
      },
    }).dxCheckBox('instance');
    const navButtons = $('#showNavButtons').dxCheckBox({
      text: 'Show Navigation Buttons',
      value: true,
      onValueChanged(data) {
        dataGrid.option('pager.showNavigationButtons', data.value);
      },
    }).dxCheckBox('instance');
});
