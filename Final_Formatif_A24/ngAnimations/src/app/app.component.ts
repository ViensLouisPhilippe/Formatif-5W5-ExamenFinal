import { Component } from '@angular/core';
import { bounce, shakeX, tada } from 'ng-animate';
import { trigger, transition, useAnimation } from '@angular/animations';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  animations: [
    trigger('shake', [transition('* => *', useAnimation(shakeX, { params: { timing: 2 } }))]),
    trigger('bounce', [transition('* => *', useAnimation(bounce, { params: { timing: 4 } }))]),
    trigger('tada', [transition('* => *', useAnimation(tada, { params: { timing: 3 } }))]),
  ],
})
export class AppComponent {
  title = 'ngAnimations';

  shakeState = false;
  bounceState = false;
  tadaState = false;

  isRotating = false;
  stopAnimations = false; // Arrête uniquement les animations Angular

  animateOnce() {
    this.stopAnimations = false;
    this.shakeState = true;

    setTimeout(() => {
      if (this.stopAnimations) return;
      this.shakeState = false;
      this.bounceState = true;

      setTimeout(() => {
        if (this.stopAnimations) return;
        this.bounceState = false;
        this.tadaState = true;

        setTimeout(() => {
          if (this.stopAnimations) return;
          this.tadaState = false;
        }, 3000);
      }, 3000);
    }, 2000);
  }

  animateLoop() {
    this.stopAnimations = false;

    const loop = () => {
      if (this.stopAnimations) return;
      this.animateOnce();

      setTimeout(() => {
        if (!this.stopAnimations) loop();
      }, 9000);
    };

    loop();
  }

  rotate() {
    this.isRotating = true;

    setTimeout(() => {
      this.isRotating = false;
    }, 2000);
  }

  stopAnimation() {
    this.stopAnimations = true;
    this.shakeState = false;
    this.bounceState = false;
    this.tadaState = false;
  }
}
