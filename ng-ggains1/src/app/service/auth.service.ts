import { Injectable } from '@angular/core';
import {
  Auth,
  browserSessionPersistence,
  createUserWithEmailAndPassword,
  getIdToken,
  GoogleAuthProvider,
  signInWithEmailAndPassword,
  signInWithPopup,
  signOut,
  user,
  User,
} from '@angular/fire/auth';
import { setPersistence } from 'firebase/auth';
import { from } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  //user$: Observable<User | null>;

  constructor(private firebaseAuth: Auth) {
    this.setSessionStoragePersistence();
    //this.user$ = user(this.firebaseAuth);
  }

  private setSessionStoragePersistence(): void {
    setPersistence(this.firebaseAuth, browserSessionPersistence);
  }

  async login(email: string, password: string): Promise<string> {
    try{
      const userCred = await signInWithEmailAndPassword(this.firebaseAuth, email, password);
      const user = userCred.user;
      if (!user) {
        throw new Error('Login error');
      }
      return await getIdToken(user);
    }
    catch(error){
      console.error('Login error:', error);
      throw error;
    }
  }

  async googleLogin(): Promise<string> {
    const provider = new GoogleAuthProvider();
    try {
      const userCred = await signInWithPopup(this.firebaseAuth, provider);
      const user = userCred.user;
      if (!user) {
        throw new Error('Google-Login error');
      }
      return await getIdToken(user);
    } catch (error) {
      console.error('Google-Login error:', error);
      throw error;
    }
  }

  logout() {
    const logout = signOut(this.firebaseAuth).then(() => {
      sessionStorage.clear();
    });
    return from(logout);
  }

  async getIdToken(): Promise<string | null> {
    const currentUser = this.firebaseAuth.currentUser;
    return currentUser ? await getIdToken(currentUser) : null;
  }
}