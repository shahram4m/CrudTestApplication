# Task Assignment: Develop a Full Stack .NET Core Reference Application
Objective: The goal of this task is to develop a comprehensive reference application for Full Stack .NET Core Developers, leveraging ASP.NET Core, Entity Framework Core, and implementing best practices in software architecture, including Clean Architecture, Domain-Driven Design (DDD), and SOLID principles.

Requirements:

Project Structure:
Implement a NLayer Hexagonal architecture consisting of Core, Application, Infrastructure, and Presentation Layers.
Follow Domain-Driven Design (DDD) best practices, including Entities, Repositories, Domain/Application Services, and DTOs.
Aim for a Clean Architecture, applying SOLID principles to ensure a loosely-coupled, dependency-inverted architecture.
Technologies and Tools:
Use ASP.NET Core for the web application.
Utilize Entity Framework Core for data access.
Implement design patterns such as Dependency Injection, logging, validation, exception handling, and localization.
Deliverables:
A complete ASP.NET Core reference application demonstrating a layered application architecture with DDD best practices.
Ensure the application adheres to Clean Architecture principles and applies SOLID principles.
Provide a comprehensive documentation of the project, including setup instructions, architecture overview, and code walkthroughs.
Evaluation Criteria:
Code quality, adherence to best practices, and implementation of Clean Architecture and SOLID principles.
Documentation clarity and completeness.
Application functionality and performance.



# run-aspnetcore
Here is CRUD operations of crudTestApplication-core template project;


**run-aspnetcore** is a general purpose to implement the **Default Web Application template of .Net** with **layered architecture** for building modern web applications with latest ASP.NET Core & Web API & EF Core technologies. 

## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)
* [.Net Core 7.0 or later](https://dotnet.microsoft.com/download/dotnet-core/3.0)
* EF Core 7.0 or later

### Installing
Follow these steps to get your development environment set up:
1. Clone the repository
2. At the root directory, restore required packages by running:
```csharp
dotnet restore
```
3. Next, build the solution by running:
```csharp
dotnet build
```
4. Next, within the crudTestApplication.Web directory, launch the back end by running:
```csharp
dotnet run
```
5. Launch http://localhost:5400/ in your browser to view the Web UI.

If you have **Visual Studio** after cloning Open solution with your IDE, crudTestApplication.Web should be the start-up project. Directly run this project on Visual Studio with **F5 or Ctrl+F5**. You will see index page of project, you can navigate customer pages and you can perform crud operations on your browser.

### Usage
After cloning or downloading the sample you should be able to run it using an In Memory database immediately. The default configuration of Entity Framework Database is **"InMemoryDatabase"**.
If you wish to use the project with a persistent database, you will need to run its Entity Framework Core **migrations** before you will be able to run the app, and update the ConfigureDatabases method in **Startup.cs** (see below).

```csharp
public void ConfigureDatabases(IServiceCollection services)
{
    // use in-memory database
    services.AddDbContext<crudTestApplicationContext>(c =>
        c.UseInMemoryDatabase("crudTestApplicationConnection")
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

    //// use real database
    //services.AddDbContext<crudTestApplicationContext>(c =>
    //    c.UseSqlServer(Configuration.GetConnectionString("crudTestApplicationConnection"))
    //    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
}
```

1. Ensure your connection strings in ```appsettings.json``` point to a local SQL Server instance.

2. Open a command prompt in the Web folder and execute the following commands:

```csharp
dotnet restore
dotnet ef database update -c crudTestApplicationContext -p ../crudTestApplication.Infrastructure/crudTestApplication.Infrastructure.csproj -s crudTestApplication.Web.csproj
```
Or you can direct call ef commands from Visual Studio **Package Manager Console**. Open Package Manager Console, set default project to crudTestApplication.Infrastructure and run below command;
```csharp
update-database
```
These commands will create crudTestApplication database which include Customer table. You can see from **crudTestApplicationContext.cs**.
1. Run the application.
The first time you run the application, it will seed crudTestApplication sql server database with a few data such that you should see customer.

If you modify-change or add new some of entities to Core project, you should run ef migrate commands in order to update your database as the same way but below commands;
```csharp
add migration YourCustomEntityChanges
update-database
```

## Layered Architecture
crudTestApplication implements NLayer **Hexagonal architecture** (Core, Application, Infrastructure and Presentation Layers) and **Domain Driven Design** (Entities, Repositories, Domain/Application Services, DTO's...). Also implements and provides a good infrastructure to implement **best practices** such as Dependency Injection, logging, validation, exception handling, localization and so on.
Aimed to be a **Clean Architecture** also called **Onion Architecture**, with applying **SOLID principles** in order to use for a project template. Also implements and provides a good infrastructure to implement **best practices** like **loosely-coupled, dependency-inverted** architecture
The below image represents crudTestApplication approach of development architecture of run repository series;

### Structure of Project
Repository include layers divided by **4 project**;
* Core
    * Entities    
    * Interfaces
    * Specifications
    * ValueObjects
    * Exceptions
* Application    
    * Interfaces    
    * Services
    * Dtos
    * Mapper
    * Exceptions
* Infrastructure
    * Data
    * Repository
    * Services
    * Migrations
    * Logging
    * Exceptions
* Web
    * Interfaces
    * Services
    * Pages
    * ViewModels
    * Extensions
    * AutoMapper

### Core Layer
Development of Domain Logic with abstraction. Interfaces drives business requirements with light implementation. The Core project is the **center of the Clean Architecture** design, and all other project dependencies should point toward it.. 

#### Entities
Includes Entity Framework Core Entities which creates sql table with **Entity Framework Core Code First Aproach**. Some Aggregate folders holds entity and aggregates.
You can see example of **code-first** Entity definition as below;

```csharp
 public class Customer : Entity
    {
        public Customer()
        {
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }

        public static Customer Create(int customerId, string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, string email, string BankAccountNumber)
        {
            var customer = new Customer
            {
                Id = customerId,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                PhoneNumber = phoneNumber,
                Email = email,
                BankAccountNumber = BankAccountNumber,
            };
            return customer;
        }
    }
```
Applying domain driven approach, Customer class responsible to create Customer instance. 

#### Interfaces
Abstraction of Repository - Domain repositories (IAsyncRepository - ICustomerRepository) - Specifications etc.. This interfaces include database operations without any application and ui responsibilities.

#### Specifications
This folder is implementation of **[specification pattern](https://en.wikipedia.org/wiki/Specification_pattern)**. Creates custom scripts with using **ISpecification** interface. Using BaseSpecification managing Criteria, Includes, OrderBy, Paging.
This specs runs when EF commands working with passing spec. This specs implemented SpecificationEvaluator.cs and creates query to crudTestApplicationRepository.cs in ApplySpecification method.This helps create custom queries.

### Infrastructure Layer
Implementation of Core interfaces in this project with **Entity Framework Core** and other dependencies.
Most of your application's dependence on external resources should be implemented in classes defined in the Infrastructure project. These classes must implement the interfaces defined in Core. If you have a very large project with many dependencies, it may make sense to have more than one Infrastructure project (eg Infrastructure.Data), but in most projects one Infrastructure project that contains folders works well.
This could be includes, for example, **e-mail providers, file access, web api clients**, etc. For now this repository only dependend sample data access and basic domain actions, by this way there will be no direct links to your Core or UI projects.

#### Data
Includes **Entity Framework Core Context** and tables in this folder. When new entity created, it should add to context and configure in context.
The Infrastructure project depends on Microsoft.**EntityFrameworkCore.SqlServer** and EF.Core related nuget packages, you can check nuget packages of Infrastructure layer. If you want to change your data access layer, it can easily be replaced with a lighter-weight ORM like Dapper. 

#### Migrations
EF add-migration classes.
#### Repository
EF Repository and Specification implementation. This class responsible to create queries, includes, where conditions etc..
#### Services
Custom services implementation, like email, cron jobs etc.

### Application Layer
Development of **Domain Logic with implementation**. Interfaces drives business requirements and implementations in this layer.
Application layer defines that user required actions in app services classes as below way;

```csharp
public interface ICustomerAppService
{
    Task<IEnumerable<CustomerDto>> GetCustomerList();
    Task<CustomerDto> GetCustomerById(int customerId);
    Task<IEnumerable<CustomerDto>> GetCustomerByName(string customerName);
    Task<CustomerDto> Create(CustomerDto entityDto);
    Task Update(CustomerDto entityDto);
    Task Delete(CustomerDto entityDto);
}
```
Also implementation located same places in order to choose different implementation at runtime when DI bootstrapped.
```csharp
public class CustomerAppService : ICustomerAppService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IAppLogger<CustomerAppService> _logger;

    public CustomerAppService(ICustomerRepository customerRepository, IAppLogger<CustomerAppService> logger)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<CustomerDto>> GetCustomerList()
    {
        var customerList = await _customerRepository.GetCustomerListAsync();
        var mapped = ObjectMapper.Mapper.Map<IEnumerable<CustomerDto>>(customerList);
        return mapped;
    }
}
```
In this layer we can add validation , authorization, logging, exception handling etc. -- cross cutting activities should be handled in here.

### Web Layer
Development of UI Logic with implementation. Interfaces drives business requirements and implementations in this layer.
The application's main **starting point** is the ASP.NET Core web project. This is a classical console application, with a public static void Main method in Program.cs. It currently uses the default **ASP.NET Core project template** which based on **Razor Pages** templates. This includes appsettings.json file plus environment variables in order to stored configuration parameters, and is configured in Startup.cs.

Web layer defines that user required actions in page services classes as below way;
```csharp
public interface ICustomerPageService
{
    Task<IEnumerable<CustomerViewModel>> GetCustomers(string customerName);
    Task<CustomerViewModel> GetCustomerById(int customerId);
    Task<CustomerViewModel> CreateCustomer(CustomerViewModel customerViewModel);
    Task UpdateCustomer(CustomerViewModel customerViewModel);
    Task DeleteCustomer(CustomerViewModel customerViewModel);
}
```
Also implementation located same places in order to choose different implementation at runtime when DI bootstrapped.
```csharp
public class CustomerPageService : ICustomerPageService
{
    private readonly ICustomerAppService _customerAppService;;
    private readonly IMapper _mapper;
    private readonly ILogger<CustomerPageService> _logger;

    public CustomerPageService(ICustomerAppService customerAppService, IMapper mapper, ILogger<CustomerPageService> logger)
    {
        _customerAppService = customerAppService ?? throw new ArgumentNullException(nameof(customerAppService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<CustomerViewModel>> GetCustomers(string customerName)
    {
        if (string.IsNullOrWhiteSpace(customerName))
        {
            var list = await _customerAppService.GetCustomerList();
            var mapped = _mapper.Map<IEnumerable<CustomerViewModel>>(list);
            return mapped;
        }

        var listByName = await _customerAppService.GetCustomerByName(customerName);
        var mappedByName = _mapper.Map<IEnumerable<CustomerViewModel>>(listByName);
        return mappedByName;
    }
}
```
### Test Layer
For each layer, there is a test project which includes intended layer dependencies and mock classes. So that means Core-Application-Infrastructure and Web layer has their own test layer. By this way this test projects also divided by **unit, functional and integration tests** defined by in which layer it is implemented. 
Test projects using **xunit and Mock libraries**.  xunit, because that's what ASP.NET Core uses internally to test the customer. Moq, because perform to create fake objects clearly and its very modular.


## Technologies
* .NET Core 7.0
* ASP.NET Core 7.0
* Entity Framework Core 7.0 
* .NET Core Native DI
* Razor Pages
* AutoMapper

## Architecture
* Clean Architecture
* Full architecture with responsibility separation of concerns
* SOLID and Clean Code
* Domain Driven Design (Layers and Domain Model Pattern)
* Unit of Work
* Repository and Generic Repository
* Multiple Page Web Application (MPA)
* Monolitic Deployment Architecture
* Specification Pattern

## Disclaimer

* This repository is not intended to be a definitive solution.
* This repository not implemented a lot of 3rd party packages, we are try to avoid the over engineering when building on best practices.
* Beware to use in customerion way.


This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
