function AddPurchase() {
    var tbody = $('purchaseTbl');
    let products = [];
    for (var i = 0; i < tbody.children().length; i++) {
        let person = {
            "id": 0,
            "product_id": 4,
            "qty": 5,
            "purchase_price_one_product": 10,
            "total_purchase_price_one_product": 50,
            "purchase_inv_id": 0,
            "notes": "testplus"
        };
    }
    //var c = $("#cat_name").val().trim();
    //var formData
    //    =
    //{
    //    "id": 0,
    //    "invoice_total": 50,
    //    "store_id": 4,
    //    "purchase_Added_Time": "2022-03-21T13:30:28.189Z",
    //    "purchase_invoice_details": [
    //        {
    //            "id": 0,
    //            "product_id": 4,
    //            "qty": 5,
    //            "purchase_price_one_product": 10,
    //            "total_purchase_price_one_product": 50,
    //            "purchase_inv_id": 0,
    //            "notes": "testplus"
    //        }
    //    ],
    //    "purchase_store_details": [
    //        {
    //            "id": 0,
    //            "product_id": 4,
    //            "qty": 5,
    //            "store_id": 4
    //        }
    //    ]
    //}
    //$.ajax({
    //    type: 'POST',
    //    url: "https://localhost:44315/api/Purchase/AddPurchase",
    //    dataType: "json",
    //    contentType: "application/json",
    //    data: JSON.stringify(formData),
    //    success: function () {
    //        $('#exampleModal').modal('hide');
    //        location.reload();
    //    },
    //    error: function () {
    //        console.log('Fail!!!');
    //    }
    //});

}
