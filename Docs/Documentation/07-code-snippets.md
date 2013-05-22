# Code Snippets

Now that you are up to speed with everything Karbon has to offer, checkout these code snippets to give your template development a kick start.

## Navigation

	<ul>
		@foreach(var page in Model.HomePage.Children(child => child.IsVisible())){
			<li><a href="@page.Url()">@page.Name</a></li>
		}
	</ul>

## Sub Navigation

	<ul>
		@foreach(var page in Model.CurrentPage.Closest(parent => parent.Depth == 2).Children(child => child.IsVisible()){
			<li><a href="@page.Url()">@page.Name</a></li>
		}
	</ul>

## Breadcrumb

	<ul>
		@foreach(var page in Model.HomePage.Find(child => child.IsOpen()){
			<li><a href="@page.Url()">@page.Name</a></li>
		}
	</ul>

## Sitemap

	<ul>
		@RenderChildren(Model.HomePage.Children())
	</ul>

	@helper RenderChildren(IEnumerable<IContent> children)
	{
	    foreach(var child in children){
			<li>
				<a href="@page.Url()">@page.Name</a>
				@if(child.Children().Any()){
					<ul>
						@RenderChildren(child.Children())
					</ul>
				}
			</li>
		}
	}

## Share your code snippets
If you've got a code snippet you can't live without and would like to share it with the rest of the world, but sure to drop us an email at help@karboncms.com and we'll see if we can't add it to the list.