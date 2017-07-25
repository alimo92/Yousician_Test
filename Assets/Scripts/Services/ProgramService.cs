using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgramService  {

    private TitleService titleservice;
    private ImageUrlBuilder imageurlbuilder;
    private DescriptionService descriptionservice;
    private PublicationEventService publicationeventservice;

    public ProgramService()
    {
        titleservice = new TitleService();
        imageurlbuilder = new ImageUrlBuilder();
        descriptionservice = new DescriptionService();
        publicationeventservice = new PublicationEventService();
    }


    //creates a program item and put it in the list
    //
    public void CreateProgram(Transform prefab, Transform parent, Program program, string language, SimpleObjectPool simple_object_pool)
    {
        //takes object from the object_pool, if not found, creates a new one
        GameObject temp_object = simple_object_pool.GetObject();

        // Alter Program component based on the new program object
        AlterProgramComponent(temp_object, program);

        // change the fields like title and imageID in a button based on the language
        AlterProgramGameObject(temp_object, program, language); 

        //gives a title to the gameobject
        temp_object.name = titleservice.GetTitleByLangue(program, language)+" ("+program.ProgramTypeMedia+")" ;

        //put the gameobject in the scrolling list
        temp_object.transform.parent = parent;

    }

    //creates a list of  program Items and put them in the list 
    public void CreateListProgram(Transform prefab, Transform parent, List<Program> list_program, string language, SimpleObjectPool simple_object_pool)
    {
        for(int i = 0; i < list_program.Count; i++)
        {
            CreateProgram(prefab, parent, list_program[i], language, simple_object_pool);
        }
    }


    //alter a program component based on the values from a Program object
    private void AlterProgramComponent(GameObject temp_object, Program program)
    {
        temp_object.GetComponent<Program>().ProgramId = program.ProgramId;
        temp_object.GetComponent<Program>().ProgramListTile = program.ProgramListTile;
        temp_object.GetComponent<Program>().ProgramListPublicationEvent = program.ProgramListPublicationEvent;
        temp_object.GetComponent<Program>().ProgramListDescription = program.ProgramListDescription;
        temp_object.GetComponent<Program>().ProgramType = program.ProgramType;
        temp_object.GetComponent<Program>().ProgramTypeMedia = program.ProgramTypeMedia;
        temp_object.GetComponent<Program>().ProgramImageId = program.ProgramImageId;
    }
    

    //populate the program item in the list with proper values
    private void AlterProgramGameObject(GameObject temp_object, Program program, string language)
    {
        //Change the title of program item
        temp_object.transform.GetChild(0).GetComponent<Text>().text = titleservice.GetTitleByLangue(program, language);

        //Change the mediaType (radio or tv) of the program item
        temp_object.transform.GetChild(2).GetComponent<TypeMediaIconLoader>().TypeMedia = program.ProgramTypeMedia;
        temp_object.transform.GetChild(2).GetComponent<TypeMediaIconLoader>().flag = true; // gives indication value changed


        //
        if (program.ProgramImageId != "not available")
        {
            //build the image url based on the ImageId and change the ImageURL and ImageId
            temp_object.transform.GetChild(1).GetComponent<ImageLoader>().ImageUrl = imageurlbuilder.GetImageUrl(imageurlbuilder.ConstructTransformation(InitImageAttributes()), program.ProgramImageId, "png");
            temp_object.transform.GetChild(1).GetComponent<ImageLoader>().flag = true;// gives indication value changed
            temp_object.transform.GetChild(1).GetComponent<ImageLoader>().ImageId = program.ProgramImageId;
        }
        else
        {
            //if Image not Available, empty the ImageURL and put "not available" in ImageId
            temp_object.transform.GetChild(1).GetComponent<ImageLoader>().ImageUrl = "";
            temp_object.transform.GetChild(1).GetComponent<ImageLoader>().flag = true;// gives indication value changed
            temp_object.transform.GetChild(1).GetComponent<ImageLoader>().ImageId = "not available";
        }
    }


    //init properties of the retrieved image
    private List<string> InitImageAttributes()
    {
        List<string> list_image_attributes = new List<string>();

        list_image_attributes.Add("w_100");
        list_image_attributes.Add("h_100");
        list_image_attributes.Add("r_max");

        return list_image_attributes;
    }


    //Desactive all objects in the list and put them in the object_pool
    public void RemoveAllPrograms(Transform parent, SimpleObjectPool simple_object_pool)
    {
        int temp = parent.childCount;
        for(int i = 0; i < temp; i++)
        {
            parent.GetChild(0).GetChild(3).gameObject.SetActive(true);
            parent.GetChild(0).GetChild(1).GetComponent<RawImage>().enabled = false;
            simple_object_pool.ReturnObject(parent.GetChild(0).gameObject);
        }

    }

    //Set program details in the second screen
    public void SetDetailProgramScreen(GameObject header, Program program, string language)
    {
        header.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = titleservice.GetTitleByLangue(program, language);

        header.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = descriptionservice.GetDescriptionByLanguage(program, language);

        header.transform.GetChild(0).GetChild(2).GetComponent<ImageLoader>().ImageId = program.ProgramImageId;
        header.transform.GetChild(0).GetChild(2).GetComponent<ImageLoader>().flag = true;
        header.transform.GetChild(0).GetChild(2).GetComponent<RawImage>().enabled= false;

        header.transform.GetChild(0).GetChild(3).GetComponent<TypeMediaIconLoader>().TypeMedia = program.ProgramTypeMedia;
        header.transform.GetChild(0).GetChild(3).GetComponent<TypeMediaIconLoader>().flag = true;


        PublicationEvent Next = publicationeventservice.GetPublication(program, "Next");
        PublicationEvent Previous = publicationeventservice.GetPublication(program, "Previous");

        header.transform.GetChild(0).GetChild(4).GetComponent<Text>().text = "Next Publication :  \n" + publicationeventservice.GetStringPublicationEvent(Next, "Previous");

        header.transform.GetChild(0).GetChild(5).GetComponent<Text>().text = "Previous Publication :  \n" + publicationeventservice.GetStringPublicationEvent(Previous, "Previous");

        header.transform.GetChild(0).GetChild(6).GetComponent<Text>().text = "Duration :  " + publicationeventservice.GetDurationPublicationEvent(Next);
        header.transform.GetChild(0).GetChild(7).GetComponent<Text>().text = "Duration :  " + publicationeventservice.GetDurationPublicationEvent(Previous);

    }

}
