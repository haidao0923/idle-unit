using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectHero : MonoBehaviour
{
    public ViewController viewController;
    public void SelectHero1()
    {
        OpenHeroView(62);
    }
    public void SelectHero2() {
        OpenHeroView(54);
    }
    public void SelectHero3()
    {
        OpenHeroView(44);
    }
    public void SelectHero4()
    {
        OpenHeroView(77);
    }
    public void SelectHero5()
    {
        OpenHeroView(70);
    }
    void OpenHeroView(int heroIndex)
    {
        viewController.OpenUnitView(selectHeroIndex: heroIndex);
    }
}
