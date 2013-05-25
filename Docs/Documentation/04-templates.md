# Templates

Templates in Karbon are standard ASP.NET MVC Views and are located in the `Views/Karbon` folder.

By default, you should find a template named `Index.cshtml` in there which will currently be used for all requests made to one of your Karbon based pages. Obviously though, you are not going to want to use the same template for every page on your site, so Karbon offers you a simple way to override the default template, and instead use a template of your own.

## Page specific templates

If you already read the chapter about adding content you might remember that we added content to a page by creating a text file. At the time, we didn't put any importance on the name of the text file, but in actuality the name of the text file plays a pretty important role when it comes to choosing a template.

When you try to access a page in Karbon, rather than looking straight for `Index.cshtml` to render the contents with, it will actually try to find a template file with the same name as the content text file within the `Views/Karbon` folder.

So for example, say we want to add some content to our home page so we create a `Home.txt` file in the root of our content folder. Now say we want to build a specific template for our homepage, all we need to do is to add a new `Home.cshtml` file to `Views/Karbon` and Karbon will use that template instead.

To use the same template for other pages, just give the text file for that page the same name.

## Template base class

What markup you put in your templates, is completely up to you, Karbon does not mess with your markup in any way. The only requirement we have is, in order to have access to the content for your page, your templates must inherit from the following base class:

	@inherits KarbonView

## Accessing content

Once you've inherited the `KarbonView` base class, within your view you will get access to two properties:

	@Model.CurrentPage
	@Model.HomePage

From these you can access content defined in your text files like so:

	@Mode.CurrentPage.Get("Title")

**Top tip:** Store site wide settings in your home pages content file and you can access them from any template via the `@Model.HomePage` property.

This is just the basics of what you can do with your content, be sure to checkout the BLA BLA BLA section for full details on the content API.

## Creating reusable components

When working with templates, you may find that you have small components that you need to use across multiple templates. Rather than copying an pasting, you can break these out into separate files and include them in your templates.

For this, Karbon uses the standard Partials Views functionality of ASP.NET MVC. Simply create a `.cshtml` for the component in either the `Views/Karbon` or `Views/Shared` folder and render it out directly in your template.

	@Html.Partial("MainNav")

If you want to access the current page or home page properties within your component, simply have the partial view inherit the `KarbonView` base class in the same way you do for templates.