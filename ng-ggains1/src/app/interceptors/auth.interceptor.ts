import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Auth } from '@angular/fire/auth';
import { from } from 'rxjs';
import { switchMap } from 'rxjs/operators';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const auth = inject(Auth);
  return from(auth.currentUser?.getIdToken() ?? Promise.resolve(null)).pipe(
    switchMap(token => {
      if (token) {
        req = req.clone({ setHeaders: {Authorization: `Bearer ${token}`} });
      }
      return next(req);
    }) 
  );
};
