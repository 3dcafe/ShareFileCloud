import { Inject, Injectable } from '@angular/core';
import { AuthProvider } from './auth-provider.interface';
import { Observable, of } from 'rxjs';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})

export class LoginAuthProvider implements AuthProvider {

  //private api: ApiService;

  constructor(/*@Inject('AuthProvider') api: ApiService*/) {
   // this.api = api;
  }

  login(username: string, password: string): Observable<any> {
    console.log(`Attempting login with username: ${username}, password: ${password}`);
  //  this.api.get()
    return of(true);
  }

  logout(): void {
    //todo exit from system
  }

  isLoggedIn(): Observable<boolean> {
    return of(true);
  }
}
