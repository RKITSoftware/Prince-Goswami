$(async () => {
    const fetchData = async () =>
        $.ajax({
            type: 'GET',
            url: 'https://6666d488a2f8516ff7a5223a.mockapi.io/userData',
            dataType: 'JSON',
            success: function (response) {
                return response;
            },
        });

    DevExpress.ui.dxSelectBox.defaultOptions({
        device: { deviceType: 'desktop' },
        options: {
            width: '40%',
            dataSource: await fetchData(),
        },
    });

    let dataArray = await fetchData();

    var store = new DevExpress.data.ArrayStore({
        key: 'id',
        data: dataArray,
        errorHandler: function (error) {
            console.log(error.message);
        },
        onInserted: (values, keys) => {
            console.log('Inserted ');
            console.log('Values ', values);
            console.log('Keys ', keys);
        },
        onInserting: () => {
            console.log('Inserting data..!!');
        },
        onLoaded: () => {
            console.log('data Loaded..!!');
        },
        onLoading: () => {
            console.log('loading Data..!!');
        },
        onModified: () => {
            console.log('Data Modified..!!');
        },
        onModifying: () => {
            console.log('Modifying data..!!');
        },
        onPush: (changes) => {
            console.log('Pushed ', changes[0].data);
        },
        onUpdated: (values, keys) => {
            console.log('Inserted ');
            console.log('Values ', values);
            console.log('Keys ', keys);
        },
        onRemoving: () => {
            console.log('Removing data..!!');
        },
        onRemoved: () => {
            console.log('Data ..!!');
        },
    });

    var selectBox = $('#selectBox')
        .dxSelectBox({
            dataSource: store,
            acceptCustomValue: true, // if true then it will allow value provided by user
            name: 'userList',
            placeHolder: 'select user',
            deferRendering: true, // If false, the content is rendered immediately.
            displayExpr: 'userName',
            valueExpr: 'id',
            openOnFieldClick: true,
            showClearButton: true,
            showDropDownButton: true,
            wrapItemText: true, // if text exceeds the drop-down list width should be wrapped.
            useItemTextAsTitle: true,
            groupTemplate(data) {
                return $(
                    `<div class='custom-icon'><span class='dx-icon-user'></span> ${data.key}</div>`,
                );
            },
            noDataText: 'no data found',
        })
        .dxSelectBox('instance');

    var pushButton = $('#pushButton').dxButton({
        text: 'Push',
        type: 'success',
        onClick: () => {
            let userName = prompt(
                'Please enter user name to push',
                'Harry Potter',
            );
            let gender = prompt('Select Gender : Male / Female', 'Male');
            let user = { userName, gender };
            pushData(user);
        },
    });

    var clearButton = $('#clearButton').dxButton({
        text: 'Clear',
        type: 'danger',
        onClick: () => {
            clearData();
        },
    });

    var reloadButton = $('#reloadButton').dxButton({
        text: 'Reload',
        type: '',
        onClick: () => {
            reloadData();
        },
    });

    // button for inserting data
    var insertButton = $('#insertButton').dxButton({
        text: 'Insert',
        type: 'success',
        onClick: () => {
            let userName = prompt(
                'Please enter user name to push',
                'Harry Potter',
            );
            let gender = prompt('Select Gender : Male / Female', 'Male');
            let user = { userName, gender };
            insertData(user);
        },
    });

    // button for updating data
    var updateButton = $('#updateButton').dxButton({
        text: 'Update',
        type: '',
        onClick: () => {
            let key = prompt('enter id of user to update', 0);
            let user;
            store.byKey(key).done((data) => {
                user = data;
            });
            console.log(user);
            let userName = prompt(
                'Please enter updated user name ',
                user.userName,
            );
            let gender = prompt(
                'Select updated Gender : Male / Female',
                user.gender,
            );
            user = { key, userName, gender };
            updateData(user);
        },
    });

    // button for removing data
    var removeButton = $('#removeButton').dxButton({
        text: 'Remove',
        type: 'danger',
        onClick: () => {
            let key = prompt('enter id of user', 0);
            removeData(key);
        },
    });

    // push data into store
    let pushData = (user) => {
        let dataLength;
        store.totalCount().done((count) => {
            dataLength = count;
        });
        user.id = dataLength + 1;
        store.push([
            {
                type: 'insert',
                data: user,
            },
        ]);
    };

    // insert data into store
    let insertData = (user) => {
        user.id = store._array.length + 1;
        store.insert(user);
    };

    // remove data using key into store
    let removeData = (key) => {
        store.remove(key);
    };

    // update existingrecord in store
    let updateData = (user) => {
        console.log(user);
        store.update(user.key, {
            userName: user.userName,
            gender: user.gender,
        });
    };

    // clear all data from store
    let clearData = () => {
        store.clear();
        console.log('data removed');
        selectBox.option('dataSource', store);
    };

    // reload dataint store
    let reloadData = () => {
        dataArray.forEach((user) => {
            store.push([{ type: 'insert', data: user }]);
        });
    };

    store.off('loading')
});
