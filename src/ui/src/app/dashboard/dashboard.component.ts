import { Component, AfterViewInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LocationStrategy } from '@angular/common';

import { initView } from './shared/app';

@Component({
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})

export class DashboardComponent implements AfterViewInit {

  path: string[];
  
  constructor(
    public router: Router,
    public location: LocationStrategy,
  ) {
    this.router.events.subscribe((val) => {
      this.path = this.location.path().slice(1, this.location.path().length).split('/');
      this.path = this.path.length > 3 ? this.path.slice(0, this.path.length - 1) : this.path;
    })
  }

  ngAfterViewInit() {
    initView();
  }

}
