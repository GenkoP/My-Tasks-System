jQuery(function ($) {

    $('#DateToEnd')
        .attr(
        "data-val-date",
        "The field Date to end: is incorect! Date must be a to bigger from current datetime or format is incorect! ");

    $('#DateToEnd').datepicker({
        dateFormat: 'dd/mm/yy',
        minDate: new Date(),
    }).val();


    $.validator.addMethod("date",
        function (value, element) {

            var today = new Date(),
                day = today.getDate(),
                month = today.getMonth(),
                year = today.getFullYear(),
                curDate = new Date(year, month, day);
            dateIsCorect = true;

            if (this.optional(element)) {
                return true;
            }

            try {
                var inputDate = $.datepicker.parseDate('dd/mm/yy', value);
            } catch (e) {
                return false;
            }

            if (curDate > inputDate) {
                dateIsCorect = false;
            }
            else {
                dateIsCorect = true;
            }
            return dateIsCorect;
        });



});