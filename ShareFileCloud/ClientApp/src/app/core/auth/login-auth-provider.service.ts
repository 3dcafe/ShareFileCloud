import { Injectable } from '@angular/core';
import { AuthProvider } from './auth-provider.interface';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class LoginAuthProvider implements AuthProvider {


  login(username: string, password: string): Observable<any> {
    console.log(`Attempting login with username: ${username}, password: ${password}`);
    return of(true);
  }

  logout(): void {
    //todo exit from system
  }

  isLoggedIn(): Observable<boolean> {
    return of(true);
  }
}
