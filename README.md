# Articles Manager
This project is devided into different project. The first one is a Content Manager System for articles and pages, the second one is an Article Reader that displays the articles and the pages created via the CMS.

In terms of architecture/design strategy, I used Dependency injection via Ninject library, the repository pattern and Unit of Work pattern.
I am using an XML file as data source, which could be replaced by an SQL Server database with an appropriate data connctor layer (using Entity Framework for example)

## Built with
ASP .NET MVC 5

jQuery 1.9.1

Ninject MVC5

Tiny MCE

## Demo
http://demoarticlemanager.azurewebsites.net
