import { BaseEntity } from '../base';
import { FileModel } from '../file.model';

export class User extends BaseEntity {
  public firstName: string;
  public lastName: string;
  public middleName: string;
  public userName: string;
  public email: string;
  public age: number;
  public photo: string;
  public comment: string;
  public postalAddress: string;
  public zIP: number;
  public countryId: number;
  public avatar: FileModel;

  public roles: Array<string>;
}


