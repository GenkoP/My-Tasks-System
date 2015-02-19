function removeSubtask(button) {

    var parent = $(button).parent().parent().remove();

};


$(document).ready(function () {

    $("#btn-addSubtassks").click(function () {

        var subtaskWrapper = $("#sobtasksInputs");


        var count = subtaskWrapper.children().length;

        if (count > 10) {

            subtaskWrapper.append('<h1 class="error-message">Subtask count can not be to big from 10! </h1>')

        }
        else {


            var formGroup = $("<div>").addClass("form-group row").attr('id', 'fg_' + count);

            var md6 = $("<div>").addClass("col-md-6");

            md6.append('<label for="subtsk_' + count + '" >Subtask title:</label>')
            md6.append('<input id="subtsk_' + count + '" class="text-box single-line form-control" data-val="true" data-val-length="The field  Subtask title: must be a string with a minimum length of 2 and a maximum length of 30." data-val-length-max="30" data-val-length-min="2" data-val-required="The  Subtask title: field is required." name="[' + count + '].SubtaskTitle" type="text" value="">')

            md6.append('<span class="field-validation-valid error-messages" data-valmsg-for="[' + count + '].SubtaskTitle" data-valmsg-replace="true"></span>');

            formGroup.append(md6);

            var md62 = $("<div>").addClass("col-md-4");

            md62.append('<label for="sel_' + count + '" >Subtask priority:</label>')
            md62.append('<select id="sel_' + count + '" class="form-control" data-val="true" data-val-required="The Subtask priority: field is required." name="[' + count + '].SubtaskPriority"><option selected="selected" value="0">Low</option><option value="1">Medium</option><option value="2">Important</option></select>')


            formGroup.append(md62);

            var revBtn = $('<div>').addClass('col-md-2')
                .append('<span class="btn btn-sm btn-primary rev-btn"  onclick="removeSubtask(this)" >Remove</span>');

            formGroup.append(revBtn);

            subtaskWrapper.append(formGroup);



            //  clear and restore form validation
            $("form").removeData("validator");
            $("form").removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse("form");
        }


    });


});