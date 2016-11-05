import {User} from './User.ts';

export class News {
    public Id: string = null;
    public Content: string = null;
    public Title: string = null;
    public Author: User = null;
    public CreateDate: Date = null;
}