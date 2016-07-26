var gulp = require('gulp'),
    watch = require('gulp-watch'),
    less = require('gulp-less');

gulp.task('copyShared', function () {
    return gulp.src('node_modules/angular/angular.min.js')
            .pipe(gulp.dest('app/shared/scripts/'));
});

gulp.task('build-less', function () {
    return gulp.src('./app/styles.less')
                .pipe(less())
    .pipe(gulp.dest('../Content/'));

});

gulp.task('build', ['copyShared', 'build-less']);