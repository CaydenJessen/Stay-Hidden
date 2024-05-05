using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemText : MonoBehaviour
{
    public TMP_Text itemCountText;
    public Player_Movement pM;
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        itemCountText.text = count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        count = pM.num + 1;
        itemCountText.text = count.ToString();
    }
}
