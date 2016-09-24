export class ModalMessageController {
    public static $inject: string[] = [
        '$uibModalInstance',
        'title',
        'message'
    ];

    constructor(
        private uibModalInstance: any,
        public title: string,
        public message: string
    ) {

    }

    public ok(): void {
        this.uibModalInstance.close();
    }
}