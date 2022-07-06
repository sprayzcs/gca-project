export class UrlBuilderService {

    public static buildQueryStringWithArray(parameterName: string, items: unknown[]){
        return `?${parameterName}=` + items.join(`&${parameterName}=`);
    }

}