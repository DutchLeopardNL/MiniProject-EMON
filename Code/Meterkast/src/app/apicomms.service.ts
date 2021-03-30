import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Datagram } from './entities/Datagram';
import { catchError, map, retry } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
@Injectable({
  providedIn: 'root'
})
export class ApicommsService {
  apiUrl=  'https://localhost:44371/api/Datagram';
  constructor(private http: HttpClient,private toastr: ToastrService) { }

  getSingleDatagram(): Observable<Datagram>
  {
    return this.http.get<Datagram>(`${this.apiUrl}/lastDatagram`).pipe(map((res:any) => res._datagram)).pipe(catchError((err) => {
      this.throwToastrError();
      return throwError(err);
    }));
  }

  getxDatagrams(amount: number): Observable<Datagram[]> {
    return this.http.get<Datagram[]>(`${this.apiUrl}/datagrams/${amount}`).pipe(map((res:any) => res._datagrams)).pipe(catchError((err) => {
      this.throwToastrError();
      return throwError(err);
    }));
  }
  throwToastrError(): void {
    this.toastr.error("API Connection", "Connection refused",
      {
        timeOut: 10 * 1000,
        progressBar: true,
        progressAnimation: 'increasing',
      });
  }
}
