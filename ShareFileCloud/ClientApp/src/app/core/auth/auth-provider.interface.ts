import { Observable } from "rxjs";

export interface AuthProvider {
  login(username: string, password: string): Observable<any>;
  logout(): void;
  isLoggedIn(): Observable<boolean>;
}
