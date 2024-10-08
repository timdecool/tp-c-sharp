namespace TPGestionCompte;

public abstract class Compte : IComparable<Compte>
{
    protected List<Operation> operations = new List<Operation>();
    protected string proprietaire;
    
    public string Proprietaire { get => proprietaire; set => proprietaire = value; }
    public decimal Solde { 
        get
        {
            decimal solde = 0;
            foreach (Operation op in operations)
            {
                if (op.Type == Mouvement.Credit)
                {
                    solde += op.Montant;
                }
                else
                {
                    solde -= op.Montant;
                }
            }

            return solde;
        } 
    }
    
    public Compte()
    {
        
    }

    public Compte(string proprietaire)
    {
        this.proprietaire = proprietaire;
    }
    
    public void Crediter(decimal montant)
    {
        operations.Add(new Operation(montant, Mouvement.Credit));
    }

    public void Debiter(decimal montant)
    {
        operations.Add(new Operation(montant, Mouvement.Debit));
    }

    public void Crediter(decimal montant, Compte compteADebiter)
    {
        Crediter(montant);
        compteADebiter.Debiter(montant);
    }

    public void Debiter(decimal montant, Compte compteACrediter)
    {
        Debiter(montant);
        compteACrediter.Crediter(montant);
    }

    protected abstract decimal CalculBenefice();

    public decimal SoldeFinal()
    {
        return Solde + CalculBenefice();
    }

    public virtual void Information()
    {
        Console.WriteLine($"INFORMATIONS DU COMPTE DE {proprietaire.ToUpper()}");
        Console.WriteLine("==========================================");
        Console.WriteLine($"Solde du compte : {Solde:C}");
        
        Console.WriteLine("OPÉRATIONS SUR LE COMPTE");
        Console.WriteLine("------------------------");
        foreach (Operation operation in operations)
        {
            Console.WriteLine($"{(operation.Type == Mouvement.Credit ? "+":"-")}{operation.Montant:C}");
        }
        Console.WriteLine("*************************");
    }
    
    public int CompareTo(Compte? autreCompte)
    {
        if (autreCompte == null)
        {
            return 1;
        }
        
        return this.Solde.CompareTo(autreCompte.Solde);
    }
}