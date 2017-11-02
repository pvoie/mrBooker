// Returns the version of Internet Explorer or a -1
// (indicating the use of another browser).
Modernizr.getIEVersion = function() {
	var rv = -1; // Return value assumes failure.
	var ua = navigator.userAgent;

	if (navigator.appName == 'Microsoft Internet Explorer')	{

		// first look at document mode to discern what standard IE is going to use.
		rv = document.documentMode;
		/* as document mode is only in IE>=8, for older IEs or when document mode doesn't
		 * happen to be set yet, we need to fall back to using the User Agent. */
		if (rv === undefined || rv === 0) {
			// see http://msdn.microsoft.com/en-us/library/ms537509(v=vs.85).aspx#ParsingUA
			var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
			if (re.exec(ua) !== null)
				rv = parseFloat(RegExp.$1);
		}
	} else if(navigator.appVersion.indexOf('Trident/') > 0) {
		rv = document.documentMode;
	} else if(ua.indexOf('Edge/') > 0) {
		rv = parseInt(ua.substring(ua.indexOf('Edge/') + 5, ua.indexOf('.', ua.indexOf('Edge/'))), 10);
	}

	return rv;
};

if (Modernizr.addTest) {
	var ieVersion = Modernizr.getIEVersion();
	if(ieVersion > 0) {
		Modernizr.addTest('ie', function() {
			return true;
		});
		Modernizr.addTest('oldie', function() {
			return (9 > ieVersion);
		});
		Modernizr.addTest('ie9', function() {
			return (9 === ieVersion);
		});
		Modernizr.addTest('ie10', function() {
			return (10 === ieVersion);
		});
		Modernizr.addTest('ie11', function() {
			return (11 === ieVersion);
		});
		Modernizr.addTest('ie12', function() {
			return (12 === ieVersion);
		});
		Modernizr.addTest('ie13', function() { // Future compatibility
			return (13 === ieVersion);
		});
		Modernizr.addTest('ie14', function() { // Future compatibility
			return (14 === ieVersion);
		});
		Modernizr.addTest('ie15', function() { // Future compatibility
			return (15 === ieVersion);
		});
	}
}