import { BehaviorSubject } from 'rxjs/Rx';
import { Injectable } from '@angular/core';

@Injectable()
export class DataService {
  public dataSource = new BehaviorSubject<any>(null);
  data = this.dataSource.asObservable();
  constructor(
  ) {
  }

  changeData(data: any) {
    console.log(data);
    this.dataSource.next(data);
  }
}
