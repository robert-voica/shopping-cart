# shopping-cart
Comparison between Starcounter &amp; Microsoft asp.net mvc stacks

### How to run Starcounter example

Follow these steps to build and run Starcounter example

- Download and install Starcounter minimum version `2.1.4.3`. Direct link: http://downloads.starcounter.com/download/Starcounter/DevelopNightlyBuilds/2.1.4.3
- Open `/Starcounter/ShoppingCart.sln` solution in Visual Studio 2012/2013/2015.
- Make sure that the Working directory of the solution points to the `/Starcounter/ShoppingCart/wwwroot` folder. Read more here: https://github.com/Starcounter/Starcounter/wiki/Using-the-internal-web-server
- Press `Ctrl + F5` to build and start solution.
- Navigate to `http://localhost:8080/shoppingcart` to see how the application works.

### How to run ASP.NET MVC example

Follow these steps to build and run asp.net mvc example

- Download and install Microsoft SQL express 2014.
- Restore database from `/Asp.net/shopping-cart-db.zip` archive.
- Ensure that connection string in `/Asp.net/ShoppingCart/web.config` is correct.
- Open `/Asp.name/ShoppingCart.sln` solution in Visual Studio 2013/2015.
- Press `Ctrl + F5` to build and start application.
- The application should start in your default browser. Open the url in Google Chrome, if it is not your default browser.

### Notes

These code samples use WebComponents and Shadow DOM with Polymer 1.1 as JavaScript framework. That is why it works in Google Chrome only.