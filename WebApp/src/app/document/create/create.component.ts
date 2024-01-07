import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { throwError } from 'rxjs';
import { environment } from "src/environments/environment";

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent {
  status: 'initial' | 'uploading' | 'success' | 'fail' = 'initial';
  file: File | null = null;

  constructor(private router: Router, private http: HttpClient, private toastr: ToastrService) {}

  ngOnInit(): void {}

  onChange(event: any) {
    const file: File = event.target.files[0];

    if (file) {
      this.status = 'initial';
      this.file = file;
    }
  }

  onUpload() {
    if (this.file) {
      const formData = new FormData();

      formData.append('file', this.file, this.file.name);
      formData.append('note', 'exam');
      const upload$ = this.http.post(`${environment.apiUrl}api/document/uploadFile`, formData);
      this.status = 'uploading';

      upload$.subscribe({
        next: () => {
          this.status = 'success';
          this.toastr.success('Dokumen telah berhasil di unggah', 'Sukses');
          this.router.navigate(["/document/list"]);
        },
        error: (error: any) => {
          this.status = 'fail';
          this.toastr.error('Gagal  mengunggah dokumen', 'Error');
          return throwError(() => error);
        },
      });
    }
  }
}
