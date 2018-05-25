import { Component, OnInit, AfterViewInit } from '@angular/core';
import { MatDialog } from '@angular/material';

import { FisheyeDialogComponent } from '../fisheye-dialog/fisheye-dialog.component';
import { Fisheye } from '@core/models';

import { Configuration } from 'app/app.configuration';

declare let $: any;

@Component({
  selector: 'app-fisheye',
  templateUrl: './fisheye.component.html',
  styleUrls: ['./fisheye.component.scss']
})
export class FisheyeComponent implements OnInit {

  files: Fisheye[] = new Array<Fisheye>();

  constructor(
    public dialog: MatDialog,
    public configuration: Configuration
  ) {

  }

  ngOnInit() {
    this.files.push(new Fisheye(0, 'assets/panorama/0/uot.xml'));
    this.files.push(new Fisheye(1, 'assets/panorama/1/uot.xml'));

    setTimeout(() => {
      $("#highlighted-justified-tab1").summernote();
      $("#highlighted-justified-tab2").summernote();
    }, 0);
  }

  show(order: number) {
    const dialogRef = this.dialog.open(FisheyeDialogComponent, {
      height: '80%',
      width: '80%',
      data: this.files[order]
    });

    dialogRef.afterClosed().subscribe(result => {
      // console.log(`Dialog result`);
    });
  }

}