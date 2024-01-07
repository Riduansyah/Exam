import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { throwError } from 'rxjs';
import { UserModel } from 'src/app/interface/userModel';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent {
  registerForm!: FormGroup;

  constructor(private formbulider: FormBuilder, public apiService: ApiService, private toastr: ToastrService) { 

  }
  
  ngOnInit(): void {
    this.registerForm = this.formbulider.group({
      userName: new FormControl(''),
      firstName: new FormControl(''),
      lastName: new FormControl(''),
      userEmail: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required])
    });
  }

/*   public validateControl = (controlName: string) => {
    return this.registerForm!.get(controlName).invalid && this.registerForm!.get(controlName).touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.registerForm.get(controlName).hasError(errorName)
  } */

  public registerUser = (registerFormValue: any) => {
    const formValues = { ...registerFormValue };

    const user: UserModel = {
      userName: formValues.userName,
      firstName: formValues.firstName,
      lastName: formValues.lastName,
      userEmail: formValues.email,
      password: formValues.password
    };

    this.apiService.addUser(user)
    .subscribe({
      next: () => {
        this.toastr.success('Registrasi berhasil', 'Sukses');
      },
      error: (error: any) => {
        this.toastr.error('Terjadi kesalahan', 'Error');
        return throwError(() => error);
      },
    });
  }
}
