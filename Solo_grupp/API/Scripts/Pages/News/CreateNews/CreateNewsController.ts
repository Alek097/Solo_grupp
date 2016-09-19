export class CreateNewsController {

    public static $inject: string[] =
    [
        '$timeout'
    ];

    public imgUrls: string[] = [];
    public content: string = null;

    constructor(
        private timeout: ng.ITimeoutService
    ) {

    }

    public submit(): void {
        let input: JQuery = angular.element('input[type=file]');

        let data = new FormData();

        for (let i: number = 0; i < input.length; i++) {
            data.append('file' + i, input[i]);
        }
    }
}