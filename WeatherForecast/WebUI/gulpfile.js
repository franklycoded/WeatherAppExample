var gulp = require('gulp'),
    watch = require('gulp-watch'),
    less = require('gulp-less');

gulp.task('copyShared', function () {
    return gulp.src('node_modules/angular/angular.min.js')
            .pipe(gulp.dest('app/shared/scripts/'));
});

gulp.task('build', ['copyShared']);