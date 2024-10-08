// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using TPGestionCompte;

GestionDeComptes gestion = new GestionDeComptes();
bool exit = false;
while (!exit)
{
    Console.Clear();
    Console.WriteLine("""
            GESTION DES COMPTES
            ===================
            Choisissez une action (entrez le chiffre associé) :
            1. Créer un compte courant
            2. Créer un compte épargne
            3. Créditer un compte
            4. Débiter un compte
            5. Effectuer un virement
            6. Afficher la liste des comptes
            7. Trier les comptes
            8. Quitter
            """);
    
    string? option = Console.ReadLine();
    int optionNumber = string.IsNullOrEmpty(option) || !"123456789".Contains(option) || int.Parse(option) > 8 ? 0 : int.Parse(option);
    switch (optionNumber)
    {
        case 1:
            CreerCompteCourant();
            break;
        case 2:
            CreerCompteEpargne();
            break;
        case 3:
            CrediterCompte();
            break;
        case 4:
            DebiterCompte();
            break;
        case 5:
            EffectuerVirement();
            break;
        case 6:
            AfficherComptes();
            break;
        case 7:
            TrierComptes();
            break;
        case 8:
            exit = true;
            Console.WriteLine("Au revoir !");
            break;
    }
}

void CreerCompteCourant()
{
    string? proprietaire = null;
    decimal decouvertAutorise = 0;
    
    AfficherTitreSection("créer un compte courant");
    while (proprietaire == null || !Regex.IsMatch(proprietaire, "^[a-zA-Z]+$"))
    {
        Console.WriteLine("Propriétaire du compte : ");
        proprietaire = Console.ReadLine();
    }

    while (decouvertAutorise < 1)
    {
        Console.WriteLine("Découvert Autorisé : ");
        string? decouvertEntre = Console.ReadLine();
        decouvertAutorise = !string.IsNullOrEmpty(decouvertEntre) && Regex.IsMatch(decouvertEntre, "^[0-9]+$") ? decimal.Parse(decouvertEntre) : 0;
    }
    
    gestion.AjouterCompte(new CompteCourant(proprietaire, decouvertAutorise));
}

void CreerCompteEpargne()
{
    string? proprietaire = null;
    decimal tauxAbondement = 0;
    AfficherTitreSection("créer un compte épargne");
    
    while (proprietaire == null || !Regex.IsMatch(proprietaire, "^[a-zA-Z]+$"))
    {
        Console.WriteLine("Propriétaire du compte : ");
        proprietaire = Console.ReadLine();
    }
    
    while (tauxAbondement <= 0)
    {
        Console.WriteLine("Taux d'abondement pratiqué : ");
        string? tauxAbondementEntre = Console.ReadLine();
        tauxAbondement = !string.IsNullOrEmpty(tauxAbondementEntre) && Regex.IsMatch(tauxAbondementEntre, "^[0-9.,]+$") ? decimal.Parse(tauxAbondementEntre) : 0;
    }
    
    gestion.AjouterCompte(new CompteEpargne(proprietaire, tauxAbondement));

}

void CrediterCompte()
{
    AfficherTitreSection("créditer un compte");
    Compte? compte = TrouverCompte();
    if (compte != null)
    {
        decimal montant = EnregistrerMontant();
        compte.Crediter(montant);
    }
}

void DebiterCompte()
{
    AfficherTitreSection("débiter un compte");
    Compte? compte = TrouverCompte();
    if (compte != null)
    {
        decimal montant = EnregistrerMontant();
        compte.Debiter(montant);
    }
}

void EffectuerVirement()
{
    AfficherTitreSection("effectuer un virement entre deux comptes");
    Console.WriteLine("COMPTE À DÉBITER : ");
    Compte compteADebiter = TrouverCompte();
    Console.WriteLine("COMPTE À CRÉDITER : ");
    Compte compteACrediter = TrouverCompte();
    decimal montant = EnregistrerMontant();
    
    compteADebiter.Debiter(montant, compteACrediter);
}

void AfficherComptes()
{
    AfficherTitreSection("afficher la liste des comptes");
    Console.WriteLine("Souhaitez-vous afficher les comptes courants (C) ou les comptes épargne (E) ?");
    string? option = Console.ReadLine();
    if (option == "C")
    {
        gestion.AfficherComptesCourant();
    }
    else if (option == "E")
    {
        gestion.AfficherComptesEpargne();
    }
    AppuyerPourContinuer();
    
}

void TrierComptes()
{
    AfficherTitreSection("trier la liste des comptes");
    gestion.comptes.Sort();
    Console.WriteLine("Affichage des comptes triés : ");
    foreach (Compte compte in gestion.comptes)
    {
        compte.Information();
    }
    AppuyerPourContinuer();
    
}

void AfficherTitreSection(string titre)
{
    Console.Clear();
    Console.WriteLine(titre.ToUpper());
    Console.WriteLine("========================");
}

Compte TrouverCompte()
{
    Console.WriteLine("Indiquer le propriétaire du compte : ");
    string? proprietaire = Console.ReadLine();
    
    List<Compte> comptes = gestion.comptes.FindAll(compte => compte.Proprietaire == proprietaire);
    if (comptes.Count == 0)
    {
        Console.WriteLine("Propriétaire introuvable.");
        AppuyerPourContinuer();
        return null;
    }
    else if (comptes.Count > 1)
    {
        Console.WriteLine("Choisissez : Compte Courant (C) ou Compte Epargne (E) :");
        string type = Console.ReadLine().ToUpper();
        if (type == "C")
        {
            comptes = comptes.FindAll(compte => compte.GetType() == typeof(CompteCourant));
        }
        else
        {
            comptes = comptes.FindAll(compte => compte.GetType() == typeof(CompteEpargne));
        }
    }

    return comptes[0];
}

void AppuyerPourContinuer()
{
    Console.WriteLine("Appuyez sur n'importe quelle touche pour retourner au menu");
    Console.ReadKey();
}

decimal EnregistrerMontant()
{
    Console.WriteLine("Entrez le montant de la transaction : ");
    string input = Console.ReadLine();
    decimal montant = !string.IsNullOrEmpty(input) && Regex.IsMatch(input, "^[0-9,]+$") ? decimal.Parse(input) : 0;
    return montant;
}