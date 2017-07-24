using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgramService  {

    private TitleService titleservice;
    private ImageUrlBuilder imageurlbuilder;
    private DescriptionService descriptionservice;

    public ProgramService()
    {
        titleservice = new TitleService();
        imageurlbuilder = new ImageUrlBuilder();
        descriptionservice = new DescriptionService();
    }

    public void CreateProgram(Transform prefab, Transform parent, Program program, string language, SimpleObjectPool simple_object_pool)
    {
        GameObject temp_object = simple_object_pool.GetObject();
        AlterProgramComponent(temp_object, program);
        AlterProgramGameObject(temp_object, program, language);
        temp_object.name = titleservice.GetTitleByLangue(program, language)+" ("+program.ProgramTypeMedia+")" ;
        temp_object.transform.parent = parent;

    }


    public void CreateListProgram(Transform prefab, Transform parent, List<Program> list_program, string language, SimpleObjectPool simple_object_pool)
    {
        for(int i = 0; i < list_program.Count; i++)
        {
            CreateProgram(prefab, parent, list_program[i], language, simple_object_pool);
        }
    }


    
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
    

    private void AlterProgramGameObject(GameObject temp_object, Program program, string language)
    {
        temp_object.transform.GetChild(0).GetComponent<Text>().text = titleservice.GetTitleByLangue(program, language);

        temp_object.transform.GetChild(2).GetComponent<TypeMediaIconLoader>().TypeMedia = program.ProgramTypeMedia;
        temp_object.transform.GetChild(2).GetComponent<TypeMediaIconLoader>().flag = true;

        if (program.ProgramImageId != "not available")
        {
            temp_object.transform.GetChild(1).GetComponent<ImageLoader>().ImageUrl = imageurlbuilder.GetImageUrl(imageurlbuilder.ConstructTransformation(InitImageAttributes()), program.ProgramImageId, "png");
            temp_object.transform.GetChild(1).GetComponent<ImageLoader>().flag = true;
            temp_object.transform.GetChild(1).GetComponent<ImageLoader>().ImageId = program.ProgramImageId;
        }
        else
        {
            temp_object.transform.GetChild(1).GetComponent<ImageLoader>().ImageUrl = "";
            temp_object.transform.GetChild(1).GetComponent<ImageLoader>().flag = true;
            temp_object.transform.GetChild(1).GetComponent<ImageLoader>().ImageId = "not available";
        }
    }

    private List<string> InitImageAttributes()
    {
        List<string> list_image_attributes = new List<string>();

        list_image_attributes.Add("w_100");
        list_image_attributes.Add("h_100");
        list_image_attributes.Add("r_max");

        return list_image_attributes;
    }



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

    public void SetDetailProgramScreen(GameObject header, Program program, string language)
    {
        header.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = titleservice.GetTitleByLangue(program, language);

        header.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = descriptionservice.GetDescriptionByLanguage(program, language);

        header.transform.GetChild(0).GetChild(2).GetComponent<ImageLoader>().ImageId = program.ProgramImageId;
        header.transform.GetChild(0).GetChild(2).GetComponent<ImageLoader>().flag = true;
        header.transform.GetChild(0).GetChild(2).GetComponent<RawImage>().enabled= false;

        header.transform.GetChild(0).GetChild(3).GetComponent<TypeMediaIconLoader>().TypeMedia = program.ProgramTypeMedia;
        header.transform.GetChild(0).GetChild(3).GetComponent<TypeMediaIconLoader>().flag = true;
    }

}
