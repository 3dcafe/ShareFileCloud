import { Injectable } from '@angular/core';
import { AuthProvider } from './auth-provider.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SolidAuthProvider implements AuthProvider {


  login(username: string, password: string): Observable<any> {
    return true;
  }

  logout(): void {
    //todo exit from system
  }

  isLoggedIn(): Observable<boolean> {
    return true;
  }
}
