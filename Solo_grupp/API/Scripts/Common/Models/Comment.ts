import {User} from './User.ts'

export class Comment {
    public Id: string = null;
    public Text: string = null;
    public CreateDate: Date = null;
    public Author: User = null;
    public Comments: Comment[] = [];
}