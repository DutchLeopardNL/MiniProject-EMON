import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Datagram } from './entities/Datagram';
import { catchError, map, retry } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class ApicommsService {
  apiUrl=  'https://localhost:44371/api/Datagram';
  constructor(private http: HttpClient) { }

  getSingleDatagram(): Observable<Datagram>
  {
    return this.http.get<Datagram>(`${this.apiUrl}/lastDatagram`).pipe(map((res:any) => res._datagram)).pipe(catchError((err) => {
      return throwError(err);
    }));
  }

  getxDatagrams(amount: number): Observable<Datagram[]> {
    return this.http.get<Datagram[]>(`${this.apiUrl}/datagrams/${amount}`).pipe(map((res:any) => res._datagrams)).pipe(catchError((err) => {
      return throwError(err);
    }));
  }
}
