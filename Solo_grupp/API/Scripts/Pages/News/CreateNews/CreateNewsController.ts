﻿import {CreateNewsService} from './CreateNewsService.ts'
import {UploadResult} from '../../../Common/Models/UploadResult.ts'
import {ModalMessageService} from '../../../Common/ModalMessage/ModalMessageService.ts'
import {CreateNews} from '../../../Common/Models/CreateNews.ts'
import {ControllerResult} from '../../../Common/Models/ControllerResult.ts'
import {PermissionService} from '../../../Common/PermissionService.ts'
import {ResolutionType} from '../../../Common/Models/ResolutionType.ts'
import {Validate} from '../../Authentification/Validate.ts'

export class CreateNewsController extends Validate {

    public static $inject: string[] =
    [
        '$timeout',
        'createNewsService',
        'modalMessageService',
        'permissionService'
    ];

    public imgUrls: string[] = [];
    public content: string = null;

    constructor(
        private timeout: ng.ITimeoutService,
        private service: CreateNewsService,
        private modalService: ModalMessageService,
        permissionService: PermissionService
    ) {
        super(
            new CreateNews(),
            'solo_grupp_create_news');

        permissionService.IsResolution(ResolutionType.AddNews)
            .error(() => {
                window.location.href = '/#/Home';
            });

        let model: CreateNews = this.getModel();

        if (model != undefined) {
            this.content = model.Content;
            this.imgUrls = model.Urls;
            angular.element('#title').val(model.Title);
        }
    }

    public titleValidate(): boolean {
        this.elem = angular.element('#error-title');

        let val: string = angular.element('#title').val();

        this.model.Title = val;

        if (val == undefined || val === '') {
            this.writeError('Введите заголовок');
            return false;
        }
        else {
            this.clearError();
            this.saveModel();
            return true;
        }
    }

    public contentValidate(): boolean {
        this.elem = angular.element('#error-content');

        if (this.content == undefined || this.content === '') {
            this.writeError('Введите текст новости');
            return false;
        }
        else {
            this.clearError();

            this.model.Content = this.content;

            this.saveModel();
            return true;
        }
    }

    public submit(): void {
        let valid: boolean = true;

        valid = this.contentValidate() && valid;
        valid = this.titleValidate() && valid;

        if (valid) {

            this.service.createNews(this.model)
                .success((data: ControllerResult<any>) => {
                    if (data.IsSucces) {
                        window.location.href = data.Message;
                        this.removeModel();
                    }
                    else {
                        this.modalService.open(
                            data.Message,
                            'Упс!'
                        );
                    }
                });
        }
    }

    public uploadFile(): void {
        let tmpVar: any = document.getElementById('uploadFile');
        let files = tmpVar.files;

        let data: FormData = new FormData();
        for (let i: number = 0; i < files.length; i++) {
            data.append('file' + i, files[i]);
        }

        angular.element('#uploadFile').remove();

        angular.element('#add-image').append('<input id="uploadFile" type="file" multiple onchange="angular.element(this).controller().uploadFile()" accept="image/*"/>');

        this.service.uploadImage(data)
            .done((data: UploadResult) => {
                if (data.IsUploading) {
                    for (let i: number = 0; i < data.Urls.length; i++) {
                        this.imgUrls[this.imgUrls.length] = data.Urls[i];

                        this.model.Urls = this.imgUrls;

                        this.saveModel();

                        this.timeout();
                    }
                }
                else {
                    this.modalService.open('Произошла ошибка при загрузке файла. Убедитесь что файл имеет формат изображения.', 'Ошибка!');
                }
            });
    }
}