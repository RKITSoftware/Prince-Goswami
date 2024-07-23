$(() => {
    var count = 1;
    // Function to initialize buttons
    const initButton = (id, text, page) => {
        $(`#${id}`).dxButton({
            text: text,
            tabIndex : count++,
            onClick: () => {
                console.log(`redirecting to ${text} page...!!!`);
                window.location.href = `${page}.html`;
            },
        });
    };

    // Initialize buttons in ascending order
    initButton('dataBinding', 'Data Binding', 'DataBinding');
    initButton('paging', 'Paging', 'Paging');
    initButton('scrolling', 'Scrolling', 'Scrolling');
    initButton('editing', 'Editing', 'Editing');
    initButton('grouping', 'Grouping', 'Grouping');
    initButton('filter', 'Filter', 'Filter');
    initButton('sorting', 'Sorting', 'Sorting');
    initButton('column', 'Column Customization', 'ColumnCustomization');
});
