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
      .pipe(
        map((response) => response),
        catchError(this.handleError)
      );
    return response;
  }

  // get(id: string | number): Observable<T> {
  //   return this.httpClient.get<T>(`/${this.APIUrl}/${id}`).pipe(
  //     map((json) => this.fromServerModel(json)),
  //     catchError(this.handleError)
  //   );
  // }

  // add(resource: T): Observable<any> {
  //   return this.httpClient
  //     .post(`/${this.APIUrl}`, this.toServerModel(resource))
  //     .pipe(catchError(this.handleError));
  // }

  // delete(id: string | number): Observable<any> {
  //   return this.httpClient
  //     .delete(`/${this.APIUrl}/${id}`)
  //     .pipe(catchError(this.handleError));
  // }

  // update(resource: T) {
  //   return this.httpClient
  //     .put(`/${this.APIUrl}`, this.toServerModel(resource))
  //     .pipe(catchError(this.handleError));
  // }

  private handleError(error: HttpErrorResponse) {
    // Handle the HTTP error here
    return throwError("Something wrong happened");
  }
}
