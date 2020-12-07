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
        public IDCardList IDCardList;
        public bool cursed;
        public List<IDCard> chosenMiniGames = new List<IDCard>();
        public bool firstGame;
        public bool isDone;

        //for easy debug
        public int purcentageAddedOnID;

        public void ChoseMiniGames()
        {
            if (!firstGame)
            {
                //number of different game calculated by devided the lenght by 2 (it's int so it's fine 5/2 = 2)
                int differentGameNumber = length / (int)2;
                //purcentage will ad every value if each game to creat a global procentage
                int purcentage =0;
                for (int i = 0; i < IDCardList.IDCards.Count; i++)
                {
                    purcentage += IDCardList.IDCards[i].apparitionPurcentage;
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
                    for (int x = 0; x < IDCardList.IDCards.Count; x++)
                    {
                        _currentChance += IDCardList.IDCards[x].apparitionPurcentage;
                        if(_random>= _previousChance && _random< _currentChance )
                        {
                            if (!_indexAlreadyTaken.Contains(x))
                            {
                                chosenMiniGames.Add(IDCardList.IDCards[x]);
                                _indexAlreadyTaken.Add(x);
                                //this number is what will be needed to be calculated
                                IDCardList.IDCards[x].apparitionPurcentage += purcentageAddedOnID;
                                Debug.Log(IDCardList.IDCards[x].name);
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
            }
        }
    }
}
