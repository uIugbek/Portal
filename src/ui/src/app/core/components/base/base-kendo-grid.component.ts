import { OnInit } from '@angular/core';
import { State } from '@progress/kendo-data-query';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';
import {
  GridDataResult,
  DataStateChangeEvent
} from '@progress/kendo-angular-grid';

import { Constant } from 'app/app.constant';
import { BaseEntity } from '@core/models';
import {
  BaseEntityService,
  IBaseEntityService
} from '../../services';

export interface IBaseKendoGridComponent<TEntity extends BaseEntity>
  extends OnInit {
  gridData: GridDataResult;
  title: string;
  state: State;

  ngOnInit();

  delete(entity: TEntity);

  getAll();

  dataStateChange(state: DataStateChangeEvent);
}

export class BaseKendoGridComponent<
  TEntity extends BaseEntity,
  TEntityService extends IBaseEntityService<TEntity>> implements IBaseKendoGridComponent<TEntity> {
  public gridData: GridDataResult;
  public title: string;
  // public culture = Constant.cultures;
  public state: State = {
    skip: 0,
    take: Constant.gridPaginationSize
  };

  constructor(
    public dataService: TEntityService,
    public translate: TranslateService
  ) {
    this.title = 'Index';

    this.translate.onLangChange.subscribe((event: LangChangeEvent) => {
      this.getAll();
    });
  }

  ngOnInit() {
    this.getAll();
  }

  onDelete() {

  }

  public delete(entity: TEntity) {
    if (confirm('Do you really want to delete this record?')) {
      this.dataService.deleteById(entity.id).subscribe(
        () => {
          this.getAll();
          this.onDelete();
        },
        error => console.log(error)
      );
    }
  }

  public getAll() {
    this.dataService.getAllAsDataResult(this.state).subscribe(
      data => this.gridData = data,
      error => console.log('& ' + error + ' &')
    );
  }

  public dataStateChange(state: DataStateChangeEvent) {
    this.state = state;
    this.getAll();
  }
}
