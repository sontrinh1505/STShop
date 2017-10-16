var cart = {
    init: function () {
        cart.loadData();
        cart.registerEvent();
    },

    registerEvent: function () {

        $('#frmPayment').validate({
            rules: {
                name: "required",
                address: "required",
                email: {
                    required: true,
                    email: true
                },
                phone: {
                    required: true,
                    number: true
                }
            },
            messages: {
                name: "Name can not be blank",
                address: "Address can not be blank",
                email: {
                    required: "Email can not be blank",
                    email: "format email was incorrect"
                },
                phone: {
                    required: "Phone number can not be blank",
                    number: "phone number was invalidate"
                }
            }
        });

        $('.btnAddToCart').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            cart.addItem(productId);
            $('#lbtTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));
        });

        $('.btnDeleteItem').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            cart.deleteItem(productId);
            $('#lbtTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));
        });

        $('.txtQuantity').off('keyup').on('keyup', function (e) {
            var quantity = parseInt($(this).val());
            var productId = parseInt($(this).data('id'));
            var price = parseFloat($(this).data('price'));
            if (isNaN(quantity) == false) {

                var amount = quantity * price;

                $('#amount_' + productId).text(numeral(amount).format('0,0'));
            }
            else {
                $('#amount_' + productId).text(0);
            }
            $('#lbtTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));
            if (!isNaN(quantity)) {
                cart.updateAll();
            }

        });

        $('#btnContinue').off('click').on('click', function (e) {
            e.preventDefault();
            window.location.href = "/";
        });

        $('#btnDeleteAll').off('click').on('click', function (e) {
            e.preventDefault();
            cart.deleteAllItem();

        });

        $('#btnCheckout').off('click').on('click', function (e) {
            e.preventDefault();
            $('#divCheckout').show();

        });

        $('#chkUserLoginInfo').off('click').on('click', function () {
            if ($(this).prop('checked')) {
                cart.getLoginUser();
            } else {
                $('#txtName').val('');
                $('#txtAddress').val('');
                $('#txtEmail').val('');
                $('#txtPhoneNumber').val('');
            }

        });

        $('#btnCreateOrder').off('click').on('click', function (e) {
            e.preventDefault();
            var isvalid = $('#frmPayment').valid();
            if (isvalid) {
                cart.createOrder();
            }
        });

    },

    createOrder: function () {
        var order = {
            CustomerName: $('#txtName').val(),
            CustomerAddress: $('#txtAddress').val(),
            CustomerEmail: $('#txtEmail').val(),
            CustomerMobile: $('#txtPhoneNumber').val(),
            CustomerMessage: $('#txtMessage').val(),
            PaymentMethod: "COD",
            Status: false,
            CustomerId: $('#txtName').val(),
        };
        $.ajax({
            url: '/ShoppingCart/CreateOrder',
            type: 'POST',
            data: {
                orderViewModel: JSON.stringify(order)
            },
            dataType: 'json',
            success: function (respone) {
                if (respone.status) {
                    $('#divCheckout').hide();
                    cart.deleteAllItem();
                    setTimeout(function () {
                        $('#cartContent').html('Thanks you for shopping at my shop.');
                    }, 1000);
                    
                }
                else
                {
                    alert(respone.message);
                }
            }
        });
    },

    updateAll: function () {
        var cartList = [];
        $.each($('.txtQuantity'), function (i, item) {
            cartList.push({
                ProductId: $(item).data('id'),
                Quantity: $(item).val()
            });
        });
        $.ajax({
            url: '/ShoppingCart/Update',
            type: 'POST',
            data: {
                cartData: JSON.stringify(cartList)
            },
            dataType: 'json',
            success: function (respone) {
                if (respone.status) {
                    cart.loadData();
                    console.log('update successfully');
                }
            }
        });
    },

    getLoginUser: function () {
        $.ajax({
            url: '/ShoppingCart/GetUser',
            type: 'POST',
            dataType: 'json',
            success: function (respone) {
                if (respone.status) {
                    var user = respone.data;
                    $('#txtName').val(user.FullName);
                    $('#txtAddress').val(user.Address);
                    $('#txtEmail').val(user.Email);
                    $('#txtPhoneNumber').val(user.PhoneNumber);
                }
            }
        });
    },

    getTotalOrder: function () {
        var listTextBox = $('.txtQuantity');
        var total = 0;
        var quantity = 0;
        $.each(listTextBox, function (i, item) {
            quantity = parseInt($(item).val());
            if (isNaN(quantity) == false) {

                total += quantity * parseFloat($(item).data('price'));
            }
            else {
                total += 0;
            }
        });
        return total;
    },

    addItem: function (productId) {
        $.ajax({
            url: '/ShoppingCart/Add',
            data: {
                productId: productId
            },
            type: 'POST',
            dataType: 'json',
            success: function (respone) {
                if (respone.status) {
                    alert("Add to cart successfully");
                }
                else {
                    alert(respone.message);
                }
            }


        });
    },

    deleteItem: function (productId) {
        $.ajax({
            url: '/ShoppingCart/DeleteItem',
            data: {
                productId: productId
            },
            type: 'POST',
            dataType: 'json',
            success: function (respone) {
                if (respone.status) {
                    cart.loadData();
                }
            }
        });
    },

    deleteAllItem: function () {
        $.ajax({
            url: '/ShoppingCart/DeleteAll',
            type: 'POST',
            dataType: 'json',
            success: function (respone) {
                if (respone.status) {
                    cart.loadData();
                }
            }
        });
    },

    loadData: function () {
        $.ajax({
            url: '/ShoppingCart/GetAll',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    var template = $('#tmplCart').html();
                    Mustache.parse(template);
                    var html = '';
                    var data = res.data;
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ProductId: item.ProductId,
                            ProductName: item.Product.Name,
                            Image: item.Product.Image,
                            ProductPrice: item.Product.Price,
                            ProductPriceF: numeral(item.Product.Price).format('0,0'),
                            Quantity: item.Quantity,
                            Amount: numeral(item.Quantity * item.Product.Price).format('0,0')
                        });

                    });

                    $('#cartBody').html(html);
                    if (html == '') {
                        $('#cartContent').html("no item in your cart.");
                    }
                    $('#lbtTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));
                    cart.registerEvent();
                }
            }

        })
    }
}
cart.init();