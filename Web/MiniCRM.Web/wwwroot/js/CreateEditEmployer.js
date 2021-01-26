var phoneCounter = 0;
var phoneLimit = 1;
$(".add_Phone").click(function AddPhone() {
    phoneCounter++;
    if (phoneCounter <= phoneLimit) {
        var clone = $("#phonesDiv").clone(true)
            .append($('<div class="col-sm-1 mb-3 mb-sm-0 form-inline"><a class="deletePhone btn btn-danger btn-circle btn-sm" href="#"><i class="fas fa-trash fa-lg"></i></a></div>'))
            .appendTo("#PhoneContainer");
        clone.find('input').attr('name', "Phones[" + phoneCounter + "].Phone");
        clone.find('input').attr('value', "");
        clone.find('input').attr('placeholder', "Add other phone");
        clone.find('input').attr('id', "Phones_" + phoneCounter + "__.Phone");
        clone.find('span').attr('data-valmsg-for', "Phones[" + phoneCounter + "].Phone");
        clone.find('select').attr('name', "Phones[" + phoneCounter + "].PhoneType");
        $("#addPhoneButton").hide();
    }
});
$("body").on('click',
    ".deletePhone",
    function () {
        $(this).closest(".phone_input").remove();
        $("#addPhoneButton").show();

        phoneCounter--;
    });

var emailCounter = 0;
var emailLimit = 1;
$(".add_Email").click(function AddEmail() {

    emailCounter++;
    if (emailCounter <= emailLimit) {

        var clone = $("#emailsDiv").clone(true)
            .append($('<div class="col-sm-1 mb-3 mb-sm-0 form-inline"><a class="deleteEmail btn btn-danger btn-circle btn-sm" href="#"><i class="fas fa-trash fa-lg"></i></a></div>'))
            .appendTo("#EmailContainer");

        clone.find('input').attr('name', "Emails[" + emailCounter + "].Email");
        clone.find('input').attr('value', "");
        clone.find('input').attr('placeholder', "Add other email");
        clone.find('input').attr('id', "Emails_" + emailCounter + "__.Email");
        clone.find('span').attr('data-valmsg-for', "Emails[" + emailCounter + "].Email");
        clone.find('select').attr('name', "Emails[" + emailCounter + "].EmailType");
        $("#addEmailButton").hide();
    }
});
$("body").on('click',
    ".deleteEmail",
    function () {
        $(this).closest(".email_input").remove();
        $("#addEmailButton").show();
        emailCounter--;
    });