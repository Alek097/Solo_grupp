"use strict";

var webpack = require('webpack');
var TransferWebpackPlugin = require('transfer-webpack-plugin');

var HtmlWebpackPlugin = require('html-webpack-plugin');


module.exports = {
    entry: {
        bundle: "./Scripts/Main.ts",
    },
    output: {
        path: './Bundles/',
        filename: '/app/bundle.js'
    },
    resolve: {
        extensions: ['', '.webpack.js', '.web.js', '.ts', '.js']
    },
    module: {
        loaders: [
          { test: /\.ts?$/, loader: 'ts-loader' },
          { test: /\.css$/, loader: 'style!css!postcss' }
        ]
    },

    plugins: [
        new HtmlWebpackPlugin({
            template: './Index_Template.html',
            inject: false,
            myFilesInjection: {
                css: [
                    GetBundles('lib/bootstrap/dist/css/bootstrap.min.css'),
                    GetBundles('lib/font-awesome/css/font-awesome.min.css'),
                    GetBundles('lib/textangular/dist/textAngular.css')
                ],
                less: [
                    GetBundles('app/style.less')
                ],
                js: [
                   GetBundles('lib/less/dist/less.min.js'),
                   GetBundles('lib/jquery/dist/jquery.min.js'),
                   GetBundles('lib/bootstrap/dist/js/bootstrap.min.js'),
                   GetBundles('lib/angular/angular.min.js'),
                   GetBundles('lib/angular-route/angular-route.min.js'),
                   GetBundles('lib/angular-animate/angular-animate.min.js'),
                   GetBundles('lib/textangular/dist/textAngular-rangy.min.js'),
                   GetBundles('lib/textangular/dist/textAngular-sanitize.min.js'),
                   GetBundles('lib/textangular/dist/textAngular.min.js'),
                   GetBundles('app/bundle.js')
                ]
            }
        }),
        new TransferWebpackPlugin([
            { from: 'node_modules/angular', to: 'lib/angular' },
            { from: 'node_modules/angular-route', to: 'lib/angular-route' },
            { from: 'node_modules/angular-animate', to: 'lib/angular-animate' },
            { from: 'node_modules/bootstrap', to: 'lib/bootstrap' },
            { from: 'node_modules/jquery', to: 'lib/jquery' },
            { from: 'node_modules/less', to: 'lib/less' },
            { from: 'node_modules/textangular', to: 'lib/textangular' },
            { from: 'node_modules/font-awesome', to: 'lib/font-awesome' }
        ])
    ]
};

function GetBundles(url) {
    return '/Bundles/' + url;
}