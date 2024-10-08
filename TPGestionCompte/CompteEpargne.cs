namespace TPGestionCompte;

public class CompteEpargne : Compte
{
    private decimal tauxAbondement;
    public decimal TauxAbondement
    {
        get => tauxAbondement;
        init => tauxAbondement = value;
    }

    public CompteEpargne(string proprietaire, decimal tauxAbondement)
    {
        this.proprietaire = proprietaire;
        this.tauxAbondement = tauxAbondement;
    }

    protected override decimal CalculBenefice()
    {
        return Solde + Solde * tauxAbondement;
    }
    
    public override void Information()
    {
        Console.WriteLine($"INFORMATIONS DU COMPTE ÉPARGNE DE {proprietaire.ToUpper()}");
        Console.WriteLine("==========================================");
        Console.WriteLine($"Solde du compte : {Solde:C}" );
        Console.WriteLine($"Taux d'abondement : {tauxAbondement:C}");
        
        Console.WriteLine("OPÉRATIONS SUR LE COMPTE");
        Console.WriteLine("------------------------");
        foreach (Operation operation in operations)
        {
            Console.WriteLine($"{(operation.Type == Mouvement.Credit ? "+":"-")}{operation.Montant:C}");
        }
    }
}