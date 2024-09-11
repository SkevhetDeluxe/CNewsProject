namespace CNewsProject.Models.HelperModels;

public class SearchBarSetting
{
    public SearchBarSetting(List<string> autoCompleteItems, string elementId, string jsFunctionName)
    {
        Suggestions = autoCompleteItems;
        InputFieldId += elementId;
        ButtonId += elementId;
        ContainerId += elementId;
        JsArrayName += elementId;

        if (!jsFunctionName.EndsWith("()"))
        {
            JsFunctionName = jsFunctionName + "()";
        }
        else
        {
            JsFunctionName = jsFunctionName;
        }
    }

    public SearchBarSetting(List<string> autoCompleteItems, string elementId, string jsFunctionName, string placeholder) : this(autoCompleteItems, elementId, jsFunctionName)
    {
        Placeholder = placeholder;
    }

    public SearchBarSetting(List<string> autoCompleteItems, string elementId, string jsFunctionName, string placeholder, string controllerParameter) :
        this(autoCompleteItems, elementId, jsFunctionName, placeholder)
    {
        ControllerParameter = controllerParameter;
    }
    
    // AutoCompleteItems
    public List<string> Suggestions { get; set; } = new List<string>();
    
    // SETTINGS

    #region Settings
    public string ContainerId { get; set; } = "SBContainer";
    public string JsFunctionName { get; set; }
    public string JsArrayName { get; set; } = "itemsArray";
    public string InputFieldId { get; set; } = "SBInputField";
    public string ButtonId {get; set;} = "SBButton";
    public string Placeholder { get; set; } = "Input";
    public string ControllerParameter { get; set; } = "No Parameter";
    
    #endregion
}