using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestMainGame
    {
        GameObject canvas;
        GameObject GamePlayGO;
        GamePlay Game;
        [SetUp]
        public void Setup()
        {
            canvas = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/GameScene/Canvas"));
            GamePlayGO = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/GameScene/GamePlay"));
            Game = GamePlayGO.GetComponent<GamePlay>();
        }
        [TearDown]
        public void TearDown()
        {
            Object.Destroy(canvas);
            Object.Destroy(GamePlayGO);
            Object.Destroy(Game);
        }
        [UnityTest]
        public IEnumerator PlaceCell()
        {
            yield return null;
        }
        [UnityTest]
        public IEnumerator PlaceCellTwo()
        {
            yield return null;
        }
        [UnityTest]
        public IEnumerator TTTWins()
        {
            yield return null;
        }
    }
}
