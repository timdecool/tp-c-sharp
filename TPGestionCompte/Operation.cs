namespace TPGestionCompte;

public enum Mouvement
{
    Credit,
    Debit
}

public class Operation
{
    #region attributes
    private decimal montant;
    private Mouvement type;
    #endregion
    
    #region properties
    public decimal Montant { get => montant; set => montant = value; }
    public Mouvement Type { get => type; set => type = value; }
    #endregion
    
    #region constructors

    public Operation()
    {
        
    }

    public Operation(decimal montant, Mouvement type)
    {
        this.montant = montant;
        this.type = type;
    }
    #endregion
}