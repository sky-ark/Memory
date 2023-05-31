using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class GameManager : MonoBehaviourSingleton<GameManager> {

    private GameObject _cardStored; //Première carte cliquée
    private GameObject _cardStoredToCompare; //Deuxième à comparer à la premiere
    private bool WaitForHiding; //Booléen pour empêcher de cliquer sur d'autres cartes pendant que l'on cache les 2 mauvaises cartes
    private int CountingTries;
    public TMP_Text CountingTriesDisplay;
    public List<GameObject> Cards; // Créé une liste des cartes
    public GameObject grid; //Ref vers la grid pour placer 
    private int foundCards; //Stocke le nombre de cartes trouvées 
    private Chrono _chrono; //ref au GO qui contient le script du chrono
    public AudioClip CardReveal;
    public AudioClip CardHide;
    public AudioClip Applause;

    private void Start()
    {
        _chrono = GameObject.FindObjectOfType<Chrono>();
        Cards = Cards.Concat(Cards).ToList(); //Double la liste
        Random rnd = new Random(); //Création Random
        Cards = Cards.OrderBy(o => rnd.Next()).ToList(); //Mélange la liste
        CountingTries = 0; // Remet à 0 le nombre d'essais. 
        for (int i = 0; i < Cards.Count; i++) // Boucle for avec pour limite la taille de la liste Cards
        {
            GameObject rndCard = Cards[i]; // 
            Instantiate(rndCard, grid.transform); // Place le GameObject dans la grille ( ne pas oublier de ref la grille ) 
        }
    }

    public void OnCardClick(GameObject cardGameObject) { 
        if (WaitForHiding) return; //Si on attend pour cacher les cartes, alors on arrête ici pour empêcher de cliquer sur d'autres cartes ( attention modif sur CardHandler.cs a faire )
        GetComponent<AudioSource>().clip = CardReveal;
        GetComponent<AudioSource>().Play();
        CountingTries += 1;
        CountingTriesDisplay.text = " Essais :" + CountingTries.ToString();
        cardGameObject.GetComponent<CardHandler>().SwitchColor(); //Gere le changement de couleur des cartes via CardHandler.cs
        if (_cardStored == null) { // si pas de carte sélectionnée 
            _cardStored = cardGameObject; // alors carte sélectionnée est celle cliquée
            _cardStored.GetComponent<Button>().interactable = false; // on désactive le bouton pour empêcher de recliquer dessus
        }
        else { //Sinon comparer
            string storedId = _cardStored.GetComponent<CardHandler>().id; //créé une string pour l'id de la première carte sélectionnée
            string newId = cardGameObject.GetComponent<CardHandler>().id; //même chose pour la deuxième ( cardGameObject de base mais erreur sur cette ligne )
            if (storedId == newId) {    //Si les deux cartes sont pareilles 
                _cardStored.GetComponent<Button>().interactable = false; //Rendre 1ere carte inactive
                cardGameObject.GetComponent<Button>().interactable = false; //Rendre 2eme carte inactive 
                _cardStored = null; //Supprimer la valeur de la première carte avant de laisser le joueur en choisir une autre
                foundCards += 2; //Ajoute 2 au nombre de cartes trouvées
                if (foundCards == Cards.Count) GameWon();
            }
            else { // si les deux cartes ne sont pas les mêmes 
                _cardStoredToCompare = cardGameObject; // Dans la methode invoke on ne peut pas utiliser de paramètre ( cardGameObject ) donc je le stock dans une variable
                WaitForHiding = true; //Indique que l'on compare les cartes ( pour empêcher le joueur de cliquer les autres  cartes pendant ce temps )
                Invoke(nameof(HideCards),1); // Invoque la méthode pour cacher les cartes après 1sc ( permet au joueur de les voir avant )
            }
        }
    }

    public void HideCards() {
        _cardStored.GetComponent<Button>().interactable = true; //Rend la carte active à nouveau
        _cardStored.GetComponent<CardHandler>().RollBackColor(); //Lui redonne la couleur face cachée
        _cardStored = null; //Supprimer la valeur de la 1ere carte 
        _cardStoredToCompare.GetComponent<CardHandler>().RollBackColor();  // ici j'ai changé cardGameObject par _cardStoredToCompare 
        _cardStoredToCompare = null; //Supprimer la valeur de la carte à comparer
        GetComponent<AudioSource>().clip = CardHide;
        GetComponent<AudioSource>().Play();
        WaitForHiding = false;
        
        
        
    }

    public void GameWon() {
        Utils.SaveScore(CountingTries, Chrono.currentChronoValue);
        // PlayerPrefs.SetInt("score", CountingTries);
        _chrono.StopChrono();
        GetComponent<AudioSource>().clip = Applause;
        GetComponent<AudioSource>().Play();
        Invoke("GoToMenu", 7);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("GameWonScene");
    }

}