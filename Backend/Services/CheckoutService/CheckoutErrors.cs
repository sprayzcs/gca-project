namespace CheckoutService;

public static class CheckoutErrors
{
    public const string CartAlreadyOrdered = "Der Warenkorb wurde bereits bestellt";
    public const string InvalidCart = "Der Warenkorb wurde bereits deaktiviert";
    public const string InvalidProduct = "Fehler beim Laden der Produkte aus dem Warenkorb";
    public const string CouldNotEstimateShipment = "Konnte Lieferkosten nicht berrechnen";
    public const string CouldNotDeactivateCart = "Konnte Warenkorb nicht deaktivieren";
}
