import { Directive, ElementRef, Renderer, OnInit } from "@angular/core";

@Directive({ selector: '[tmFocus]' })

export class MyFocus implements OnInit {
    constructor(public el: ElementRef, public renderer: Renderer) {
        // focus won't work at construction time - too early
    }

    ngOnInit() {
        this.renderer.invokeElementMethod(this.el.nativeElement, 'focus', []);
    }
}