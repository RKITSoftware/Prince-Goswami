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
    initButton('arrayStore', 'Array Store', 'ArrayStore');
    initButton('customStore', 'Custom Store', 'CustomStore');
    initButton('dataSource', 'Data Source', 'DataSource');
    initButton('localStore', 'Local Store', 'LocalStore');
    initButton('query', 'Query', 'Query');
});
