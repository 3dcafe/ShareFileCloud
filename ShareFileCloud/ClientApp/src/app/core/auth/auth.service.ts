import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private apiUrl: string = 'https://example.com/auth';
  private loggedInSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  constructor(private http: HttpClient) { }

  private getHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json',
    });
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error);
    return Promise.reject(error.message || error);
  }

  login(username: string, password: string): Observable<any> {
    const url = `${this.apiUrl}/login`;
    const headers = this.getHeaders();
    const body = { username, password };

    return this.http.post(url, body, { headers })
      .pipe(
        map(response => {
          this.loggedInSubject.next(true);
          return response;
        }),
        catchError(this.handleError)
      );
  }

  logout(): void {
    this.loggedInSubject.next(false);
  }

  isLoggedIn(): Observable<boolean> {
    return this.loggedInSubject.asObservable();
  }
}
