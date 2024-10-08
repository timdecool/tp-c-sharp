namespace TPGestionCompte;

public class GestionDeComptes
{
    internal List<Compte> comptes = new List<Compte>();

    public void AjouterCompte(Compte compte)
    {
        comptes.Add(compte);
    }

    public void AfficherComptesCourant()
    {
        Console.WriteLine("Tous les comptes courants : ");
        Console.WriteLine("----------------------------");
        foreach (Compte compte in comptes)
        {
            if (compte.GetType().Name == "CompteCourant")
            {
                compte.Information();
            }
        }
    }

    public void AfficherComptesEpargne()
    {
        Console.WriteLine("Tous les comptes épargne : ");
        Console.WriteLine("---------------------------");
        foreach (Compte compte in comptes)
        {
            if (compte.GetType().Name == "CompteEpargne")
            {
                compte.Information();
            }
        }
    }
}