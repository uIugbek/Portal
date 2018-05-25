import { OnInit, AfterContentInit, AfterViewInit } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute, Params } from '@angular/router';

import { BaseEntity } from '@core/models';
import { IBaseEntityService } from '@core/services';
import { Constant } from 'app/app.constant';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

export interface IBaseAddUpdateComponent<TEntity extends BaseEntity>
  extends OnInit,
    AfterViewInit,
    AfterContentInit {
  id: any;
  isNew: boolean;
  entity: TEntity;

  ngOnInit();

  ngAfterViewInit();

  ngAfterContentInit();

  onAdd();

  onUpdate();

  addOrUpdate();

  add();

  update();

  goBack();
}

export class BaseAddUpdateComponent<
  TEntity extends BaseEntity,
  TEntityService extends IBaseEntityService<TEntity>
> implements IBaseAddUpdateComponent<TEntity> {
  public id: any;
  public isNew: boolean;
  public entity: TEntity;

  constructor(
    public dataService: TEntityService,
    public location: Location,
    public route: ActivatedRoute,
    public $toastr?: ToastrService,
    public $translate?: TranslateService
  ) {}

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = +params['id'];
    });

    this.isNew = !this.id;

    if (!this.isNew) {
      this.route.params
        .switchMap((params: Params) => this.dataService.getById(+params['id']))
        .subscribe(
          (entity: TEntity) => {
            this.entity = entity;
            this.onGetByIdComplete();
          },
          err => console.error()
        );
    }
  }
  ngAfterViewInit() {}

  ngAfterContentInit() {}

  onGetByIdComplete() {}

  onAdd() {}

  onUpdate() {}

  public addOrUpdate() {
    if (this.isNew) {
      this.add();
    } else {
      this.update();
    }
  }

  public add() {
    this.dataService.add(this.entity).subscribe(
      () => {
        this.onAdd();
        this.goBack();
      },
      error => {
        for (const key in error.error) {
          if (error.error.hasOwnProperty(key)) {
            const elem = error.error[key];
            console.log(elem);
            this.$toastr.error(this.$translate.instant('errors.' + elem), key);
          }
        }
      }
    );
  }

  public update() {
    this.dataService.update(this.entity.id, this.entity).subscribe(
      () => {
        this.onUpdate();
        this.goBack();
      },
      error => {
        for (const key in error.error) {
          if (error.error.hasOwnProperty(key)) {
            const elem = error.error[key];
            console.log(elem);
            this.$toastr.error(this.$translate.instant('errors.' + elem), key);
          }
        }
      }
    );
  }

  goBack(): void {
    this.location.back();
  }
}
