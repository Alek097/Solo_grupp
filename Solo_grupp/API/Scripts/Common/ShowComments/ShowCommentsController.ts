import {Comment} from '../Models/Comment'

export class ShowCommentsController {
    public isAuth: boolean = false;
    public isRender: boolean = false;
    public Comments: Comment[] = [];
    public innerLevel: number = 0;

    public send(id: string): void {
        
    }

}