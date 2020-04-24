using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestSideScroller
    {
        GameObject canvas;
        GameObject MainCam;
        GameObject X;
        GameObject O;
        GameObject Scroller;
        GameObject scroolFloor;
        SideScrollerButton XButton;
        SideScrollerButton OButton;
        SideScrollerManager SSM;
        SideScrollerScroller SSS;
        SideScrollerPlayer SSPX;
        SideScrollerPlayer SSPO;
        [SetUp]
        public void SetUp()
        {
            canvas = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/SideScroller/canvas"));
            MainCam = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/SideScroller/Main Camera"));
            X = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/SideScroller/X"));
            O = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/SideScroller/O"));
            Scroller = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/SideScroller/Scroller"));
            scroolFloor = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/SideScroller/scrollFloor"));
            XButton = canvas.transform.GetChild(0).gameObject.GetComponent<SideScrollerButton>();
            OButton = canvas.transform.GetChild(1).gameObject.GetComponent<SideScrollerButton>();
            SSM = MainCam.GetComponent<SideScrollerManager>();
            SSM.X = X;
            SSM.O = O;
            SSS = Scroller.GetComponent<SideScrollerScroller>();
            SSPX = X.GetComponent<SideScrollerPlayer>();
            SSPO = O.GetComponent<SideScrollerPlayer>();
        }
        [TearDown]
        public void TearDown()
        {
            Object.Destroy(canvas);
            Object.Destroy(MainCam);
            Object.Destroy(Scroller);
            Object.Destroy(scroolFloor);
            Object.Destroy(XButton);
            Object.Destroy(OButton);
            Object.Destroy(SSM);
            Object.Destroy(SSS);
            Object.Destroy(SSPX);
            Object.Destroy(SSPO);
            Object.Destroy(X);
            Object.Destroy(O);
        }
        [UnityTest]
        public IEnumerator TestSideScrollerXJumps()
        {
            SSM.StartGame();
            yield return new WaitForSeconds(0.3f);
            float init = X.transform.position.y;
            SSPX.Jump();
            yield return new WaitForSeconds(0.3f);
            Assert.Less(init, X.transform.position.y);
        }
        [UnityTest]
        public IEnumerator TestSideScrollerOJumps()
        {

            SSM.StartGame();
            yield return new WaitForSeconds(0.3f);
            float init = O.transform.position.y;
            SSPO.Jump();
            yield return new WaitForSeconds(0.3f);
            Assert.Less(init, O.transform.position.y);
        }
    }
}
