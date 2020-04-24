using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class TestMentalMath
    {
        GameObject MMGO;
        MMButtonHandler MMBH;
        [SetUp]
        public void Setup()
        {
            MMGO = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/MentalMath/GameObject"));
            MMBH = MMGO.GetComponent<MMButtonHandler>();
        }
        [TearDown]
        public void TearDown()
        {
            Object.Destroy(MMGO);
            Object.Destroy(MMBH);
        }
        [UnityTest]
        public IEnumerator TestMathInit()
        {
            yield return new WaitForSeconds(0.3f);
            Assert.IsTrue(MMBH.xstart.activeSelf);
            Assert.IsFalse(MMBH.ostart.activeSelf);
            Assert.IsFalse(MMBH.finalMessage.activeSelf);
        }

    }
}
