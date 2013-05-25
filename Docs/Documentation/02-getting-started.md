# Getting Started
## System Requirements

The easiest way to get a complete environment setup is to install **Visual Studio 2012**. Alternatively, as long as you have the following components installed, you should have everything you need.

    ASP.NET MVC 4, IIS7 (Integrated Mode)

## Installing the NuGet package

Karbon CMS is available as a NuGet-package for **Visual Studio**. To begin with, create a new *ASP.NET MVC 4 Web Application*. Once the project is created, right-click it in the *Solution Explorer* and choose *Manage NuGet Packages*. Search for **Karbon.CMS**, click *install* and Karbon CMS and all of it's dependencies will be installed automatically. 

If you want to use the package manager console, simply enter the following command.

    PM> Install-Package Karbon.CMS

Once the package is installed, just launch your web application and you'll be greeted by the Karbon CMS welcome page.

![Karbon CMS Welcome Page](getting-started\01.png)

And you are done!

## Get the Source Code

If you are just building a website, you should only need to install the NuGet package. If however, you would like to have a poke around the source code or feel like getting involved, you can find the full source code for Karbon CMS over on BitBucket.

[https://bitbucket.org/theoutfieldnet/karbon-cms](https://bitbucket.org/theoutfieldnet/karbon-cms)

## Recommended Reading

As Karbon CMS makes use of ASP.NET MVC, if you are unfamiliar with ASP.NET MVC, it is recommended that you first take a look at the [tutorials and examples found on the official website](http://www.asp.net/mvc). If you feel comfortable with the basics, then you should be able to carry on reading without much trouble.