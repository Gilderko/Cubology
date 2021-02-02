using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Saving;

public class MainMenu : MonoBehaviour, ISaveable
{
    public static Material playerMaterial;
    public static Mesh playerMesh = null;    
    [SerializeField] GameObject menu;
    [SerializeField] Mesh[] meshOptions;
    [SerializeField] List<NameToSkinButton> nameSkinButtonDictionary;
    [SerializeField] List<NameToShapeButton> nameShapeButtonDictionary;


    private static bool isInitialLoad = true;
    protected SavingWrapper savingWrapper;
    protected static List<SkinButtonSaveInfo> staticNameSkins;
    protected static List<ShapeButtonSaveInfo> staticNameShapes;
    protected static int meshOptionIndex;
    protected static string skinOptionName;


    [Serializable]
    public class NameToSkinButton
    {
        public SkinButtonTypes buttonType;
        public Button button;        

        public void UpdateButton(List<NameToSkinButton> skinButtons)
        {
            foreach (NameToSkinButton button in skinButtons)
            {                
                button.button.image.color = new Color(1f, 1f, 1f, 0f);
            }
            var colors = button.colors;
            button.image.color = new Color(1f, 1f, 1f, 1f);                        
        }
    }

    [Serializable]
    public class NameToShapeButton 
    {
        public ShapeButtonTypes buttonType;
        public Button button;        

        public void UpdateButton(List<NameToShapeButton> shapeButtons)
        { 
            foreach (NameToShapeButton button in shapeButtons)
            {
                button.button.image.color = new Color(1f, 1f, 1f, 0f);
            }
            var colors = button.colors;
            button.image.color = new Color(1f, 1f, 1f, 0.3f);                  
        }
    }

    [Serializable]
    public class ShapeButtonSaveInfo
    {
        public ShapeButtonTypes buttonType;        
        public bool selected = false;

        public void UpdateButton(List<ShapeButtonSaveInfo> shapeButtonSaveInfos, List<NameToShapeButton> shapeButtons)
        {
            shapeButtons.Find(x => x.buttonType == this.buttonType).UpdateButton(shapeButtons);
            foreach (ShapeButtonSaveInfo shapeButtonSaveInfo in shapeButtonSaveInfos)
            {
                shapeButtonSaveInfo.selected = false;
            }
            selected = true;
        }
    }

    [Serializable]
    public class SkinButtonSaveInfo
    {
        public SkinButtonTypes buttonType;        
        public bool selected = false;

        public void UpdateButton(List<SkinButtonSaveInfo> skinButtonSaveInfos, List<NameToSkinButton> shapeButtons)
        {
            shapeButtons.Find(x => x.buttonType == this.buttonType).UpdateButton(shapeButtons);
            foreach (SkinButtonSaveInfo skinButtonSaveInfo in skinButtonSaveInfos)
            {
                skinButtonSaveInfo.selected = false;
            }
            selected = true;
        }
    }

    [Serializable]
    public enum SkinButtonTypes
    {
        BasicSkin,Cactus,Rock,Dirt,Wall,GoldCoin
    }

    [Serializable]
    public enum ShapeButtonTypes
    {
        SphereShape, CubeShape
    }

    LevelManager levelManager;

    public event Action OnSkinChange;
    public event Action OnShapeChange;
    
    List<SkinButtonSaveInfo> InitialiseSkinButtonSaveInfos()
    {
        List<SkinButtonSaveInfo> skinSaveInfos = new List<SkinButtonSaveInfo>();
        foreach (NameToSkinButton skinButton in nameSkinButtonDictionary)
        {
            SkinButtonSaveInfo newSave = new SkinButtonSaveInfo();
            newSave.buttonType = skinButton.buttonType;
            newSave.selected = false;
            skinSaveInfos.Add(newSave);
        }
        return skinSaveInfos;
    }

    List<ShapeButtonSaveInfo> InitialiseShapeButtonSaveInfos()
    {
        List<ShapeButtonSaveInfo> shapeSaveInfos = new List<ShapeButtonSaveInfo>();
        foreach (NameToShapeButton shapeButton in nameShapeButtonDictionary)
        {
            ShapeButtonSaveInfo newSave = new ShapeButtonSaveInfo();
            newSave.buttonType = shapeButton.buttonType;
            newSave.selected = false;
            shapeSaveInfos.Add(newSave);
        }
        return shapeSaveInfos;
    }

    private void Start()
    {
        savingWrapper = FindObjectOfType<SavingWrapper>();
        if (isInitialLoad)
        {
            staticNameSkins = InitialiseSkinButtonSaveInfos();
            staticNameShapes = InitialiseShapeButtonSaveInfos();
            isInitialLoad = false;
        }
        levelManager = FindObjectOfType<LevelManager>();
        foreach (SkinButtonSaveInfo nameToSkinButton in staticNameSkins)
        {
            if (nameToSkinButton.selected)
            {
                print("changing skin button");
                nameToSkinButton.UpdateButton(staticNameSkins,nameSkinButtonDictionary);
            }
        }
        foreach (ShapeButtonSaveInfo nameToShapeButton in staticNameShapes)
        {
            if (nameToShapeButton.selected)
            {
                print("changing shape button");
                nameToShapeButton.UpdateButton(staticNameShapes,nameShapeButtonDictionary);
            }
        }
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void DeleteSaveFile()
    {
        FindObjectOfType<SavingWrapper>().DeleteLevelInfo();
    }

    public void BacktoLevelSelection()
    {        
        menu.GetComponent<Animation>().Play("ToggleOffMenu");
        menu.GetComponent<CanvasGroup>().interactable = false;
        StartCoroutine(ToggleOfMenu());
    }

    private IEnumerator ToggleOfMenu()
    {
        yield return new WaitForSeconds(0.5f);
        print("seting false");
        FindObjectOfType<MoveCameraTouch>().blockingRaycast = false;
        menu.SetActive(false);
    }

    public void ChangeSkinnCactus()
    {
        playerMaterial = (Material)Resources.Load("Cactus", typeof(Material));
        skinOptionName = "Cactus";
        OnSkinChange();
        SkinButtonSaveInfo customButton = staticNameSkins.Find(x => x.buttonType == SkinButtonTypes.Cactus);
        customButton.UpdateButton(staticNameSkins, nameSkinButtonDictionary);
        savingWrapper.SaveData();
    }
    public void ChangeSkinnPlayer()
    {
        playerMaterial = (Material)Resources.Load("Player", typeof(Material));
        skinOptionName = "Player";
        OnSkinChange();
        SkinButtonSaveInfo customButton = staticNameSkins.Find(x => x.buttonType == SkinButtonTypes.BasicSkin);
        customButton.UpdateButton(staticNameSkins, nameSkinButtonDictionary);
        savingWrapper.SaveData();
    }
    public void ChangeSkinnCoin()
    {
        playerMaterial = (Material)Resources.Load("Coin", typeof(Material));
        skinOptionName = "Coin";
        OnSkinChange();
        SkinButtonSaveInfo customButton = staticNameSkins.Find(x => x.buttonType == SkinButtonTypes.GoldCoin);
        customButton.UpdateButton(staticNameSkins, nameSkinButtonDictionary);
        savingWrapper.SaveData();
    }
    public void ChangeSkinDirt()
    {
        playerMaterial = (Material)Resources.Load("Dirt", typeof(Material));
        skinOptionName = "Dirt";
        OnSkinChange();
        SkinButtonSaveInfo customButton = staticNameSkins.Find(x => x.buttonType == SkinButtonTypes.Dirt);
        customButton.UpdateButton(staticNameSkins, nameSkinButtonDictionary);
        savingWrapper.SaveData();
    }
    public void ChangeSkinnRock()
    {
        playerMaterial = (Material)Resources.Load("Rock", typeof(Material));
        skinOptionName = "Rock";
        OnSkinChange();
        SkinButtonSaveInfo customButton = staticNameSkins.Find(x => x.buttonType == SkinButtonTypes.Rock);
        customButton.UpdateButton(staticNameSkins, nameSkinButtonDictionary);
        savingWrapper.SaveData();
    }
    public void ChangeSkinnWall()
    {
        playerMaterial = (Material)Resources.Load("Wall", typeof(Material));
        skinOptionName = "Wall";
        OnSkinChange();
        SkinButtonSaveInfo customButton = staticNameSkins.Find(x => x.buttonType == SkinButtonTypes.Wall);
        customButton.UpdateButton(staticNameSkins, nameSkinButtonDictionary);
        savingWrapper.SaveData();
    }
    public void ChangeShapeCube()
    {
        playerMesh = meshOptions[1];
        meshOptionIndex = 1;
        GameManager.isGameHard = true;
        OnShapeChange();
        ShapeButtonSaveInfo customButton = staticNameShapes.Find(x => x.buttonType == ShapeButtonTypes.CubeShape);
        customButton.UpdateButton(staticNameShapes,nameShapeButtonDictionary);
        savingWrapper.SaveData();
    }
    public void ChangeShapeSphere()
    {
        playerMesh = meshOptions[0];
        meshOptionIndex = 0;
        GameManager.isGameHard = false;
        OnShapeChange();
        ShapeButtonSaveInfo customButton = staticNameShapes.Find(x => x.buttonType == ShapeButtonTypes.SphereShape);
        customButton.UpdateButton(staticNameShapes, nameShapeButtonDictionary);
        savingWrapper.SaveData();
    }

    [Serializable]
    public enum SavedDataType
    {
        ShapeButtons,SkinButtons,MeshOption,SkinOption
    }

    object ISaveable.CaptureState()
    {
        Dictionary<SavedDataType, object> savedData = new Dictionary<SavedDataType, object>();
        savedData.Add(SavedDataType.SkinButtons, staticNameSkins);
        savedData.Add(SavedDataType.ShapeButtons, staticNameShapes);
        savedData.Add(SavedDataType.MeshOption,meshOptionIndex);
        savedData.Add(SavedDataType.SkinOption,skinOptionName);
        return savedData;
    }

    void ISaveable.RestoreState(object state)
    {
        Dictionary<SavedDataType, object> savedData = (Dictionary<SavedDataType, object>) state;
        meshOptionIndex = (int)savedData[SavedDataType.MeshOption];
        skinOptionName = (string)savedData[SavedDataType.SkinOption];
        staticNameShapes = (List<ShapeButtonSaveInfo>)savedData[SavedDataType.ShapeButtons];
        staticNameSkins = (List<SkinButtonSaveInfo>)savedData[SavedDataType.SkinButtons];

        playerMaterial = (Material)Resources.Load(skinOptionName, typeof(Material));
        playerMesh = meshOptions[meshOptionIndex];

        isInitialLoad = false;
    }
}
