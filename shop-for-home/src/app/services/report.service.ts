// Frontend/src/app/services/report.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { SalesReport } from '../../models/sales-report.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReportService {
  private apiUrl = 'https://localhost:5001/api/reports';

  constructor(private http: HttpClient) {}

  getSalesReport(fromDate: string, toDate: string): Observable<SalesReport> {
    const params = new HttpParams()
      .set('fromDate', fromDate)
      .set('toDate', toDate);
    return this.http.get<SalesReport>(`${this.apiUrl}/sales`, { params });
  }
}
