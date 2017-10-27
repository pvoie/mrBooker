(function($){

	var <%= jsName %>, defaultOptions, __bind;

	__bind = function(fn, me) {
		return function() {
			return fn.apply(me, arguments);
		};
	};

	// Plugin default options
	defaultOptions = {
		
	};

	<%= jsName %> = (function(options) {
		function <%= jsName %>(handler, options) {
			this.handler = handler;

			// Extend default options
			$.extend(true, this, defaultOptions, options);

			// Bind methods
			this.example = __bind(this.example, this);
			this.init = __bind(this.init, this);
		}

		<%= jsName %>.prototype.example = function(e) {
			alert("Hello World! It's automplete");
		};

		// Main method
		<%= jsName %>.prototype.init = function() {
			if(!this.handler.length) { return; }
			
			this.handler.on('click.<%= jsName %>', this.example);
		};

		return <%= jsName %>;
	})();

	$.fn.<%= jsName %> = function(options) {
		// Create a <%= jsName %> instance if not available.
		if (!this.<%= jsName %>Instance) {
			this.<%= jsName %>Instance = new <%= jsName %>(this, options || {});
		}

		this.<%= jsName %>Instance.init();

		return this;
	};
})(jQuery);