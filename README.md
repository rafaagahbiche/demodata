# Articles Manager
In this project I wanted to use ASP .NET MVC 5 and custom jQuery plugin.
I used three controllers in one page (multiple partial views) and using partial loading avoiding reloading the page everytime we update the data. 
In terms of architecture/design strategy, I used Dependency injection via Ninject library, the repository pattern and Unit of Work pattern.
I am using an XML file as data source, which could be replaced by an SQL Server database with an appropriate data connctor layer (using Entity Framework for example)

## Demo
http://demoarticlemanager.azurewebsites.net
