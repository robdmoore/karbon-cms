# Custom Controllers

If you need to do anything at the beginning of a Karbon page request, or you want to handle a postback from a view, then you are going to want to use a custom controller.

By default, all Karbon page requests are routed to the `Karbob.Cms.Web.Controllers.KarbonController` Index action (this is why your views need to be inside `Views/Karbon` and is also why it'll look to use the Index view by default). To override this behaviour for a given template, just create a custom controller that extends the `KarbonController` like so:

	public class HomeController : KarbonController
	{
		public override ActionResult Index()
		{
			// Do your thing...

			return base.Index();
		}
	}

Just like with your views, and with strongly typing, Karbon will use the name of your content text file to try and find a suitable controller, so in the above example, any text files with the name `Home.txt` will get routed to our `HomeController` instead of the default `KarbonController`.

Just like with your views, controllers that extend the KarbonController get access to a `.CurrentPage` and `.HomePage` property, so you can still access all the page data within your controller actions. And just like views, they also support strongly typing:

	public class HomeController : KarbonController<Home>
	{
		...
	}

	public class AboutController : KarbonController<About, Home>
	{
		...
	}

## Alternate Actions

Another nice feature when using custom controllers is the ability to route requests to a custom action. As mentioned previously, by default, all requests are routed to the `Index` action of your controller, but you can override this by appending the desired action to the end of your URL.

For example, say you have the following controller:

	public class AboutController : KarbonController<About, Home>
	{
		public override ActionResult MyAction()
		{
			// Do your thing...
		}
	}

To have Karbon use your action rather than the Index action, instead of requesting:

	http://yourdomain.com/about

You can request:

	http://yourdomain.com/about/myaction

And as long as your route doesn't match a Karbon page route, it will redirect the request to your custom action.