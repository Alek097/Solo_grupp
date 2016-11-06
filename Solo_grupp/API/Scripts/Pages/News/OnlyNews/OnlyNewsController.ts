import {News} from '../../../Common/Models/News.ts'
import {Comment} from '../../../Common/Models/Comment.ts'
import {ControllerResult} from '../../../Common/Models/ControllerResult.ts'
import {OnlyNewsService} from './OnlyNewsService.ts'
import {ModalMessageService} from '../../../Common/ModalMessage/ModalMessageService.ts'

export class OnlyNewsController {
    public static $inject: string[] =
    [
        'onlyNewsService',
        '$routeParams',
        'modalMessageService',
        '$sce'
    ];

    public model: News;
    public isComments: boolean = false;

    constructor(
        private service: OnlyNewsService,
        params: ng.route.IRouteParamsService,
        private modalService: ModalMessageService,
        sce: ng.ISCEService
    ) {

        let id: string = params['id'];

        if (id == undefined || id === '') {
            location.href = '/#/Home';
        }

        this.service.GetNews(id)
            .success((data: ControllerResult<News>) => {
                if (data.IsSucces) {
                    this.model = data.Value;
                    this.model.Content = sce.trustAsHtml(data.Value.Content);

                    this.service.GetComments(this.model.Id)
                        .success((data: ControllerResult<Comment[]>) => {
                            if (data.IsSucces) {
                                this.model.Comments = data.Value;

                                this.isComments = true;

                            }
                        });
                }
                else {
                    location.href = '/#/Home';

                    this.modalService.open(data.Message,
                        'Упс!');
                }
            });
    }
}