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
    let url = 'https://6666d488a2f8516ff7a5223a.mockapi.io/users/';

    var store = new DevExpress.data.CustomStore({
        key: 'id',
        load: async () => {
            return await $.ajax({
                type: 'GET',
                url: url,
                dataType: 'JSON',
                success: function (response) {
                    console.log(response);
                },
            });
        },
        byKey: async (key) => {
            return await $.ajax({
                type: 'GET',
                url: url+key,
                dataType: 'JSON',
                success: function (response) {
                    console.log(response);
                },
                error: function (e) {
                    console.log(e);
                },
            });
        },
        insert: async (data) => {
            return await $.ajax({
                type: 'POST',
                url: url,
                dataType: 'JSON',
                data: data,
                success: function (response) {
                    console.log(response);
                },
            });
        },
        update: async (data) => {
            return await $.ajax({
                type: 'PUT',
                url: url + data.id,
                dataType: 'JSON',
                data: data,
                success: function (response) {
                    console.log(response);
                },
            });
        },
        remove: async (key) => {
            return await $.ajax({
                type: 'DELETE',
                url: url + key,
                dataType: 'JSON',
                success: function (response) {
                    console.log(response);
                },
            });
        },
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
            // let userName = prompt(
            //     'Please enter updated user name ',
            //     user.userName,
            // );
            // let gender = prompt(
            //     'Select updated Gender : Male / Female',
            //     user.gender,
            // );
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

    // insert data into store
    let insertData = (user) => {
        store.insert(user).done(() => refreshDataSource());
    };

    // remove data using key into store
    let removeData = (key) => {
        store.remove(key).done(() => refreshDataSource());
    };

    // update existingrecord in store
    let updateData = (user) => {
        console.log(user);
        store.update(user);
    };


    let refreshDataSource = () => {
        store.load().done((data) => selectBox.option('dataSource', data));
    };
});
