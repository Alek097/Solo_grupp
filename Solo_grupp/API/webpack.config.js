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
          { test: /\.ts?$/, loader: 'ts-loader' }
        ]
    },

    plugins: [
        new HtmlWebpackPlugin({
            template: './Index_Template.html',
            inject: false,
            myFilesInjection: {
                css: [
                    //css files
                ],
                js: [
                     GetBundles('lib/jquery/dist/jquery.min.js'),
                     GetBundles('lib/angular/angular.min.js'),
                     GetBundles('lib/bootstrap/dist/js/bootstrap.min.js'),
                     GetBundles('lib/angular-route/angular-route.min.js')
                ]
            }
        }),
        new TransferWebpackPlugin([
            { from: 'node_modules/angular', to: 'lib/angular' },
            { from: 'node_modules/angular-route', to: 'lib/angular-route' },
            { from: 'node_modules/bootstrap', to: 'lib/bootstrap' },
            { from: 'node_modules/jquery', to: 'lib/jquery' },
        ])
    ]
};

function GetBundles(url) {
    return '/Bundles/' + url;
}