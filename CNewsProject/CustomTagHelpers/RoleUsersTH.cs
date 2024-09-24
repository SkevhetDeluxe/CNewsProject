
namespace CNewsProject.CustomTagHelpers
{
    [HtmlTargetElement("td", Attributes = "i-role")]
    public class RoleUsersTH(UserManager<AppUser> usermgr)
        : TagHelper
    {
        [HtmlAttributeName("i-role")]
        public string Role { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            List<string> names = new List<string>();
           
            
            var anvaendare =  await usermgr.GetUsersInRoleAsync(Role);

            if (anvaendare.Any())
            {
                foreach (var individ in anvaendare)
                {
                    names.Add(individ.UserName!);
                }
            }
            
            output.Content.SetContent(names.Count == 0 ? "No Users" : string.Join(", ", names));
        }
    }
}