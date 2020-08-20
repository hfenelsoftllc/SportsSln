using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SportShop.Models.ViewModels;

namespace SportShop.Infrastructure{

[HtmlTargetElement("div",Attributes="page-Model")]
    public class PageLinkTagHelper: TagHelper{
        private IUrlHelperFactory urlHelperFactory;
        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory=helperFactory;
        }

    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext viewContext{get;set;}

    public PagingInfo PageModel{get;set;}

    public string PageAction{get;set;}

    public override void Process(TagHelperContext context, TagHelperOutput output){
        
        IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(viewContext);

        TagBuilder result = new TagBuilder("div");

        for(int i=1; i<=PageModel.TotalPages; i++){
            TagBuilder tag = new TagBuilder("a");   
            tag.Attributes["href"] = urlHelper.Action(PageAction, new {productPage =1});
            tag.InnerHtml.Append(i.ToString());
            result.InnerHtml.AppendHtml(tag);
        }
        output.Content.AppendHtml(result.InnerHtml);
    }

    }
}

