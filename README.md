# H1 GcMvcDemo

=========
** Disclaimer ** This code is offered WITHOUT warrenty and guarantee. I did a bit of testing before I checked it in, but I can't say it's 100% solid. I'm sure I missed something somewhere.

This repository contains a finished Media Manage website done thee different ways:

# H2 Entity Framework
The EF folder has the verion using entity framework. In this case I went database first and used the Visual Studio/EF scaffolding to build as much of the site as I could. This was fast, but I had to expose my back-end entity classes directly to the MVC controllers, which I don't like doing. I'll explain why in the Fluent nHibernate section. The one benefit of pushing the entities all the wasy to the web layer is that I don't have to worry about mapping (which will be explained in the FNH section below)

# H2 Fluent nHibernate
In the FnH folder I built a solution using Fluent nHibernate, which is a competeing ORM to Entity Framework. FNH is open source and fee, with lots of community support and documentation online. I like FNH because it's code-first story is a little bit better than EF in my opinion. Since I had an existing database, I didn't need to have my code-first setup create a database, but I was able to write the mappings in C# (checkout out the mapping files in the Data project)

Another thing I've done here is NOT pass my entities to the web project.  I don't like doing that as what I end up with is a tightly bound depenency between my data/business language and my front end. This means (more often than not) a change on either side requries a change on the other. I accomplish this by creating "view model" classes for each entity. For example, I have a DVD entity (Data.Entities.DVD) and a DVD view model (Web.Model.ViewModels.DVD) that are actually seperate classes. The controllers and view ONLY know about the view model versions of the classes, not the entity classes. This means that they can change without effecting the entity versions of the class and vice versa. It's also a good security practice as I don't potentilly expose too much information about how my backend services work or how my data is structured. It also prevents me from sending data to the website that I don't want to be public.

The question then becomes, how to I convert an entity class to a view model class? There are a couple ways to do this. In the FNH demo I'm using a library called Automapper, which is avaiable via NuGet. Automapper allows you to easily create a mapping definition (as seen in AutomapperConfiguration.Configure, which is a lass and method I created) and then letting automapper do the work for me, as you can see in the models (calls to Mapper.Map). Automapper is really easy to use if both sides of the exchange have the same parameters with the same names and the same types, which for a view model is almost always the case. However, it does allow you to do some configurtion of mappings where this is not the case, which I did a little bit of on line 13 of the AutomapperConfiguration class.

Another differnce is that I am doing what I consider _true_ MVC. I feel that the EF version puts too much logic in the controller. I'm of the opinon that the controller should really be nothing more than a traffic cop; it should call out to a model (busienss model) for all the business stuff and do some page routing. I think EF makes you put too much logic in the controller.

# H2 Native SQL
In the sql folder I'm creating the site much like the FNH version, but instead of using an ORM like FNH or EF I am using the ADO.NET framework (part of .NET) to make my own calls to the database then create the objects from the data that comes back and vice versa. As you'll see, this is A LOT more work than using an ORM!

I am also NOT using Automapper in this example. The reason is to enable you to compare and contrast the different between using Automapper and doing the type conversion by hand. Again, it's a lot of work that is better automated when possible.