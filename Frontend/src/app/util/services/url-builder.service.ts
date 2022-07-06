export class UrlBuilderService {

    public static buildQueryStringWithArray(parameterName: string, items: unknown[]){
        if(items.length == 0){
            return '';
        }
        return `?${parameterName}=` + items.join(`&${parameterName}=`);
    }

}