import { Observable } from "rxjs/internal/Observable";

export interface ApiProvider {
  get(endpoint: string): Observable<any>;
}
