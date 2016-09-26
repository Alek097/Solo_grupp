import {CreateNewsService} from './CreateNewsService.ts'
import {UploadResult} from '../../../Common/Models/UploadResult.ts'
import {ModalMessageService} from '../../../Common/ModalMessage/ModalMessageService.ts'
import {CreateNews} from '../../../Common/Models/CreateNews.ts'
import {MoveTo} from '../../../Common/Models/MoveTo.ts'

export class CreateNewsController {

    public static $inject: string[] =
    [
        '$timeout',
        'createNewsService',
        'modalMessageService'
    ];

    public imgUrls: string[] = [];
    public content: string = null;

    constructor(
        private timeout: ng.ITimeoutService,
        private service: CreateNewsService,
        private modalService: ModalMessageService
    ) {

    }

    public submit(): void {
        let data: CreateNews = new CreateNews();

        data.Content = this.content;
        data.Urls = this.imgUrls;

        this.service.createNews(data)
            .success((data: MoveTo) => {
                if (data.IsMoving) {
                    window.location.href = data.Location;
                }
                else {
                    this.modalService.open(
                        'Ошибка при созании новости!',
                        'Упс! Похоже что при загрузке файлов возникла ошибка. Попробуйте ещё раз. Убедитесь что нет файлов с внешних ресурсов так как они могут загружаться с ненадёжного ресурса. Если ошибка не исчезает огбратитесь в техподержку дабы они придумали пытку для разработчика.'
                    );
                }
            });
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

                        this.timeout();
                    }
                }
                else {
                    this.modalService.open('Произошла ошибка при загрузке файла. Убедитесь что файл имеет формат изображения.', 'Ошибка!');
                }
            });
    }
}