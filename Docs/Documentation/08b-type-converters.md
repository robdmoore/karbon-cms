# Type Converters
We've shown how easy it is to access string values from your content, but what if you want to return a none string value? Of course you can simply convert it in your view each time, but there is a nicer way.

Enter type converters.

Type converters are a standard .NET concept used to help convert one value type to another. There are a few built in type converters for things like string-to-number and string-to-date conversion, but you can also define your own converters.

If you want to create a custom converter, checkout the [official documentation on how to do this over on MSDN](http://msdn.microsoft.com/en-us/library/ayybcxe5.aspx).

Now that you know how to create type converters, lets see how to hook them up.

## Non-Strongly Typed Content
If you are accessing data via the `.Data` property on your content, you can use one of the extended `GetValue(...)` methods to get data of a given type.

If the type converter you want to use is a built in one, you can just do something like this:

	@Model.CurrentPage.Data.GetValue<Int32>("NumberOfEmployees")

If you want to use your own type converter though, you will need to do this:

	@Model.CurrentPage.Data.GetValue<MyType, MyTypeConverter>("MyProperty")

Karbon will then grab the value and attempt to convert it to the specific type.


## Strongly Typed Content
If you are using strongly typed content, you can tell Karbon how to convert the values by using the `TypeConverterAttribute` like so.

	public class MyModel : Content
	{
		[TypeConverter(typeof(MyTypeConverter))]
		public MyType MyProperty { get; set; }
	}

When Karbon loads your content, it will automatically use the defined type converter to convert the value for you.