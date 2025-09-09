// Frontend/src/app/components/admin/sales-report/sales-report.component.ts
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CouponService } from '../../../services/coupon.service';
import { ReportService } from '../../../services/report.service';
import { SalesReport } from '../../../../models/sales-report.model';
import { OrderSummary } from '../../../../models/sales-report.model';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-sales-report',
  templateUrl: './sales-report.component.html',
  styleUrls: ['./sales-report.component.scss'],
  standalone:true,
  imports: [CommonModule,ReactiveFormsModule]
 
})
export class SalesReportComponent implements OnInit {
  reportForm: FormGroup;
  salesReport?: SalesReport;
  error = '';
  isLoading = false;

  constructor(
    private fb: FormBuilder,
    private reportService: ReportService
  ) {
    this.reportForm = this.fb.group({
      fromDate: ['', Validators.required],
      toDate: ['', Validators.required]
    });
  }

  ngOnInit(): void {}

  generateReport(): void {
    if (this.reportForm.invalid) return;

    this.isLoading = true;
    this.error = '';

    const { fromDate, toDate } = this.reportForm.value;

    this.reportService.getSalesReport(fromDate, toDate).subscribe({
      next: (report) => {
        this.salesReport = report;
        this.isLoading = false;
      },
      error: () => {
        this.error = 'Failed to fetch sales report.';
        this.isLoading = false;
      }
    });
  }
}
