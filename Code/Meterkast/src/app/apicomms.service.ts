import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Datagram } from './entities/Datagram';
import { catchError, map, retry } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
import { Temprature } from './entities/Temprature';
@Injectable({
  providedIn: 'root'
})
export class ApicommsService {
  apiUrlDatagram =  'https://localhost:44371/api/Datagram';
  apiUrlTemprature= 'https://localhost:44371/api/Temprature';
  constructor(private http: HttpClient,private toastr: ToastrService) { }

  getSingleDatagram(): Observable<Datagram>
  {
    return this.http.get<Datagram>(`${this.apiUrlDatagram}/lastDatagram`).pipe(map((res:any) => res._datagram)).pipe(catchError((err) => {
      this.throwToastrError();
      return throwError(err);
    }));
  }
  getSingleTemprature(): Observable<Temprature>
  {
    return this.http.get<Temprature>(`${this.apiUrlTemprature}/lastTemprature`).pipe(map((res:any)=> res._temprature)).pipe(catchError((err) => {
      this.throwToastrError();
      return throwError(err);
    }));
  }

  getxTempratures(amount:number): Observable<Temprature[]>{
    return this.http.get<Temprature>(`${this.apiUrlTemprature}/tempratures/${amount}`).pipe(map((res:any)=> res._tempratures)).pipe(catchError((err) => {
      this.throwToastrError();
      return throwError(err);
    }));
  }
    
  getxDatagrams(amount: number): Observable<Datagram[]> {
    return this.http.get<Datagram[]>(`${this.apiUrlDatagram}/datagrams/${amount}`).pipe(map((res:any) => res._datagrams)).pipe(catchError((err) => {
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
