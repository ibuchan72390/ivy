# Ivy Framework

The Ivy Framework was created out of a departure from my previous job.  When working at my former employer, we had a relatively substantial framework providing a majority of the core functionality for our applications.  When I left, I was somewhat at a loss without a majority of this core functionality.  While I was able to re-create some aspects of what they had done, unlike their implementation, I wanted mine to be purely open source.  Everything in Ivy takes the mentality that you should code to interfaces and simply allow your container to determine the appropriate implementations.

Ivy provides a series of libraries that can be used to develop a frame for your applications properly leveraging a container to provide the context for your services. The Ivy.IoC project is made to provide an abstraction of an IoC container based on the Microsoft ServiceCollection. This can be setup in the beginning of your application and it will generate the ServiceCollection for you to return to your MVC context.  All Ivy libraries have a corresponding Ivy IoC project that will allow you to include them in the ContainerGenerator.

Ivy Provides the following libraries for your usage:
1) Amazon.S3: A basic library that is currently used for reading content via S3 in a web browser.  Allows you to easily create signed S3 links for various pieces of content in your S3 buckets.
2) Auth0 : A library that includes functionality for working with Auth0 and the JWT.  Includes middleware for interpreting the JWT as well as a service for interpreting the assigned user claims.  This library is specifically tooled to work with RS256 due to the higher security that it provides. I would discourage people from using HS256, as RS256 is almost just as easy and much more secure.
3) Caching (On-Hold): A library that provides an in-memory caching structure in conjunction with cache accessors.  I largely work in an AWS Lambda context, so working with an "in-memory" data store becomes somewhat pointless.
4) Data.Common: A common data library that is used for providing some basic structures for working with a database.
5) Data.MySQL: Works in conjunction with Common Data, except that it provides the MySQL specific transactions and SQL writer.
6) Data.MSSQL (On-Hold): Works in conjunction with Common Data, except that it provides the MSSQL specific transactions and SQL writer.
7) IoC: A library providing an abstraction of an IoC container.  Also includes a simple implementation based on the IServiceCollection.
8) MailChimp: A library for adding a user to a MailChimp list with proper verification of whether or not they have already been added to the list.
9) PayPal: A library specifically made for verifying and handling the IPN response.
10) TestUtilities: A number of utilities used for working in a TDD context.
11) Transformer: A library made to specifically provide a series of transformer abstractions when working with the Data.Common models.  A number of base transformers are set up that can provide the necessary functionality, you're simply expected to create an interface with the appropriate methods on it using the composite interfaces in the Ivy.Transformer.Core.Interfaces namespace.
12) Utilities: A few items that are used to extend the functionality of certain aspects of the system.  Includes a basic randomization, clock, and generic validation abstractions.
13) Validation: Provides a base with which you can "validate" certain items in the system.
14) Web: A series of services and interfaces made to support work on a web project.  Includes an abstraction of the HttpClient, JsonSerialization, and ApiHelper.
