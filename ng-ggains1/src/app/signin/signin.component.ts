import { Component, inject } from '@angular/core';
import {
  FormsModule,
  Validators,
  ReactiveFormsModule,
  FormBuilder,
} from '@angular/forms';
import { AuthService } from '../service/auth.service';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-user-login',
  standalone:true,
  imports: [FormsModule, ReactiveFormsModule, RouterLink],
  templateUrl: './signin.component.html',
  styleUrl: './signin.component.scss'
})
export class SigninComponent {
  error: boolean = false;
  fb: FormBuilder = inject(FormBuilder);
  authService: AuthService = inject(AuthService);
  router: Router = inject(Router);
  form = this.fb.nonNullable.group({
    email: [
      '',
      [
        Validators.required,
        Validators.pattern(/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/),
      ],
    ],
    password: ['', Validators.required],
  });

  async onSubmit(): Promise<void> {
    const rawForm = this.form.getRawValue();
    try {
      const token = await this.authService.login(rawForm.email, rawForm.password);
      console.log('Email/Password Sign-In successful, token:', token);
    }
    catch (error) {
      this.error = true;
      console.error('Email/Password Sign-In error:', error);
    }
    // this.authService.login(rawForm.email, rawForm.password).subscribe({
    //   next: () => {
    //     this.router.navigateByUrl('/protected-content');
    //   },
    //   error: (error) => {
    //     this.error = true;
    //     console.error('Email/Password Sign-In error:', error);
    //   },
    // });
  }

  async onGoogleSignIn(): Promise<void> {
    try {
      const token = await this.authService.googleLogin();
      console.log('Google Sign-In successful, token:', token);
      //this.router.navigateByUrl('/main');
    } catch (error) {
      console.error('Google Sign-In error:', error);
    }
  }  
}