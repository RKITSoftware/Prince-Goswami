$(async () => {
    const array = [
        {
          "avatar": "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1017.jpg",
          "userName": "Charlie Welch",
          "gender": "female",
          "id": "1"
        },
        {
          "avatar": "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/509.jpg",
          "userName": "Elsa Price",
          "gender": "male",
          "id": "2"
        },
        {
          "avatar": "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/581.jpg",
          "userName": "Nicolas Friesen",
          "gender": "female",
          "id": "3"
        },
        {
          "avatar": "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/904.jpg",
          "userName": "Laura Bogisich",
          "gender": "male",
          "id": "4"
        },
        {
          "avatar": "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/93.jpg",
          "userName": "Karen Parker",
          "gender": "female",
          "id": "5"
        },
        {
          "avatar": "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/198.jpg",
          "userName": "Neal Krajcik",
          "gender": "female",
          "id": "6"
        },
        {
          "avatar": "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/273.jpg",
          "userName": "Karl Kling",
          "gender": "male",
          "id": "7"
        },
        {
          "avatar": "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/702.jpg",
          "userName": "Edwin Jaskolski",
          "gender": "female",
          "id": "8"
        }
      ]
    const fetchData = async () =>
        $.ajax({
            type: 'GET',
            url: 'https://6666d488a2f8516ff7a5223a.mockapi.io/users',
            dataType: 'JSON',
            success: function (response) {
                return response;
            },
        });
    var storeArray = new DevExpress.data.ArrayStore({
        data: await fetchData(),
    });

    var dataStore = new DevExpress.data.DataSource({
        store: storeArray,
    });

    $("#ajaxDataGrid").dxDataGrid({
		dataSource : storeArray,
		//keyExpr: "id",
		columns: ["userName", "BirthDate", "Gender"],
		showBorders: true,
	});
    
    $("#arrayDataGrid").dxDataGrid({
		dataSource : array,
		//keyExpr: "id",
		columns: ["id","userName", "gender"],
		showBorders: true,
	});
    
})
