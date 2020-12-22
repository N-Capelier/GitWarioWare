using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Caps
{
    public class Cap
    {
        /// <summary>
        /// this name will be changed latter when we have islandes
        /// </summary>
        public int length;
        public bool cursed;
        public List<IDCard> chosenMiniGames = new List<IDCard>();
        public bool[] hasBarrel;
        public bool isDone;
        public GameObject trail;

        //for easy debug
        public int capWeight;

        
        public void ChoseMiniGames(int barrelProbabilty, CapsSorter sorter)
        {
            
                //number of different game calculated by devided the lenght by 2 (it's int so it's fine 5/2 = 2)
                int differentGameNumber = length / (int)2;
                //purcentage will ad every value if each game to creat a global procentage
                int purcentage =0;
                for (int i = 0; i < sorter.idCardsNotPlayed.Count; i++)
                {
                    purcentage += sorter.idCardsNotPlayed[i].idWeight;
                }
                
                // this reference all the differente mini game stocked in the cap
                List<int> _indexAlreadyTaken = new List<int>(differentGameNumber);
                for (int i = 0; i < differentGameNumber; i++)
                {
                    // random is the number selected in the global pool of value
                    int _random = Random.Range(0, purcentage);
                    //for each slot of different mini game avaible, test if random is between the previous purecentage
                    // and this id card purcentage with the previous value added 
                    // for exemple first id has a purcentage of 10 and  the second of 5, purcentage = 15
                    // lets say random = 12, its between the previous value (10) and the current id purcentage +the previous (5+10=15)
                    //if this id isnt in _indexAlreadyTaken, then the id is selected and is stocked in _indexAlreadyTaken
                    // if its taken, redue this iteration by doing i--
                    int _currentChance = 0;
                    int _previousChance = 0;
                    for (int x = 0; x < sorter.idCardsNotPlayed.Count; x++)
                    {
                        _currentChance += sorter.idCardsNotPlayed[x].idWeight;
                        if(_random>= _previousChance && _random< _currentChance )
                        {
                            if (!_indexAlreadyTaken.Contains(x))
                            {
                                chosenMiniGames.Add(sorter.idCardsNotPlayed[x]);
                                _indexAlreadyTaken.Add(x);
                                //this number is what will be needed to be calculated
                                sorter.idCardsNotPlayed[x].idWeight += capWeight;
                                break;
                            }
                            else
                            {
                                i--;
                                break;
                            }
                        }
                        _previousChance = _currentChance;
                    }
                    
                }
                //add the selected id to idcards played so they wonte be selected in the next zone
            foreach (IDCard idcard in chosenMiniGames)
            {
                if (sorter.idCards.Contains(idcard))
                {
                    if(!sorter.iDCardsPlayed.Contains(idcard))
                        sorter.iDCardsPlayed.Add(idcard);
                }
            }
                hasBarrel = new bool[length];
                for (int i = 0; i < hasBarrel.Length; i++)
                {
                    var _random = Random.Range(1, 99);
                    if (_random < barrelProbabilty)
                        hasBarrel[i] = true;
                    else
                        hasBarrel[i] = false;

                }
            
        }

        public Cap()
        {
         
        }
        /// <summary>
        /// use only in freeMode
        /// </summary>
        /// <param name="cards"></param>
        public Cap(List<IDCard> cards)
        {
            chosenMiniGames = cards;
            length = chosenMiniGames.Count;
            List<bool> _barrel = new List<bool>();
            foreach (var id in chosenMiniGames)
            {
                _barrel.Add(false);
            }
            hasBarrel = _barrel.ToArray();
        }
    }
}
