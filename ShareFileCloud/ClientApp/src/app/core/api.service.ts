import { Injectable } from '@angular/core';
import { ApiProvider } from './api-provider.interface';


@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiProvider: ApiProvider;
  
  constructor(apiProvider: ApiProvider) {
    this.apiProvider = apiProvider;
  }

  get(endpoint: string) {
    return this.apiProvider.get(endpoint);
  }
}
