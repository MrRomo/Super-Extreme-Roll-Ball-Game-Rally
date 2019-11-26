using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class codeAnimationPausa : MonoBehaviour {


      // Update is called once per frame
    public void ChangeTier(int tier)
    {
        if (tier == 0)
        {
            QualitySettings.SetQualityLevel(tier, true);
        }
        if (tier == 2)
        {
            QualitySettings.SetQualityLevel(tier, true);
        }
        if (tier == 5)
        {
            QualitySettings.SetQualityLevel(tier, true);
        }

    }


}
