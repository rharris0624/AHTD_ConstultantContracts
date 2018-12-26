var JTABLE_DIALOG_LOADING_CONTENT = '<div class="formLoading"><img src="/Content/images/loading.gif" /><br />Loading...</div>';

function LoadDropdownOptions(selector) {
	$(selector + ' select').each(function () {
		var ddl = $(this);
		if (ddl.data('optionsUrl')) {
			$.getJSON(ddl.data('optionsUrl'), function (result) {
				ddl.empty();
				$(result.Options).each(function () {
					ddl.append(
					$('<option/>', {
						value: this.Value,
						selected: ddl.data('optionsSelectedValue') == this.Value
					}).html(this.DisplayText)
					);
				});
			})
		}
	});
}