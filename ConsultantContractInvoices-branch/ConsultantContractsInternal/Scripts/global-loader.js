/// <reference path="_references.js" />
/// <reference path="global-shared.js" />

$(document).ready(function () {
	globalTopbarLoader();
	globalUserInfoInit();
	globalSiteMapInit();
	globalUpdateRecentSites();
});

function globalTopbarLoader() {
	if ($('#global-topbar-container').length == 0) {
		$('body').prepend($('<div></div>').attr('id', 'global-topbar-container'));
	}
	if ($('#global-topbar-container').length == 0) {
		$('body').prepend($('<div></div>').attr('id', 'global-topbar-container'));
	}
	if ($('#global-topbar').length == 0) {
		$('#global-topbar-container').prepend($('<div></div>').attr('id', 'global-topbar'));
	}

	if ($('#global-links-container').length == 0) {
		$('#global-topbar').prepend($('<div></div>').attr('id', 'global-links-container'));
	}
	if ($('#global-homelink').length == 0) {
		$('#global-links-container').prepend(
		$('<span></span>')
			.attr('id', 'global-homelink')
			.html('<a href="http://ahtdnet/"><img src="/Content/images/ahtd-logo-color-24.png")" alt="AHTD logo" title="" /> AHTDnet</a>'));
	}
	if ($('#global-sitemap').length == 0) {
		$('#global-links-container').append(' <span class="globalSeparator">|</span> ');
		$('#global-links-container').append(
		$('<span></span>')
			.attr('id', 'global-sitemap')
			.html('Site list <span id="global-sitemap-dropdown">▾</span>'));
	}

	if ($('#global-userinfo-container').length == 0) {
		$('#global-topbar').prepend($('<div></div>').attr('id', 'global-userinfo-container'));
	}
	if ($('#global-userinfo').length == 0) {
		$('#global-userinfo-container').prepend(
		$('<span></span>')
			.attr('id', 'global-userinfo')
			.html('<img src="/Content/images/user-silhouette.png")" alt="User icon" title="" /> Your info <span id="global-userinfo-dropdown">▾</span>'));
	}
	if ($('#global-userinfo-popup').length == 0) {
		$('#global-userinfo-container').append(
		$('<div></div>')
			.attr('id', 'global-userinfo-popup')
			.css('display', 'none'));
	}
	if ($('#global-userinfo-help').length == 0) {
		$('#global-userinfo-popup').append('<a id="global-userinfo-help" href="#">What are these?</a>');
	}
	if ($('#global-userinfo-claims, #global-userinfo-popup > table').length != 0) {
		globalUserInfoLoaded = true;
	}

	if ($('#global-sitemap-container').length == 0) {
		$('#global-topbar-container').append($('<div></div>').attr('id', 'global-sitemap-container'));
	}
	if ($('#global-sitemap-flyout').length == 0) {
		$('#global-sitemap-container').html($('<div></div>').attr('id', 'global-sitemap-flyout').css('display', 'none'));
	}
}