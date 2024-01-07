import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { throwError } from 'rxjs';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent {
  documentList: any[] = [];

  constructor(public apiService: ApiService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.apiService.getAllDocument().subscribe({
      next: result => {
        this.documentList = result;
      },
      error: (error: any) => {
        this.toastr.error('Terjadi kesalahan', 'Error');
        return throwError(() => error);
      },
    });
  }
}
