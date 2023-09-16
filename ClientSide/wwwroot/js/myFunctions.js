$(function () {
    $('.btn-UpdateUserInfo').click(function (e) {
        e.preventDefault();
        UpdateUserInfo();
    })
})

function GetUserInfo(id) {


    Swal.fire({
        title: 'ویرایش اطلاعات کاربری',
        width: 750,
        showConfirmButton: false,
        showCancelButton: true,
        confirmButtonText: 'ویرایش اطلاعات',
        cancelButtonText: `بستن`,
        html: `<div id="formContainer"><div>`,
        didRender: () => {
            getDatafromUrl('/AdminPanel/Account/GetUserInfo/' + id, "#formContainer");
        },
    })
}


function getDatafromUrl(url, divToUpdate) {
    $.ajax({
        method: 'Get',
        url: url,
        dataType: 'Html',
        success: function (data) {
            $(divToUpdate).html(data);
        }
    })
}

function UpdateUserInfo(id) {

    console.log(id);

    Swal.fire({
        title: 'اطلاعات کاربری',
        showConfirmButton: true,
        showCancelButton: true,
        confirmButtonText: 'ویرایش اطلاعات',
        cancelButtonText: `بستن`,
        html: `<div id="formContainer"><div>`,
        didRender: () => {
            getDatafromUrl('/UserPanel/LoadUserInfo/' + id,"#formContainer");
        },
        preConfirm: () => {
            let frmData = $("#frm-Data").serialize();
            console.log(frmData)
            $.ajax({
                method: "post",
                url: '/userpanel/UpdateUserInfo',
                data: frmData,
                success: function (res) {
                    if (res.status == "success") {
                        Swal.fire({
                            icon: 'success',
                            text: res.message,
                            confirmButtonText: 'اوکی',
                            preConfirm: () => {
                                window.location.reload()
                            }
                        })
                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            text: res.message,
                        })
                    }
                }
            })
        }
    })
}

function CreateCategory() {


    Swal.fire({
        title: 'ایجاد دسته جدید',
        showConfirmButton: true,
        showCancelButton: true,
        confirmButtonText: 'ثبت',
        cancelButtonText: `لغو`,
        html: `<div id="formContainer"><div>`,
        didRender: () => {
            getDatafromUrl('/AdminPanel/Store/CreateCategory/',"#formContainer");
        },
        preConfirm: () => {
            let frmData = $("#frm-Data").serialize();
            console.log(frmData)
            $.ajax({
                method: "post",
                url: '/AdminPanel/Store/CreateCategory/',
                data: frmData,
                success: function (res) {
                    if (res.status == "success") {
                        Swal.fire({
                            icon: 'success',
                            text: res.mess,
                            confirmButtonText: 'اوکی',
                            preConfirm: () => {
                                window.location.reload()
                            }
                        })
                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            text: res.mess,
                        })
                    }
                }
            })
        }
    })
}

function CreateProductBySeller() {


    Swal.fire({
        title: 'ایجاد محصول جدید',
        showConfirmButton: true,
        showCancelButton: true,
        Width:880,
        confirmButtonText: 'ثبت',
        cancelButtonText: `لغو`,
        html: `<div id="formContainer"><div>`,
        didRender: () => {
            getDatafromUrl('/UserPanel/CreateProduct/',"#formContainer");
        },
        preConfirm: () => {
            let frmData = $("#frm-Data").serialize();
            console.log(frmData)
            $.ajax({
                method: "post",
                url: '/UserPanel/CreateProduct/',
                data: frmData,
                success: function (res) {
                    if (res.status == "success") {
                        Swal.fire({
                            icon: 'success',
                            text: res.mess,
                            confirmButtonText: 'اوکی',
                            preConfirm: () => {
                                window.location.reload()
                            }
                        })
                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            text: res.mess,
                        })
                    }
                }
            })
        }
    })
}
