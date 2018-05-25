import { Component, OnInit, Inject, OnDestroy } from '@angular/core';

import { panoToVRPlayer, stop } from '../pano';
import { MAT_DIALOG_DATA } from '@angular/material';
import { Fisheye } from '@core/models';

@Component({
  selector: 'app-fisheye-dialog',
  templateUrl: './fisheye-dialog.component.html',
  styleUrls: ['./fisheye-dialog.component.scss']
})
export class FisheyeDialogComponent implements OnInit, OnDestroy {

  constructor(@Inject(MAT_DIALOG_DATA) public data: Fisheye) { }

  pano: any;
  ngOnInit() {
    // this.loadScript("assets/panorama/" + this.data.id + "/pano2vr_player.js");
    setTimeout(()=>{
      this.pano = panoToVRPlayer();
      this.pano.readConfigUrl(this.data.xmlFile);
    }, 1000);

    // System.import("assets/panorama/" + this.data.id + "/pano2vr_player.js").then(() => {
    //   let pano = panoToVRPlayer('container');
    //   pano.readConfigUrl(this.data.xmlFile);
    // });
  }

  ngOnDestroy() {
    stop();
  }

  // public loadScript(url) {
  //   console.log('preparing to load...')
  //   let node = document.createElement('script');
  //   node.src = url;
  //   node.type = 'text/javascript';
  //   document.getElementsByTagName('head')[0].appendChild(node);
  //   console.log('success...')
  // }

}
