using UnityEngine;

public class Level144WetTurbansController : MonoBehaviour
{
    public Level144WetTurban[] turbans;

    public void Show(Level144TurbanColors color)
    {
        turbans[(int)color].Show();
    }
}