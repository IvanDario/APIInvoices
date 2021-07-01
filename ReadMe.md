# Invoices and Credit Notes API
> Invoice and Credit Notes Repository

This API expose three controllers, for Accounting Documents.

Invoices:
/api/Invoices
[Example]https://apiinvoicesapi.azure-api.net/api/Invoices

Credit Notes:
/api/CreditNotes
[Example]https://apiinvoicesapi.azure-api.net/api/Invoices

Documents: Documents are the Invoices and Credit Notes aggregation, this objects is GET only
/api/Documents
[Example]https://apiinvoicesapi.azure-api.net/api/Documents

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
2. Update the Connection String in appsettings.json [HERE](https://github.com/IvanDario/APIInvoices/blob/master/APIInvoices/appsettings.json#L10-L12)


## TASKS

This project is the solution for a technical test with the followin Tasks:

1. Expose an API which takes in a batch of invoices and stores them in mysql and returns that back in response
 ``
 **SOLUTION:**

    /api/Invoices - POST

    The parameter is an Invoice Array.  It ignores CreatedAt, Id fields.

    When success returns an Invoice Array with the same information but the Id and CreatedAt fields populated after saving then in the Database 

    ***InvoiceArray
    {
    "type": "array",
    "items": {
        "$ref": "#/definitions/Invoice"
    }
    ***

``

2. Expose another API which takes in a batch of credit notes and stores them in mysql and returns that back in response 

3. Expose another API which returns back an aggregated list of the data stored in invoice and credit note as a single list sorted by createdAt


Following the REST standard those operations and definitions were implemented:

### OPERATIONS


### DEFINITIONS



## Links

Live Example:

https://apiinvoicesapi.azure-api.net/

This code was published using Azure and SQL Azure, with no security or authetincation to be tested.


## Licensing

The code in this project is licensed under MIT license.