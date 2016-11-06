import {User} from './User.ts';
import {Comment} from './Comment.ts';

export class News {
    public Id: string = null;
    public Content: string = null;
    public Title: string = null;
    public Author: User = null;
    public CreateDate: Date = null;
    public Comments: Comment[] = null;
}