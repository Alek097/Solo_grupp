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
        let modalInstance: any = this.uibModal.open({
            animation: true,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '../../../Common/ModalMessage/ModalMessageView.html',
            controller: 'modalMessageController',
            controllerAs: 'message',
            size: 'lg',
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