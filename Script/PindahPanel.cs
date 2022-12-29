using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PindahPanel : MonoBehaviour
{
    public GameObject panelAwal;

    public GameObject panelTujuan;

    public void GantiPanelBaru()
    {
        panelAwal.SetActive(false);
        panelTujuan.SetActive(true);
    }
}
