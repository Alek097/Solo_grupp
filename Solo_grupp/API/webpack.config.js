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
                    //JavaScripts libs
                ]
            }
        }),
        new TransferWebpackPlugin([
            //JavaScripts libs
        ])
    ]
};