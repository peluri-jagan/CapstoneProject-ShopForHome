// Frontend/src/app/components/admin/bulk-upload/bulk-upload.component.ts
import { Component } from '@angular/core';
import { HttpClient, HttpEventType } from '@angular/common/http'; 
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-bulk-upload',
  templateUrl: './bulk-upload.component.html',
  styleUrls: ['./bulk-upload.component.scss'],
  standalone: true,
  imports: [CommonModule],
})
export class BulkUploadComponent {
  selectedFile?: File;
  uploadProgress = 0;
  message = '';

  constructor(private http: HttpClient) {}

  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0];
  }

  onUpload(): void {
    if (!this.selectedFile) {
      this.message = 'Please select a file first';
      return;
    }
    const formData = new FormData();
    formData.append('file', this.selectedFile);

    this.http.post('http://localhost:5263/api/bulkupload/products', formData, {
      reportProgress: true,
      observe: 'events'
    }).subscribe(event => {
      if (event.type === HttpEventType.UploadProgress && event.total) {
        this.uploadProgress = Math.round(100 * event.loaded / event.total);
      } else if (event.type === HttpEventType.Response) {
        this.message = 'Upload successful';
      }
    }, () => {
      this.message = 'Upload failed';
    });
  }
}
