using System.Collections.Generic;
using System.IO;

public class UrlBuilder  {

    private List<UrlItem> ListUrlItem;
    private Tools tool;

    private string UrlBasic = "https://external.api.yle.fi/v1/programs/items.json?";



    public UrlBuilder()
    {
        tool = new Tools();
        ListUrlItem = InitListUrlItem();
    }


    //Returns an initialized List of UrlItems based on the attributes found in the file : UserKeys
    private List<UrlItem> InitListUrlItem()
    {
        JSONObject json = GetUserKeys();
        List<UrlItem> list_url_item = new List<UrlItem>();

        AddUrlItemToList(list_url_item, "app_id", tool.RemoveFirstLastCharacter(json.GetField("app_id").Print()));
        AddUrlItemToList(list_url_item, "app_key", tool.RemoveFirstLastCharacter(json.GetField("app_key").Print()));

        return list_url_item;
    }


    //Reads Userkeys file filled with attributes needed for Yle API
    //Returns JSONObject based on those attributes (app_id, app_key, url_decryption)
    private JSONObject GetUserKeys()
    {
        string userkeys = File.ReadAllText("./Assets/Scripts/Configuration/UserKeys.txt");
        JSONObject json = new JSONObject(userkeys);

        return json;
    }


    //Add an UrlItem to a list of urlitems
    //UrlItem consists of two strings (UrlItemType and UrlItemValue) 
    public void AddUrlItemToList(List<UrlItem> list_url_item, string urlitemtype, string urlitemvalue)
    {
        UrlItem urltem = new UrlItem(urlitemtype, urlitemvalue);
        list_url_item.Add(urltem);
    }


    //Remove an UrlItem from a list of urlitems
    //Checks if an item with a given urlitemtype and removes from the the list if it exists
    public void RemoveUrlItemFromList(List<UrlItem> list_url_item, string urlitemtype)
    {
        for(int i=0; i< list_url_item.Count; i++)
        {
            if (list_url_item[i].UrlItemType == urlitemtype)
            {
                list_url_item.RemoveAt(i);
            }
        }
    }

    //Replaces an UrlItem from a list of urlitems with a new value
    //if urlitem not found, it's added with a the new value
    public void AlterUrlItemFromList(List<UrlItem> list_url_item, string urlitemtype, string new_urlitemvalue)
    {
        bool found = false;
        for(int i=0;i< list_url_item.Count; i++)
        {
            if(list_url_item[i].UrlItemType== urlitemtype)
            {
                list_url_item[i].UrlItemValue = new_urlitemvalue;
                found = true;
            }
        }
        if (!found)
        {
            AddUrlItemToList(list_url_item, urlitemtype, new_urlitemvalue);
        }
    }

    //get new URL with new offset and new limit
    public string GetNewListUrl(List<UrlItem> list_url_item, string StartCount, string EndCount)
    {
        AlterUrlItemFromList(list_url_item, "offset", StartCount);
        AlterUrlItemFromList(list_url_item, "limit", EndCount);

        return GetUrl(list_url_item);
    }


    //Retuns a constructed url, ready to use, based of a list of UrlItems and UrlBasic
    public string GetUrl(List<UrlItem> list_url_item)
    {
        string url = UrlBasic;
        string temp_item;

        for(int i = 0; i < list_url_item.Count; i++)
        {
            temp_item = list_url_item[i].UrlItemType + "=" + list_url_item[i].UrlItemValue;
            url += temp_item + "&";
        }

        return url;
    }


    //Returns a constructed url with only two urlitems (app_key and app_id) which is the bare minimum
    public string GetUrl()
    {
        return GetUrl(InitListUrlItem());
    }

    //Returns ListUrlItem
    public List<UrlItem> GetListUrlItem()
    {
        return ListUrlItem;
    }






}
