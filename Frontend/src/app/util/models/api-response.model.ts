export type ApiResponseModel<T> = {
    success: boolean;
    data: T;
    errors: string[];
}