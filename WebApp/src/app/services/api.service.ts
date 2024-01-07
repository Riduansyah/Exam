import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { UserModel } from "../interface/userModel";

@Injectable({
    providedIn: 'root'
  })
  export class ApiService {
    private apiURL = "http://localhost:50016/api";

    constructor(private http: HttpClient) { }


    addUser(body: UserModel){
      return this.http.post<any>(`${environment.apiUrl}api/addUser`,body);
    }

    getAllDocument(): Observable<any[]> {
      return this.http.get<any[]>(`${environment.apiUrl}api/document/getAll`);
    }

  }