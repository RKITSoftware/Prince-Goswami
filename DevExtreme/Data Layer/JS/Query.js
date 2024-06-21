$(async () => {
    const fetchData = async () =>
        $.ajax({
            type: 'GET',
            url: 'https://6666d488a2f8516ff7a5223a.mockapi.io/users',
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
    //console.log(dataArray);
    var currentDate = new Date();
    currentDate = currentDate.toISOString();
    console.log(currentDate);

    var processedArray = DevExpress.data
        .query(dataArray)
        .filter(['BirthDate', '<', currentDate])
        .sortBy('userName')
        .thenBy('BirthDate','desc')
        .select('Gender','userName', 'id', 'BirthDate')
        .slice(1)
        .groupBy('Gender')
        .toArray();
    console.log(processedArray);

    var age = DevExpress.data.query(dataArray).select('age').toArray();
    //console.log(age)
    DevExpress.data
        .query(age)
        .avg('age')
        .done(function (result) {
            console.log(result);
        });

    DevExpress.data
        .query(age)
        .count('age')
        .done(function (result) {
            console.log(result);
        });

    DevExpress.data
        .query(age)
        .enumerate()
        .done(function (result) {
            console.log(result);
            // "result" contains the obtained array
        });

    // var pushButton = $('#pushButton').dxButton({
    //     text: 'Push',
    //     type: 'success',
    //     onClick: () => {
    //         let userName = prompt(
    //             'Please enter user name to push',
    //             'Harry Potter',
    //         );
    //         let gender = prompt('Select Gender : Male / Female', 'Male');
    //         let user = { userName, gender };
    //         pushData(user);
    //     },
    // });

    // var clearButton = $('#clearButton').dxButton({
    //     text: 'Clear',
    //     type: 'danger',
    //     onClick: () => {
    //         clearData();
    //     },
    // });

    // var reloadButton = $('#reloadButton').dxButton({
    //     text: 'Reload',
    //     type: '',
    //     onClick: () => {
    //         reloadData();
    //     },
    // });

    // // button for inserting data
    // var insertButton = $('#insertButton').dxButton({
    //     text: 'Insert',
    //     type: 'success',
    //     onClick: () => {
    //         let userName = prompt(
    //             'Please enter user name to push',
    //             'Harry Potter',
    //         );
    //         let gender = prompt('Select Gender : Male / Female', 'Male');
    //         let user = { userName, gender };
    //         insertData(user);
    //     },
    // });

    // // button for updating data
    // var updateButton = $('#updateButton').dxButton({
    //     text: 'Update',
    //     type: '',
    //     onClick: () => {
    //         let key = prompt('enter id of user to update', 0);
    //         let user;
    //         store.byKey(key).done((data) => {
    //             user = data;
    //         });
    //         console.log(user);
    //         let userName = prompt(
    //             'Please enter updated user name ',
    //             user.userName,
    //         );
    //         let gender = prompt(
    //             'Select updated Gender : Male / Female',
    //             user.gender,
    //         );
    //         user = { key, userName, gender };
    //         updateData(user);
    //     },
    // });

    // // button for removing data
    // var removeButton = $('#removeButton').dxButton({
    //     text: 'Remove',
    //     type: 'danger',
    //     onClick: () => {
    //         let key = prompt('enter id of user', 0);
    //         removeData(key);
    //     },
    // });

    // // push data into store
    // let pushData = (user) => {
    //     let dataLength;
    //     store.totalCount().done((count) => {
    //         dataLength = count;
    //     });
    //     user.id = dataLength + 1;
    //     store.push([
    //         {
    //             type: 'insert',
    //             data: user,
    //         },
    //     ]);
    // };

    // // insert data into store
    // let insertData = (user) => {
    //     user.id = store._array.length + 1;
    //     store.insert(user);
    // };

    // // remove data using key into store
    // let removeData = (key) => {
    //     store.remove(key);
    // };

    // // update existingrecord in store
    // let updateData = (user) => {
    //     console.log(user);
    //     store.update(user.key, {
    //         userName: user.userName,
    //         gender: user.gender,
    //     });
    // };

    // // clear all data from store
    // let clearData = () => {
    //     store.clear();
    //     console.log('data removed');
    //     selectBox.option('dataSource', store);
    // };

    // // reload dataint store
    // let reloadData = () => {
    //     dataArray.forEach((user) => {
    //         store.push([{ type: 'insert', data: user }]);
    //     });
    // };

    // store.off('loading');
});
