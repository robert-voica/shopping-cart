# shopping-cart
Comparison between [Starcounter](http://starcounter.io/) &amp; Microsoft [asp.net](http://www.asp.net/) mvc stacks

### How to run Starcounter example

Follow these steps to build and run Starcounter example

- Download and install Starcounter minimum version `2.1.15`. Direct link: http://downloads.starcounter.com/download/Starcounter/DevelopNightlyBuilds/latest
- Open `/Starcounter/ShoppingCart.sln` solution in Visual Studio 2012/2013/2015.
- Make sure that the Working directory of the solution points to the `/Starcounter/ShoppingCart/wwwroot` folder. [Read more](https://github.com/Starcounter/Starcounter/wiki/Using-the-internal-web-server).
- Press `Ctrl + F5` to build and start solution.
- Navigate to http://localhost:8080/shoppingcart to see how the application works.

### How to run ASP.NET MVC example

Follow these steps to build and run asp.net mvc example

- Download and install [Microsoft SQL express 2014](http://www.microsoft.com/en-us/download/details.aspx?id=42299).
- [Restore database](https://msdn.microsoft.com/en-us/library/ms177429.aspx) from `/Asp.net/shopping-cart-db.zip` archive.
- Ensure that connection string in `/Asp.net/ShoppingCart/web.config` is correct.
- Open `/Asp.name/ShoppingCart.sln` solution in [Visual Studio 2013](https://www.visualstudio.com/en-us/news/vs2013-community-vs.aspx).
- [Restore NuGet packages](http://stackoverflow.com/questions/26315756/enable-nuget-package-restore-on-visual-studio-2013).
- Press `Ctrl + F5` to build and start application.
- The application should start in your default browser. Open the url in Google Chrome, if it is not your default browser.

### Notes

- These code samples use [WebComponents](http://webcomponents.org/), [Shadow DOM](http://w3c.github.io/webcomponents/spec/shadow/), and [Object.observe](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Object/observe) with [Polymer 1.1](https://www.polymer-project.org/1.0/) as JavaScript framework. So far these features are supported by [Google Chrome](https://www.google.com/chrome/) only.
- The asp.net mvc example should also work in Vistual Studio 2012/2015, but was not tested.