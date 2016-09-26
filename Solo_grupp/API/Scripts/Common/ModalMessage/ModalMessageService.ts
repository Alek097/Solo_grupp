export class ModalMessageService {
    public static $inject: string[] =
    [
        '$uibModal'
    ];

    constructor(
        private uibModal: any
    ) {

    }

    public open(message: string, title: string): void {
        this.uibModal.open({
            animation: true,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Scripts/Common/ModalMessage/ModalMessageView.html',
            controller: 'modalMessageController',
            controllerAs: 'message',
            size: 'lg',
            backdrop: '/Bundles/lib/angular-bootstrap-npm/dist/template/modal/backdrop.html',
            windowTemplateUrl: '/Bundles/lib/angular-bootstrap-npm/dist/template/modal/window.html',
            resolve: {
                title: (): string => {
                    return title;
                },
                message: (): string => {
                    return message;
                }
            }
        });
    }
}