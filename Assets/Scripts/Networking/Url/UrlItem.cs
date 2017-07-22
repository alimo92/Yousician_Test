using UnityEngine;


[System.Serializable]
public class UrlItem {

    public string UrlItemType;
    public string UrlItemValue;

    public UrlItem()
    {

    }

    public UrlItem(string urlitemtype , string urlitemvalue)
    {
        UrlItemType = urlitemtype;
        UrlItemValue = urlitemvalue;
    }


    public override string ToString()
    {
        return "UrlItemType: " + UrlItemType + " UrlItemValue: " + UrlItemValue;
    }

}
