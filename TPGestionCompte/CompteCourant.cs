namespace TPGestionCompte;

public class CompteCourant : Compte
{
    private decimal _decouvertAutorise;
    public decimal DecouvertAutorise { get => _decouvertAutorise; init => _decouvertAutorise = value; }

    public CompteCourant(string proprietaire, decimal decouvertAutorise)
    {
        this.proprietaire = proprietaire;
        _decouvertAutorise = decouvertAutorise;
    }

    protected override decimal CalculBenefice()
    {
        return 0;
    }
    
    public override void Information()
    {
        Console.WriteLine($"INFORMATIONS DU COMPTE COURANT DE {proprietaire.ToUpper()}");
        Console.WriteLine("==========================================");
        Console.WriteLine($"Solde du compte : {Solde:C}");
        Console.WriteLine($"Découvert autorisé : {_decouvertAutorise:C}");
        
        Console.WriteLine("OPÉRATIONS SUR LE COMPTE");
        Console.WriteLine("------------------------");
        foreach (Operation operation in operations)
        {
            Console.WriteLine($"{(operation.Type == Mouvement.Credit ? "+":"-")}{operation.Montant:C}");
        }
    }

}