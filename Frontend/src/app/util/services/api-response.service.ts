import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { NbGlobalPhysicalPosition, NbToastrService } from "@nebular/theme";
import { Store } from "@ngxs/store";
import { catchError, map, Observable, Operator, OperatorFunction, throwError } from "rxjs";
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

    public resolveGet<TResponse>(
        system: string,
        url: string,
        onFinish: () => void,
        failAction: (errors: string[]) => FailAction,
        showErrors: boolean = true): Observable<TResponse | undefined> {
        if(!url.startsWith('/')){
            url = `/${url}`;
        }

        return this.http.get<ApiResponseModel<TResponse>>(system + url)
            .pipe(
                ...this.handle<TResponse>(showErrors, onFinish, failAction),
            );
    }

    public resolvePost<TRequest, TResponse>(
        system: string,
        url: string,
        data: TRequest,
        onFinish: () => void,
        failAction: (errors: string[]) => FailAction,
        showErrors: boolean = true): Observable<TResponse | undefined> {
        if(!url.startsWith('/')){
            url = `/${url}`;
        }

        return this.http.post<ApiResponseModel<TResponse>>(system + url, data)
            .pipe(
                ...this.handle<TResponse>(showErrors, onFinish, failAction),
            );
    }

    public resolvePatch<TRequest, TResponse>(
        system: string,
        url: string,
        data: TRequest,
        onFinish: () => void,
        failAction: (errors: string[]) => FailAction,
        showErrors: boolean = true): Observable<TResponse | undefined> {
        if(!url.startsWith('/')){
            url = `/${url}`;
        }

        return this.http.patch<ApiResponseModel<TResponse>>(system + url, data)
            .pipe(
                ...this.handle<TResponse>(showErrors, onFinish, failAction),
            );
    }

    private handle<TResponse>(showErrors: boolean, onFinish: () => void, failAction: (errors: string[]) => FailAction)
        : [OperatorFunction<any, any>, OperatorFunction<ApiResponseModel<TResponse>, TResponse | undefined>] {
        return [
            catchError(error => {
                if(showErrors){
                    this.showErrorToast(error?.error?.error ?? 'Es ist ein Fehler aufgetreten');
                }
                this.store.dispatch(failAction(error?.error?.error ?? []));
                onFinish();
                return throwError(() => new Error(`Get request failed.`));
            }),
            map((response: ApiResponseModel<TResponse>) => {
                if(!response.success){
                    if(showErrors){
                        this.showErrorToast(response.errors);
                    }
                    this.store.dispatch(failAction(response.errors ?? []));
                    onFinish();
                    return undefined;
                }

                onFinish();
                return response.data;
            })
        ];
    }

    private showErrorToast(errorMessages: string[] | undefined = undefined): void {
        this.toastrService.danger(
            errorMessages?.join(', ') ?? '',
            'Es ist ein Fehler aufgetreten', 
            {
                duration: 5000,
                hasIcon: true,
                icon: 'alert-circle-outline',
                position: NbGlobalPhysicalPosition.BOTTOM_RIGHT
            }
        );
    }

}