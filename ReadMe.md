# Invoices and Credit Notes API
> Invoice and Credit Notes Repository

This API expose three controllers, for Accounting Documents.

Invoices:
/api/Invoices

Credit Notes:
/api/CreditNotes

Documents: Documents are the Invoices and Credit Notes aggregation, this objects is GET only
/api/Documents


## Installing / Getting started

This is a Visual Studio Project, wich includes the API Code, also a Client Library that could be used as an example for how to counsume the API or a package with the set of objects to do easy to use for another components or applications.

After download the project, it will install the required nugget package, and you will see 

Solution
├── APIInvoices                   # API Source
├── ClientInvoices                # A Client Library which consumes the API
├── Models                        # Classes used in the project
└── TestUnits                     # Automated tests 
|  ├── ClientTest                          # This Test is an integration Test, using the Client
|  ├── CreditNoteControllerTest            # Unit Test Methods for the CreditNoteControllers
|  ├── DocumentControllerTest              # Unit Test Methods for the DocumentControllers
|  ├── InMemoryCreditNotesControllerTest   # Test Class implementing CreditNote Test Units in local memory space
|  ├── InMemoryDocumentsControllerTest     # Test Class implementing Documents Test Units in local memory space
|  ├── InMemoryInvoicesControllerTest      # Test Class implementing Invoices Test Units in local memory space
|  └── InvoiceControllerTest               # Unit Test Methods for the InvoiceControllers
└── scriptAPIDb.sql                 # SQL Server Script to create required tables.

For running the project it is required to create a database, The API use LINQ, which makes it easy to connect with different data base engines, but it was tested with SQL Server, and it is included a script to replicate the data base.

### Initial Configuration

1. Create a database and run the scripts (or use database-update in Visual Studio to use code-first and create the tables after updating the connection string).
2. Update the Connection String in appsettings.json

https://github.com/IvanDario/APIInvoices/blob/master/APIInvoices/appsettings.json#L10-L12


## Developing

xxx:


### Building

If your project needs some additional steps for the developer to build the
xxct after some code changes, state them here:

```shell
./configure
make
make install
```

Here again you should state what actually happens when the code above gets
executed.

### Deploying / Publishing

xxx


## Features

What's all the bells and whistles this project can perform?
* What's the main functionality
* You can also do another thing
* If you get really randy, you can even do this

## Configuration

gge you should write what are all of the configurations a user can enter when
using the project.

#### Argument 1
Type: `String`  
Default: `'default value'`

State what an argument does and how you can use it. If needed, you can provide
an example below.

Example:
```bash
awesome-project "Some other value"  # Prints "You're nailing this readme!"
```

#### Argument 2
Type: `Number|Boolean`  
Default: 100

Copy-paste as many of these as you need.


## Links

Live Example:


## Licensing

The code in this project is licensed under MIT license.