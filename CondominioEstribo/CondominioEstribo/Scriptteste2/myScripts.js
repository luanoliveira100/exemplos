var Categories = []
    //fetch categories
function LoadCategory(element) {
    if (Categories.length == 0) {
        //ajax function for fetch data
        $.ajax({
            type:"GET",
            url: '/teste/getProductCategories',
            succes: function (data) {
                Categories = data;
                //render category
                renderCategory(element);
            }
        })
            
    } else {
        //render category to the element
    }
}

function renderCategory(element) {
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));
    $.each(Categories, function (i, val) {
        $ele.append($('<option/>').val(val.CategoryId).text(CategoryName));
    })
}


//fetch products
function LoadProduct(categoryDD) {
    $.ajax({
        type: "GET",
        url: "/teste/getProducts",
        data: { 'CategoryId': $(categoryDD).val()},
        succes:function(data){
            // render products to appropiate dropdown
            renderProduct($(categoryDD).parents('mycontainer').find('select.product'), data)
        },
        error: function (error) {
            console.log(error);
        }
    })
}

function renderProduct(element, data) {
    //render product 
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option>').val('0').text('Select'));
    $each(data, function (i, val) {
        $ele.append($('<option>').val(val.ProductId).text(val.ProductName));
    })
}

$(document).ready(function () {
    //add button click envent
    $('#add').click(function () {
        //validation and add ordem Items
        var isAllValid = true;
        if ($('#productCategory').val() == "0") {
            isAllValid = false;
            $('#productCategory').siblings('span.error').css('visibility', 'visible');

        } else {
            $('#productCategory').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#product').val() == "0") {
            isAllValid = false;
            $('#product').siblings('span.error').css('visibility', 'visible');

        } else {
            $('#product').siblings('span.error').css('visibility', 'hidden');
        }
        if (!($('quantity').val().trim() != '' && (parseInt($('#quantity').val()) || 0))) {
            isAllValid = false;
            $('#quantity').siblings('span.error').css('visibility', 'visible');

        } else {
            $('#quantity').siblings('span.error').css('visibility', 'hidden');
        }


        if (!($('value').val().trim() != '' && !isNaN($('#value').val().trim()))) {
            isAllValid = false;
            $('#value').siblings('span.error').css('visibility', 'visible');

        } else {
            $('#value').siblings('span.error').css('visibility', 'hidden');
        }

        if (isAllValid) {
            var $newRow = $('#mainrow').clone().removeAttr('id');
            $('pc.', $newRow).val($('#productCategory').val());
            $('#product', $newRow).val($('#product').val());
            //replace add button with remove button
            $('#add', $newRow).addclass('remove').val('Remove').removeClass('btn-success').addclass(btn - danger);

            //remove id attribute from new clone row
            $('#productCategory,#product,#quantity,#value,#add', $newRow).removeAttr('id');
            $('span.error', $newRow).remove();
            //append clone row
            $('#orderdetailsItems').append($newRow);

            //Clear select data 
            $('#productCategory,#product').val('0');
            $('#quantity,#value').val('');
            $('#orderItemError').empty();
        }

    })
    // remove button click envent
    $('#orderdetailsItems').on('click', 'remove', function () {
        $(this).parents('tr').remove();
    })

    $('#submit').click(function () {
        var isAllValid = true;

        //validate order items
        $('#orderItemError').text('');
        var list = [];
        var errorItemCount = 0;
        $('#orderdetailsItem tbody tr').each(function (index, ele) {
            if ($('select.product', this).val() == "0" ||
                (parseInt($('#quantity', this).val()) || 0) == 0 ||
                $('.value', this).val() == '' ||
                isNaN($('.value', this).val())
                ) {
                errorItemCount++;
                $(this).addclass('error');

            } else {
                var orderItem = {
                    ProductId: $('select.product', this).val(),
                    Quantity: parseInt($('.quantity', this).val()),
                    Value: parseFloat($('.value', this).val())
                }
                list.push(orderItem);
            }
        })
        if (errorItemCount > 0) {
            $('#orderItemError').text(errorItemCount + "invalid entry in order item list.");
            isAllValid = false;
        }
        if (list.length == 0) {
            $('#orderItemError').text("at least 1 order item required");
            isAllValid = false;
        }
        if ($('#orderNo').val().trim() == '') {
            $('#orderNo').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        } else {
            $('#orderNo').siblings('span.error').css('visibility', 'hidden');
            isAllValid = false;
        }
        if ($('#orderDate').val().trim() == '') {
            $('#orderDate').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        } else {
            $('#orderDate').siblings('span.error').css('visibility', 'hidden');
            isAllValid = false;
        }
        if (isAllValid) {
            var data = {
                OrderNo: $('#orderNo').val().trim(),
                OrderDateString: $('#orderDate').val().trim(),
                Description: $('#description').val().trim(),
                OrderDetails: list
            }
            $(this).val('Please wait...');
            $.ajax({
                type: 'POST',
                url: '/teste/save',
                data: JSON.stringify(data),
                contentType: 'aplication/json',
                success: function (data) {
                    if (data.status) {
                        alert('Successfuly saved');
                        // here will clear the form
                        list = [];
                        $('#orderNo,#orderDate,#description').val('');
                        $('#orderDetailsItems').empty();
                    } else {
                        alert('Error');
                    }
                    $('#submit').text('save');
                },
                error: function (error) {
                    console.log(error);
                    $('#submit').text('save');
                }

            });
        }

    });

});

LoadCategory($('productCategory'));

