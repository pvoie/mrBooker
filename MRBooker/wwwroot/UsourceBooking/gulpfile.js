/******************************************************
 * PATTERN LAB NODE
 * EDITION-NODE-GULP
 * The gulp wrapper around patternlab-node core, providing tasks to interact with the core library and move supporting frontend assets.
******************************************************/
var gulp = require('gulp'),
  path = require('path'),
  browserSync = require('browser-sync').create(),
  argv = require('minimist')(process.argv.slice(2));

function resolvePath(pathInput) {
  return path.resolve(pathInput).replace(/\\/g,"/");
}

/******************************************************
* MODIFICATIONS BEGIN - ADDITIONS
******************************************************/
  var replace = require('gulp-replace'),
    sass = require('gulp-sass'),
    concat = require('gulp-concat'),
    json = require('gulp-merge-json'),
    order = require('gulp-order'),
    uglify = require('gulp-uglify'),
    autoprefixer = require('autoprefixer'),
    postcss = require('gulp-postcss'),
    sourcemaps = require('gulp-sourcemaps'),
    browserSync = require('browser-sync').create(),
    jsonfile = require('jsonfile'),
    pkg = require('./package.json'),
    babel = require('gulp-babel'),
    eol = require('os').EOL,
    header = require('gulp-header');

      // CSS Copy
  function copycss(isIntegrated) {
    var isIntegrated = isIntegrated || false;
    var files = [
      '**/*.css'
    ];

    if(isIntegrated) { files.push('!patternlab-specific.css'); }

    var ref = gulp.src(files, {cwd: resolvePath(paths().source.css + '/_compiled')})
      .pipe(gulp.dest(resolvePath(isIntegrated ? paths().integrated.css : paths().public.css)));

    if(!isIntegrated) {
      browserSync.notify('Copying CSS, please wait!');

      ref.pipe(browserSync.stream());
    }

    return  ref;
  }

  gulp.task('integrated-copy:css', function() { return copycss(true); });

  gulp.task('pl-copy:css', function() { return copycss(false); });

  gulp.task('sass', function() {
    var files = [
      './node_modules/foundation-sites/scss',
      paths().source.css+'/*.scss'
    ];
  
    return gulp.src(paths().source.css + "/*.scss")
      .pipe(sourcemaps.init())
      .pipe(sass({
          includePaths: files,
          sourceComments: true,
          outputStyle: 'expanded',
          errLogToConsole: true
      }).on('error', sass.logError))
      .pipe(postcss([ autoprefixer({browsers: ['last 2 versions', 'iOS >= 8', 'Safari >= 8']})]))
      .pipe(sourcemaps.write('.'))
      .pipe(replace('/*# sourceMappingURL=../.'+paths().public.css, '/*# sourceMappingURL='))
      .pipe(gulp.dest(paths().source.css + '/_compiled'));
  });

  gulp.task('json', function() {
    return gulp.src(paths().source.data+'modules/*.json')
      .pipe(json('data.json'))
      .pipe(gulp.dest(resolvePath(paths().source.data)));
  });

  //SampleData (Hit with AJAX)
  gulp.task('pl-copy:endpoints', function() {
    var files = [
      '**/*.*'
    ];

    return gulp.src(files, {cwd: resolvePath(paths().source.endpoints)} )
      .pipe(gulp.dest(resolvePath(paths().public.endpoints)));
  });


  //Foundation JS Source Files
  var foundationSources = [
    './node_modules/foundation-sites/js/foundation.core.js'
    // , './node_modules/foundation-sites/js/foundation.abide.js'
    // , './node_modules/foundation-sites/js/foundation.accordion.js'
    // , './node_modules/foundation-sites/js/foundation.accordionMenu.js'
    // , './node_modules/foundation-sites/js/foundation.drilldown.js'
    // , './node_modules/foundation-sites/js/foundation.dropdown.js'
    // , './node_modules/foundation-sites/js/foundation.dropdownMenu.js'
    // , './source/js/forks/foundation.equalizer.js'
    // , './node_modules/foundation-sites/js/foundation.equalizer.js'
    // , './node_modules/foundation-sites/js/foundation.interchange.js'
    // , './source/js/forks/foundation.interchange.js'
    //, './node_modules/foundation-sites/js/foundation.magellan.js'
    // , './node_modules/foundation-sites/js/foundation.offcanvas.js'
    // , './node_modules/foundation-sites/js/foundation.orbit.js'
    // , './node_modules/foundation-sites/js/foundation.responsiveMenu.js'
    // , './node_modules/foundation-sites/js/foundation.responsiveToggle.js'
    // , './node_modules/foundation-sites/js/foundation.reveal.js'
    // , './node_modules/foundation-sites/js/foundation.slider.js'
    //, './node_modules/foundation-sites/js/foundation.sticky.js'
    // , './source/js/forks/foundation.sticky.js'
    // , './node_modules/foundation-sites/js/foundation.tabs.js'
    // , './node_modules/foundation-sites/js/foundation.toggler.js'
    // , './node_modules/foundation-sites/js/foundation.tooltip.js'
    // , './node_modules/foundation-sites/js/foundation.util.box.js'
    // , './node_modules/foundation-sites/js/foundation.util.keyboard.js'
    , './node_modules/foundation-sites/js/foundation.util.mediaQuery.js'
    // , './node_modules/foundation-sites/js/foundation.util.motion.js'
    // , './node_modules/foundation-sites/js/foundation.util.nest.js'
    // , './node_modules/foundation-sites/js/foundation.util.timerAndImageLoader.js'
    // , './node_modules/foundation-sites/js/foundation.util.touch.js'
    //, './node_modules/foundation-sites/js/foundation.util.triggers.js'
    // , './source/js/forks/foundation.util.triggers.js'
  ];

  //Foundation JS
  gulp.task('js:foundationJS', function(){
    // console.warn('\nWARNING: USING FORKED VERSIONS OF FOUNDATION RESOURCES\n');
    return gulp.src(foundationSources)
      .pipe(babel({
        presets: ['es2015'],
        compact: false
      }))
      .pipe(concat('foundation_custom.js'))
      .pipe(gulp.dest('./source/js/_compiled'))
      .pipe(browserSync.stream());
  });


  //APP JS 
  gulp.task('js:top', function( ){
    browserSync.notify('Compiling JS-TOP, please wait!');

    var files = [
        paths().source.js + '/js-top/**/*.js'
      ],
      includeOrder = [
        '**/*.js'
      ];

    return gulp.src(files)
      .pipe(order(includeOrder))
      .pipe(concat('js-top.js'))
      .pipe(gulp.dest(paths().source.js + '/_compiled'))
      .pipe(browserSync.stream());
  });

  gulp.task('js:bottom', function( ){
    browserSync.notify('Compiling JS-BOTTOM, please wait!');

    var files = [
        paths().source.js + '/_compiled/**/*.js',
        paths().source.js + '/js-bottom/vendor/*.js',
        paths().source.js + '/js-bottom/modules/*.js',
        paths().source.js + '/js-bottom/*.js',
        '!' + paths().source.js + '/_compiled/js-bottom.js', // Created here
        '!' + paths().source.js + '/_compiled/js-top.js', // Created by js:top
        '!' + paths().source.js + '/js-bottom/vendor/jquery-*.js' // Copied over on its own (pl-copy:js)
      ],
      includeOrder = [
          '**/vendor/modernizr*.js',
          '**/vendor/jquery*',
          '_compiled/foundation*.js',
          '**/vendor/*',
          '**/modules/modernizr*.js',
          '**/modules/*.js',
          '!**/init.js', // init is included at the end
          '**/*.js',
          '**/init.js'
      ];

    //////
    // Note: "Include the file at the end"
    //    https://github.com/sirlantis/gulp-order/issues/5
    //    https://github.com/sirlantis/gulp-order/pull/12/files
    //////

    var banner = ['/**',
      ' Customized Pattern Lab',
      ' * @version <%= pkg.integratedVersion %>',
      ' * Last Updated <%= today %>',
      ' */',
      ''
    ].join(eol);

    return gulp.src(files)
      .pipe(order(includeOrder, {base:'./'}))
      .pipe(concat('js-bottom.js'))
      // if you want to minify the JS pre-integration, comment this back in
      // .pipe(uglify())
      .pipe(header(banner,
        {
          pkg : pkg,
          today : new Date().toString()
        }
      ))
      .pipe(gulp.dest('./source/js/_compiled'))  // save .js
      .pipe(browserSync.stream());
  });

  gulp.task('integrated:clean', function( done ){
    var integratedRoot = resolvePath(paths().integrated.root);
    var logPrefix = '     integrated: Cleaning - ';
    console.log('\x1b[36m', '' + logPrefix + ' "' + paths().integrated.root + '"', '\x1b[0m');

    try {
      utils.emptyDirectory(integratedRoot);
      utils.debug(logPrefix + '[SUCCESS]');
    }
    catch (ex) {
      switch (ex.errno) {
        case -4058:
          utils.warning(logPrefix + '[WARN] Directory not found "' + integratedRoot + '"');
          break;
        default:
          utils.error(logPrefix + '[FAIL] to clean \n' + ex );
      }
    }
    done();
  });

  gulp.task('integrated-copy:js-top', function() {
    var files = [
      '**/js-top.js'
    ];

    return gulp.src(files, {cwd: path.resolve(paths().source.js+'/_compiled/')} )
      .pipe(gulp.dest(resolvePath(paths().integrated.jshead)));
  });

  gulp.task('integrated-copy:js-bottom', function() {
    var files = [
      '**/js-bottom.js'
    ];

    return gulp.src(files, {cwd: path.resolve(paths().source.js+'/_compiled/')} )
      .pipe(gulp.dest(resolvePath(paths().integrated.jsfoot)));
  });


/******************************************************
* MODIFICATIONS END
******************************************************/

/******************************************************
 * COPY TASKS - stream assets from source to destination
******************************************************/

/******************************************************
* MODIFICATIONS BEGIN - Including specific JS files to be copied over instead of all of them
******************************************************/

  // JS copy
  gulp.task('pl-copy:js',  function(){
    var files = [
      '_compiled/js-top.js',
      '_compiled/js-bottom.js',
      'patternlab-specific.js',
      'vendor/jquery-*.js'
    ];

    return gulp.src(files, {cwd: path.resolve(paths().source.js)} )
      .pipe(gulp.dest(resolvePath(paths().public.js)));
  });

/******************************************************
* MODIFICATIONS END
******************************************************/

/******************************************************
* MODIFICATIONS BEGIN - Changing out pl-copy tasks to use functions with an integrated flag
******************************************************/

  // Images copy
  function copyimg(integrated) {
    var files = [
      '**/*.*',
      '!*.psd' // Reference images
    ];

    if(integrated) { files.push('!_styleguide/*.*'); }

    return gulp.src(files, {cwd: resolvePath(paths().source.images)} )
      .pipe(gulp.dest(resolvePath(integrated ? paths().integrated.images : paths().public.images)));
  }
  gulp.task('integrated-copy:img', function() { return copyimg(true); });
  gulp.task('pl-copy:img', function() { return copyimg(false); });

  // Favicon copy
  function copyfavicon(integrated) {
    return gulp.src('favicon.ico', {cwd: resolvePath(paths().source.root)} )
      .pipe(gulp.dest(resolvePath(integrated ? paths().integrated.root : paths().public.root)));
  }
  gulp.task('integrated-copy:favicon', function() { return copyfavicon(true); });
  gulp.task('pl-copy:favicon', function() { return copyfavicon(false); });

  // Fonts copy
  function copyfont(integrated) {
    return gulp.src('**/*.*', {cwd: resolvePath(paths().source.fonts)})
      .pipe(gulp.dest(resolvePath(integrated ? paths().integrated.fonts : paths().public.fonts)));
  }
  gulp.task('integrated-copy:font', function() { return copyfont(true); });
  gulp.task('pl-copy:font', function() { return copyfont(false); });


/******************************************************
* MODIFICATIONS END
******************************************************/

// Styleguide Copy everything but css
gulp.task('pl-copy:styleguide', function(){
  return gulp.src(resolvePath(paths().source.styleguide) + '/**/!(*.css)')
    .pipe(gulp.dest(resolvePath(paths().public.root)))
    .pipe(browserSync.stream());
});

// Styleguide Copy and flatten css
gulp.task('pl-copy:styleguide-css', function(){
  return gulp.src(resolvePath(paths().source.styleguide) + '/**/*.css')
    .pipe(gulp.dest(function(file){
      //flatten anything inside the styleguide into a single output dir per http://stackoverflow.com/a/34317320/1790362
      file.path = path.join(file.base, path.basename(file.path));
      return resolvePath(path.join(paths().public.styleguide, '/css'));
    }))
    .pipe(browserSync.stream());
});

/******************************************************
 * PATTERN LAB CONFIGURATION - API with core library
******************************************************/
//read all paths from our namespaced config file
var config = require('./patternlab-config.json'),
  patternlab = require('patternlab-node')(config),
  utils = require('patternlab-node/core/lib/utilities'); // ADDITION

function paths() {
  return config.paths;
}

function getConfiguredCleanOption() {
  return config.cleanPublic;
}

function build(done) {
  patternlab.build(done, getConfiguredCleanOption());
}

gulp.task('pl-assets', gulp.series(
  gulp.parallel(
    'pl-copy:endpoints', // ADDITION
    'pl-copy:js',
    'pl-copy:img',
    'pl-copy:favicon',
    'pl-copy:font',
    'pl-copy:css',
    'pl-copy:styleguide',
    'pl-copy:styleguide-css'
  ),
  function(done){
    done();
  })
);

gulp.task('patternlab:version', function (done) {
  patternlab.version();
  done();
});

gulp.task('patternlab:help', function (done) {
  patternlab.help();
  done();
});

gulp.task('patternlab:patternsonly', function (done) {
  patternlab.patternsonly(done, getConfiguredCleanOption());
});

gulp.task('patternlab:liststarterkits', function (done) {
  patternlab.liststarterkits();
  done();
});

gulp.task('patternlab:loadstarterkit', function (done) {
  patternlab.loadstarterkit(argv.kit, argv.clean);
  done();
});

// gulp.task('patternlab:build', gulp.series('pl-assets', build, function(done){
gulp.task('patternlab:build', gulp.series('js:foundationJS', 'js:top', 'js:bottom', 'sass', 'json', 'pl-assets', build, function(done){  // MODIFIED
  done();
}));

gulp.task('patternlab:installplugin', function (done) {
  patternlab.installplugin(argv.plugin);
  done();
});

/******************************************************
* MODIFICATIONS BEGIN - Additions
******************************************************/

  gulp.task('integrated:headlibs', gulp.series('js:top', 'sass', gulp.parallel(
    'integrated-copy:img',
    'integrated-copy:favicon',
    'integrated-copy:font',
    'integrated-copy:css',
    'integrated-copy:js-top'
  )));

  gulp.task('integrated:footlibs', gulp.series('js:bottom', 'integrated-copy:js-bottom'));

  gulp.task('integrated:build', gulp.series('integrated:clean', gulp.parallel('integrated:headlibs', 'integrated:footlibs')));

/******************************************************
* MODIFICATIONS END
******************************************************/


/******************************************************
 * SERVER AND WATCH TASKS
******************************************************/
// watch task utility functions
function getSupportedTemplateExtensions() {
  var engines = require('./node_modules/patternlab-node/core/lib/pattern_engines');
  return engines.getSupportedFileExtensions();
}
function getTemplateWatches() {
  return getSupportedTemplateExtensions().map(function (dotExtension) {
    return resolvePath(paths().source.patterns) + '/**/*' + dotExtension;
  });
}

function reload() {
  browserSync.reload();
}

function reloadCSS() {
  browserSync.reload('*.css', '*.css.map');
}

function watch() {
  /******************************************************
  * MODIFICATIONS BEGIN - ADDITION
  ******************************************************/
  
    // USING Docker Container on Windows may prevent gulp.watch from picking up changes. 
    // Setting Watch Options to use polling.
    //
    // http://stackoverflow.com/a/37136082
    var watchOpts = {
      awaitWriteFinish: true,
      interval: 1000,
      usePolling: true
     };

    gulp.watch(resolvePath(paths().source.css) + '/**/*.scss', watchOpts).on('change', gulp.series('sass'));
    gulp.watch(resolvePath(paths().source.data) + '/modules/*.json', watchOpts).on('change', gulp.series('json'));
    gulp.watch(resolvePath(paths().source.images) + '/**/*.*', watchOpts).on('change', gulp.series('pl-copy:img', reload));
    gulp.watch(resolvePath(paths().source.endpoints) + '/**/*.json', watchOpts).on('change', gulp.series('pl-copy:endpoints', reload));
    // Note: Not including foundation since any changes to that would come from this file and would need a fresh build anyway, save some processing time
    gulp.watch([resolvePath(paths().source.js) + '/**/*.js', '!' + resolvePath(paths().source.js) + '/_compiled/*.js'], watchOpts).on('change', gulp.series('js:top', 'js:bottom', 'pl-copy:js', reload));
    
  /******************************************************
  * MODIFICATIONS END - ADDITION
  ******************************************************/

  gulp.watch(resolvePath(paths().source.css) + '/**/*.css', watchOpts).on('change', gulp.series('pl-copy:css', reloadCSS));
  gulp.watch(resolvePath(paths().source.styleguide) + '/**/*.*', watchOpts).on('change', gulp.series('pl-copy:styleguide', 'pl-copy:styleguide-css', reloadCSS));

  var patternWatches = [
    resolvePath(paths().source.patterns) + '/**/*.json',
    resolvePath(paths().source.patterns) + '/**/*.md',
    resolvePath(paths().source.data) + '/*.json',
    resolvePath(paths().source.fonts) + '/*',
    resolvePath(paths().source.images) + '/*',
    resolvePath(paths().source.meta) + '/*',
    resolvePath(paths().source.annotations) + '/*'
  ].concat(getTemplateWatches());

  gulp.watch(patternWatches, watchOpts).on('change', gulp.series(build, reload));
}

gulp.task('patternlab:connect', gulp.series(function(done) {
  browserSync.init({
    server: {
      baseDir: resolvePath(paths().public.root)
    },
    online: true,
    snippetOptions: {
      // Ignore all HTML files within the templates folder
      blacklist: ['/index.html', '/', '/?*']
    },
    notify: {
      styles: [
        'display: none',
        'padding: 15px',
        'font-family: sans-serif',
        'position: fixed',
        'font-size: 1em',
        'z-index: 9999',
        'bottom: 0px',
        'right: 0px',
        'border-top-left-radius: 5px',
        'background-color: #1B2032',
        'opacity: 0.4',
        'margin: 0',
        'color: white',
        'text-align: center'
      ]
    }
  }, function(){
    console.log('PATTERN LAB NODE WATCHING FOR CHANGES');
    done();
  });
}));

/******************************************************
 * COMPOUND TASKS
******************************************************/
gulp.task('default', gulp.series('patternlab:build'));
gulp.task('patternlab:watch', gulp.series('patternlab:build', watch));
gulp.task('patternlab:serve', gulp.series('patternlab:build', 'patternlab:connect', watch));