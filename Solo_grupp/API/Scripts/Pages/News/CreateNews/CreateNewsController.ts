﻿import {CreateNewsService} from './CreateNewsService.ts'
import {UploadResult} from '../../../Common/Models/UploadResult.ts'

export class CreateNewsController {

    public static $inject: string[] =
    [
        '$timeout',
        'createNewsService'
    ];

    public imgUrls: string[] = [];
    public content: string = null;

    constructor(
        private timeout: ng.ITimeoutService,
        private service: CreateNewsService
    ) {

    }

    public submit(): void {
        let input: JQuery = angular.element('input[type=file]');

        let data = new FormData();

        for (let i: number = 0; i < input.length; i++) {
            data.append('file' + i, input[i]);
        }
    }

    public uploadFile(): void {
        let tmpVar: any = document.getElementById('uploadFile');
        let files = tmpVar.files;

        let data: FormData = new FormData();
        for (let i: number = 0; i < files.length; i++) {
            data.append('file' + i, files[i]);
        }

        this.service.uploadImage(data)
            .done((data: UploadResult) => {
                if (data.IsUploading) {
                    for (let i: number = 0; i < data.Urls.length; i++) {
                        this.imgUrls[this.imgUrls.length] = data.Urls[i];

                        this.timeout();
                    }
                }
            });
    }
}