using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishHandler : MonoBehaviour
{

    bool finished;
    public GameObject winText;

    /* Any code that needs to execute the moment the minigame starts should go here.
     * This method runs once when the scene is loaded.
     */
    void Start()
    {
        finished = false;
        winText.gameObject.SetActive(false);
    }

    /* Un-comment this method if frame by frame code is needed.
     * 
     * private void Update()
     * {
     *   
     * }
     * 
     */

    /* Any code that needs to be executed as soon as any horse crosses the finish line should go here.
     * This method executes every time one of the horses touches the finish line.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* Any code that needs to be executed once the minigame is finished should go in this if-block.
         * This block only executes once the first horse crosses the finish line.
         */
        if (!finished)
        {
            winText.GetComponent<TMPro.TextMeshProUGUI>().text = "Winner " + collision.name;
            winText.gameObject.SetActive(true);

            finished = true;
        }
    }
}
