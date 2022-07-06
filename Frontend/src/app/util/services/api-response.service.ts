import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { NbGlobalPhysicalPosition, NbToastrService } from "@nebular/theme";
import { Store } from "@ngxs/store";
import { catchError, map, Observable, throwError } from "rxjs";
import { FailAction } from "src/app/store/actions/base.actions";
import { ApiResponseModel } from "../models/api-response.model";

@Injectable({
    providedIn: 'root'
})
export class ApiResponseService {

    constructor(
        private readonly http: HttpClient,
        private readonly toastrService: NbToastrService,
        private readonly store: Store
    ) { }

    public resolveGet<T>(
        system: string,
        url: string,
        onFinish: Function,
        failAction?: (errors: string[]) => FailAction,
        showErrors: boolean = true): Observable<T | undefined> {
        if(!url.startsWith('/')){
            url = `/${url}`;
        }

        return this.http.get<ApiResponseModel<T>>(system + url)
            .pipe(
                catchError(error => {
                    if(showErrors){
                        this.showErrorToast(error.error.error);
                    }
                    this.store.dispatch(failAction);
                    onFinish();
                    return throwError(() => new Error(`Get request to ${system + url} failed.`));
                }),
                map(response => {
                    if(!response.success){
                        if(showErrors){
                            this.showErrorToast(response.errors);
                        }
                        this.store.dispatch(failAction);
                        onFinish();
                        return undefined;
                    }

                    onFinish();
                    return response.data;
                })
            );
    }

    public resolvePost<TRequest, TResponse>(
        system: string,
        url: string,
        data: TRequest,
        onFinish: Function,
        failAction?: (errors: string[]) => FailAction,
        showErrors: boolean = true): Observable<TResponse | undefined> {
        if(!url.startsWith('/')){
            url = `/${url}`;
        }

        return this.http.post<ApiResponseModel<TResponse>>(system + url, data)
            .pipe(
                catchError(error => {
                    if(showErrors){
                        this.showErrorToast(error?.error?.error ?? 'Es ist ein Fehler aufgetreten');
                    }
                    this.store.dispatch(failAction);
                    onFinish();
                    return throwError(() => new Error(`Get request to ${system + url} failed.`));
                }),
                map(response => {
                    if(!response.success){
                        if(showErrors){
                            this.showErrorToast(response.errors);
                        }
                        this.store.dispatch(failAction);
                        onFinish();
                        return undefined;
                    }

                    onFinish();
                    return response.data;
                })
            );
    }

    private showErrorToast(errorMessages: string[] | undefined = undefined): void {
        this.toastrService.danger(errorMessages?.join(', ') ?? '', 'Es ist ein Fehler aufgetreten', {
            duration: 5,
            hasIcon: true,
            icon: 'alert-circle-outline',
            position: NbGlobalPhysicalPosition.BOTTOM_RIGHT
        });
    }

}