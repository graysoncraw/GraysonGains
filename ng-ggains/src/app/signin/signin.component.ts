import { Component } from '@angular/core';
import { GoogleSsoDirective } from '../google-sso.directive';

@Component({
  selector: 'app-signin',
  standalone: true,
  imports: [],
  hostDirectives: [GoogleSsoDirective],
  templateUrl: './signin.component.html',
  styleUrl: './signin.component.scss'
})
export class SigninComponent {

}
