/// <reference path="_references.js" />

/* FIX FOR IE'S MISSING ARRAY.INDEXOF METHOD */
if (!Array.indexOf) {
	Array.prototype.indexOf = function (obj) {
		for (var i = 0; i < this.length; i++) {
			if (this[i] == obj) {
				return i;
			}
		}
		return -1;
	}
}

function GetFromUserProvDataService(query, success, error) {
	$.ajax({
		type: "GET",
		url: "/userprovdata/UserProvDataService.svc/" + query,
		dataType: "json",
		beforeSend: function (xhr) {
			xhr.setRequestHeader("Accept", "application/json;odata=verbose");
			xhr.setRequestHeader("MaxDataServiceVersion", "3.0");
		},
		success: success,
		error: error
	});
}

function AddPlaceholders() {
	if (Modernizr && !Modernizr.input.placeholder) {
		$('input[type="text"][placeholder]').focus(function () {
			var input = $(this);
			if (input.val() == input.attr('placeholder')) {
				input.val('');
				input.removeClass('placeholder');
			}
		}).blur(function() {
			var input = $(this);
			if (input.val() == '' || input.val() == input.attr('placeholder')) {
				input.addClass('placeholder');
				input.val(input.attr('placeholder'));
			}
		}).blur();
		$('input[type="text"][placeholder]').parents('form').submit(function () {
			$(this).find('input[type="text"][placeholder]').each(function () {
				var input = $(this);
				if (input.val() == input.attr('placeholder')) {
					input.val('');
				}
			})
		});
	}
}
