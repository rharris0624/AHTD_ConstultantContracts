/// <reference path="_references.js" />
/// <reference path="global-shared.js" />

var globalAppListLoaded = false;
var globalUserInfoLoaded = false;

//Requires Jquery 1.9+

var hasPreAuthenticated = false;
var webAPIHtmlPage = "http://devweb/UserProv/Home/PreAuth"

function preauthenticate() {

    //ADFS breaks Ajax requests, so we pre-authenticate the first call using an iFRAME and "authentication" page to get the cookies set

    return $.Deferred(function (d) {

        if (hasPreAuthenticated) {
            //console.log("Already pre-authenticated, skipping");
            d.resolve();
            return;
        }
        //Potentially could make this into a little popup layer 
        //that shows we are authenticating, and allows for re-authentication if needed
        var iFrame = $("<iframe></iframe>");
        iFrame.hide();
        iFrame.appendTo("body");
        iFrame.attr('src', webAPIHtmlPage);


        iFrame.ready(function () {
            hasPreAuthenticated = true;
            iFrame.remove();
            d.resolve();
        });

    });

};

function globalSiteMapInit() {
    $('#global-sitemap').click(function (event) {
        $('#global-sitemap-flyout').slideToggle(500, function () {
            if (!globalAppListLoaded && $('#global-sitemap-flyout').is(':visible'))
                preauthenticate().then(function () {
                    globalGetApplicationsList();
                });
        });

        event.stopPropagation();
    });
    $('html').click(function () {
        if ($('#global-sitemap-flyout').is(':visible')) {
            $('#global-sitemap-flyout').slideUp(500);
        }
    });
    $('#global-sitemap-flyout').click(function (event) {
        event.stopPropagation();
    });
}
function globalUserInfoInit() {
    $('#global-userinfo').click(function (event) {
        $(this).toggleClass('on');
        $('#global-userinfo-popup').fadeToggle(200, function () {
            if (!globalUserInfoLoaded && $('#global-userinfo-popup').is(':visible'))
                globalGetUserInfo();
        });

        event.stopPropagation();
    });
    $('html').click(function () {
        if ($('#global-userinfo').hasClass('on')) {
            $('#global-userinfo-popup').fadeOut(200);
            $('#global-userinfo').removeClass('on');
        }
    });
    $('#global-userinfo-popup').click(function (event) {
        event.stopPropagation();
    });
    $('#global-userinfo-help').click(function () {
        window.open('/stsweb/help/claims.htm', '_blank', 'width=400,height=400,location=no,resizable=yes,scrollbars=yes,status=no,toolbar=no');
    });
}
function globalGetApplicationsList() {
    $('#global-sitemap-flyout').html('<span>Loading site list...</span>');

    var query = "Applications/GetWebApplications";

    //Need to get auth token.

    $.ajax({
        type: "GET",
        url: "/userprov/api/" + query,
        dataType: "json",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Accept", "application/json");
        },
        success: function (d) {
            var wowitsnothing;
        },
        error: function (d) {
            var wowitsnothing;
        }
    });

    GetFromUserProvDataService(query,
		function (msg) {
		    globalBuildApplicationsList(msg);
		    globalAppListLoaded = true;
		},
		function (jqXHR, textStatus, errorThrown) {
		    $('#global-sitemap-flyout').html('<span class="globalError">Error retrieving application list.</span>');
		}
	);
}
function globalBuildApplicationsList(data) {
    $('#global-sitemap-flyout').empty();

    var list, sectionNumber = null;

    for (var i in data) {
        var post = data[i];

        if (post.AppName == null || post.FormPresence == null) continue;

        if (sectionNumber == null || sectionNumber != post.AppOwner.Number) {
            sectionNumber = post.AppOwner.Number;

            if (list) $('#global-sitemap-flyout').append(list + '</ul></div>');

            list = '<div class="globalLinkContainer"><span class="globalLinkLabel">' + post.AppOwner.Description + '</span><ul>';
        }

        list += '<li><a href="/' + post.AppName.toLowerCase() + '/">' + post.FormPresence + '</a></li>';
    }

    $('#global-sitemap-flyout').append(list + '</ul></div>');
}
function globalGetUserInfo() {
    $('#global-userinfo-popup').prepend('<span id="global-userinfo-loading">Loading user info...</span>');

    var locPathMatches = location.pathname.match(/\/\w+\//);

    if (locPathMatches.length > 0) {
        $.ajax({
            type: "GET",
            url: locPathMatches[0] + "Shared/Claims",
            dataType: "html",
            success: function (msg) {
                $('#global-userinfo-loading').remove();
                $('#global-userinfo-popup').prepend(msg);
                globalUserInfoLoaded = true;
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('#global-userinfo-popup').prepend('<span class="globalError">Error retrieving user info.</span>');
            }
        });
    }
    else {
        $('#global-userinfo-popup').prepend('<span class="globalError">Unable to load user info.</span>');
    }
}
function globalUpdateRecentSites() {
    var splitPath = location.pathname.split('/');
    if (splitPath.length > 1) {
        var siteName = splitPath[1];
        var recentSites = $.cookie('globalRecentSites');

        if (recentSites) {
            recentSites = recentSites.split(',');
        }
        else {
            recentSites = [];
        }

        var sitePos = recentSites.indexOf(siteName);

        // Skip updating if site is already at the top
        if (sitePos == recentSites.length - 1) {
            // Remove current site if already in the list
            if (sitePos != -1) {
                recentSites.splice(sitePos, 1);
            }

            // Add current site to top
            recentSites.push(siteName);

            // Trim to last 5 sites
            if (recentSites.length > 5) {
                recentSites = recentSites.slice(recentSites.length - 5);
            }

            // Save the list back to the browser
            $.cookie('globalRecentSites', recentSites.toString(), {
                expries: 100,
                path: '/'
            });
        }
    }
}