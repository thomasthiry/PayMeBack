var gulp = require('gulp');

var paths = {
    bower: "./bower_components/",
    scripts: "./www/libraries/",
    css: "./www/css/",
    fonts: "./www/fonts/"
};

gulp.task("copy_libs", function () {
    gulp.src(paths.bower + 'ionic/release/css/ionic.css').pipe(gulp.dest(paths.css));
    gulp.src(paths.bower + 'ionic/release/fonts/ionicons.ttf').pipe(gulp.dest(paths.fonts));
    gulp.src(paths.bower + 'ionic/release/js/ionic.bundle.js').pipe(gulp.dest(paths.scripts));
    gulp.src(paths.bower + 'angular-resource/angular-resource.js').pipe(gulp.dest(paths.scripts));
});