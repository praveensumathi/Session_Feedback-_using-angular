import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs/internal/Observable";
import { throwError } from "rxjs/internal/observable/throwError";
import { catchError, map } from "rxjs/operators";

@Injectable({
  providedIn: "root",
})
export class ResourceService<T> {
  private base = location.origin;

  constructor(private http: HttpClient) {}

  private token = localStorage.getItem("jwt");

  BuildRequest(path: string, params: any = {}): string {
    const url: URL = new URL(path, this.base);
    Object.keys(params).forEach((key) =>
      url.searchParams.append(key, params[key])
    );
    return url.href;
  }

  getAll(url: string, urlParams: any = {}): Observable<T[]> {
    var request: string = this.BuildRequest(url, urlParams);

    var response: Observable<T[]>;
    response = this.http
      .get<T[]>(request, {
        headers: new HttpHeaders({
          "Content-Type": "application/json",
          Authorization: `Bearer ${this.token}`,
        }),
      })
      .pipe(catchError(this.handleError));
    return response;
  }

  getById(url: string, urlParams: any = {}): Observable<T> {
    var request: string = this.BuildRequest(url, urlParams);

    return this.http
      .get<T>(request, {
        headers: new HttpHeaders({
          "Content-Type": "application/json",
          Authorization: `Bearer ${this.token}`,
        }),
      })
      .pipe(catchError(this.handleError));
  }

  add(url: string, data: any, urlParams: any = {}): Observable<T> {
    var request: string = this.BuildRequest(url, urlParams);
    return this.http
      .post<T>(request, data, {
        headers: new HttpHeaders({
          "Content-Type": "application/json",
          Authorization: `Bearer ${this.token}`,
        }),
      })
      .pipe(catchError(this.handleError));
  }

  delete(
    url: string,
    data: any = null,
    urlParams: any = {}
  ): Observable<boolean> {
    var request: string = this.BuildRequest(url, urlParams);

    return this.http
      .delete<boolean>(request, {
        headers: new HttpHeaders({
          "Content-Type": "application/json",
          Authorization: `Bearer ${this.token}`,
        }),
      })
      .pipe(catchError(this.handleError));
  }

  update(url: string, data: any, urlParams: any = {}): Observable<boolean> {
    var request: string = this.BuildRequest(url, urlParams);

    return this.http
      .put<boolean>(request, data, {
        headers: new HttpHeaders({
          "Content-Type": "application/json",
          Authorization: `Bearer ${this.token}`,
        }),
      })
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    // Handle the HTTP error here
    return throwError("Something wrong happened");
  }
}
