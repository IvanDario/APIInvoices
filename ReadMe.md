# Invoices and Credit Notes API
> Invoice and Credit Notes Repository
</br>
This API exposes three controllers, for Accounting Documents.
</br>
Invoices:
/api/Invoices
</br>
https://apiinvoicesapi.azure-api.net/api/Invoices

Credit Notes:
/api/CreditNotes
</br>
https://apiinvoicesapi.azure-api.net/api/CreditNotes

Documents: Documents are the Invoices and Credit Notes aggregation, this object is GET only
/api/Documents
</br>
https://apiinvoicesapi.azure-api.net/api/Documents

## Installing / Getting started

This is a Visual Studio Project, which includes the API Code, also a Client Library that could be used as an example for how to consume the API or a package with the set of objects to do easy to use for other components or applications.

After downloading the project, it will install the required nugget package, and you will see: 
 ```shell
Solution
├── APIInvoices                   # API Source
├── ClientInvoices                # A Client Library which consumes the API
├── Models                        # Classes used in the project
└── TestUnits                     # Automated tests
│  ├── ClientTest                          # This Test is an integration Test, using the Client
│  ├── CreditNoteControllerTest            # Unit Test Methods for the CreditNoteControllers
│  ├── DocumentControllerTest              # Unit Test Methods for the DocumentControllers
│  ├── InMemoryCreditNotesControllerTest   # Test Class implementing CreditNote Test Units in local memory space
│  ├── InMemoryDocumentsControllerTest     # Test Class implementing Documents Test Units in local memory space
│  ├── InMemoryInvoicesControllerTest      # Test Class implementing Invoices Test Units in local memory space
│  └── InvoiceControllerTest               # Unit Test Methods for the InvoiceControllers
└── scriptAPIDb.sql                 # SQL Server Script to create required tables.
 ```

For running the project it is required to create a database, The API use LINQ, which makes it easy to connect with different database engines, but it was tested with SQL Server, and it is included a script to replicate the database.

### Initial Configuration

1. Create a database and run the scripts (or use database-update in Visual Studio to use code-first and create the tables after updating the connection string).
2. Update the Connection String in appsettings.json [HERE](https://github.com/IvanDario/APIInvoices/blob/master/APIInvoices/appsettings.json#L10-L12)


## TASKS

This project is the solution for a technical test with the following Tasks:

*1. Expose an API which takes in a batch of invoices and stores them in mysql and returns that back in response*
<br>

 **SOLUTION:**
 ```shell
 

    /api/Invoices - POST

    The parameter is a CreditNotes Array.  It ignores "CreatedAt" and "Id" fields.

    When success returns an Invoice Array with the same information but the "Id" and "CreatedAt" fields populated after storing in the Database.

    CreditNoteArray
    {
    "type": "array",
    "items": {
        "$ref": "#/definitions/CreditNote"
    }
```

*2. Expose another API which takes in a batch of credit notes and stores them in mysql and returns that back in response* 
<br>

 **SOLUTION:**
 ```shell
 

    /api/CreditNotes - POST

    The parameter is an Invoice Array.  It ignores CreatedAt, Id fields.

    When success returns an Invoice Array with the same information but the "Id" and "CreatedAt" fields populated after storing in the Database.

    InvoiceArray
    {
    "type": "array",
    "items": {
        "$ref": "#/definitions/Invoice"
    }
```

*3. Expose another API which returns back an aggregated list of the data stored in invoice and credit note as a single list sorted by createdAt
<br>

 **SOLUTION:**
 ```shell
 

    /api/Documents - GET

    It doesn't require any parameter.
  
    When success returns a DocumentsViewModelArraywith the information of all the data stored in invoice and credit note lists sorted by CreatedAt and DocumentNumber.

    DocumentsViewModelArray
    {
        "type": "array",
        "items": {
            "$ref": "#/definitions/DocumentsViewModel"
        }
    }
```

Following the REST standard those operations and definitions were implemented:

### OPERATIONS


<ul class="list-group" role="list"><!----><li role="presentation" class="list-group-item level-1"><div class="list-group-item-container expanded active" role="presentation" name="/api/CreditNotes - GET"><div class="list-group-item-heading flex-container" role="presentation"><!----><div class="list-group-item-label flex-item flex-grow" role="presentation"><!----><a class="text-overflow" href="#" role="listitem" data-method="GET" title="/api/CreditNotes - GET"> /api/CreditNotes - GET <!----></a><!----></div><!----><div dropdown-box=""><a class="btn btn-link no-padding" dropdown-box-toggle="" href="#" role="button" title="Operation context menu" aria-label="Operation /api/CreditNotes - GET context menu"><i class="icon icon-option-horizontal"></i></a><!----><!----><div class="dropdown-menu context-menu" role="menu"><!---->
</div><!----></div></div></div><!----></li><li role="presentation" class="list-group-item level-1"><div class="list-group-item-container" role="presentation" name="/api/CreditNotes - POST"><div class="list-group-item-heading flex-container" role="presentation"><!----><div class="list-group-item-label flex-item flex-grow" role="presentation"><!----><a class="text-overflow" href="#" role="listitem" data-method="POST" title="/api/CreditNotes - POST"> /api/CreditNotes - POST <!----></a><!----></div><!----><div dropdown-box=""><a class="btn btn-link no-padding" dropdown-box-toggle="" href="#" role="button" title="Operation context menu" aria-label="Operation /api/CreditNotes - POST context menu"><i class="icon icon-option-horizontal"></i></a><!----><!---->
</div></div></div><!----></li><li role="presentation" class="list-group-item level-1"><div class="list-group-item-container" role="presentation" name="/api/CreditNotes/{id} - DELETE"><div class="list-group-item-heading flex-container" role="presentation"><!----><div class="list-group-item-label flex-item flex-grow" role="presentation"><!----><a class="text-overflow" href="#" role="listitem" data-method="DELETE" title="/api/CreditNotes/{id} - DELETE"> /api/CreditNotes/{id} - DELETE <!----></a><!----></div><!----><div dropdown-box=""><a class="btn btn-link no-padding" dropdown-box-toggle="" href="#" role="button" title="Operation context menu" aria-label="Operation /api/CreditNotes/{id} - DELETE context menu"><i class="icon icon-option-horizontal"></i></a><!----><!----></div></div></div><!----></li><li role="presentation" class="list-group-item level-1"><div class="list-group-item-container" role="presentation" name="/api/CreditNotes/{id} - GET"><div class="list-group-item-heading flex-container" role="presentation"><!----><div class="list-group-item-label flex-item flex-grow" role="presentation"><!----><a class="text-overflow" href="#" role="listitem" data-method="GET" title="/api/CreditNotes/{id} - GET"> /api/CreditNotes/{id} - GET <!----></a><!----></div><!----><div dropdown-box=""><a class="btn btn-link no-padding" dropdown-box-toggle="" href="#" role="button" title="Operation context menu" aria-label="Operation /api/CreditNotes/{id} - GET context menu"><i class="icon icon-option-horizontal"></i></a><!----><!----></div></div></div><!----></li><li role="presentation" class="list-group-item level-1"><div class="list-group-item-container" role="presentation" name="/api/CreditNotes/{id} - PUT"><div class="list-group-item-heading flex-container" role="presentation"><!----><div class="list-group-item-label flex-item flex-grow" role="presentation"><!----><a class="text-overflow" href="#" role="listitem" data-method="PUT" title="/api/CreditNotes/{id} - PUT"> /api/CreditNotes/{id} - PUT <!----></a><!----></div><!----><div dropdown-box=""><a class="btn btn-link no-padding" dropdown-box-toggle="" href="#" role="button" title="Operation context menu" aria-label="Operation /api/CreditNotes/{id} - PUT context menu"><i class="icon icon-option-horizontal"></i></a><!----><!----></div></div></div><!----></li><li role="presentation" class="list-group-item level-1"><div class="list-group-item-container" role="presentation" name="/api/Documents - GET"><div class="list-group-item-heading flex-container" role="presentation"><!----><div class="list-group-item-label flex-item flex-grow" role="presentation"><!----><a class="text-overflow" href="#" role="listitem" data-method="GET" title="/api/Documents - GET"> /api/Documents - GET <!----></a><!----></div><!----><div dropdown-box=""><a class="btn btn-link no-padding" dropdown-box-toggle="" href="#" role="button" title="Operation context menu" aria-label="Operation /api/Documents - GET context menu"><i class="icon icon-option-horizontal"></i></a><!----><!----></div></div></div><!----></li><li role="presentation" class="list-group-item level-1"><div class="list-group-item-container" role="presentation" name="/api/Invoices - GET"><div class="list-group-item-heading flex-container" role="presentation"><!----><div class="list-group-item-label flex-item flex-grow" role="presentation"><!----><a class="text-overflow" href="#" role="listitem" data-method="GET" title="/api/Invoices - GET"> /api/Invoices - GET <!----></a><!----></div><!----><div dropdown-box=""><a class="btn btn-link no-padding" dropdown-box-toggle="" href="#" role="button" title="Operation context menu" aria-label="Operation /api/Invoices - GET context menu"><i class="icon icon-option-horizontal"></i></a><!----><!----></div></div></div><!----></li><li role="presentation" class="list-group-item level-1"><div class="list-group-item-container" role="presentation" name="/api/Invoices - POST"><div class="list-group-item-heading flex-container" role="presentation"><!----><div class="list-group-item-label flex-item flex-grow" role="presentation"><!----><a class="text-overflow" href="#" role="listitem" data-method="POST" title="/api/Invoices - POST"> /api/Invoices - POST <!----></a><!----></div><!----><div dropdown-box=""><a class="btn btn-link no-padding" dropdown-box-toggle="" href="#" role="button" title="Operation context menu" aria-label="Operation /api/Invoices - POST context menu"><i class="icon icon-option-horizontal"></i></a><!----><!----></div></div></div><!----></li><li role="presentation" class="list-group-item level-1"><div class="list-group-item-container" role="presentation" name="/api/Invoices/{id} - DELETE"><div class="list-group-item-heading flex-container" role="presentation"><!----><div class="list-group-item-label flex-item flex-grow" role="presentation"><!----><a class="text-overflow" href="#" role="listitem" data-method="DELETE" title="/api/Invoices/{id} - DELETE"> /api/Invoices/{id} - DELETE <!----></a><!----></div><!----><div dropdown-box=""><a class="btn btn-link no-padding" dropdown-box-toggle="" href="#" role="button" title="Operation context menu" aria-label="Operation /api/Invoices/{id} - DELETE context menu"><i class="icon icon-option-horizontal"></i></a><!----><!----></div></div></div><!----></li><li role="presentation" class="list-group-item level-1"><div class="list-group-item-container" role="presentation" name="/api/Invoices/{id} - GET"><div class="list-group-item-heading flex-container" role="presentation"><!----><div class="list-group-item-label flex-item flex-grow" role="presentation"><!----><a class="text-overflow" href="#" role="listitem" data-method="GET" title="/api/Invoices/{id} - GET"> /api/Invoices/{id} - GET <!----></a><!----></div><!----><div dropdown-box=""><a class="btn btn-link no-padding" dropdown-box-toggle="" href="#" role="button" title="Operation context menu" aria-label="Operation /api/Invoices/{id} - GET context menu"><i class="icon icon-option-horizontal"></i></a><!----><!----></div></div></div><!----></li><li role="presentation" class="list-group-item level-1"><div class="list-group-item-container expanded" role="presentation" name="/api/Invoices/{id} - PUT"><div class="list-group-item-heading flex-container" role="presentation"><!----><div class="list-group-item-label flex-item flex-grow" role="presentation"><!----><a class="text-overflow" href="#" role="listitem" data-method="PUT" title="/api/Invoices/{id} - PUT"> /api/Invoices/{id} - PUT <!----></a><!----></div><!----><div dropdown-box=""><a class="btn btn-link no-padding" dropdown-box-toggle="" href="#" role="button" title="Operation context menu" aria-label="Operation /api/Invoices/{id} - PUT context menu"><i class="icon icon-option-horizontal"></i></a><!----><!----></div></div></div><!----></li></ul>

### DEFINITIONS

CreditNote
```JSON
{
    "type": "object",
    "properties": {
        "id": {
            "format": "uuid",
            "type": "string"
        },
        "value": {
            "format": "double",
            "type": "number"
        },
        "createdAt": {
            "format": "int64",
            "type": "integer"
        },
        "creditNumber": {
            "maxLength": 255,
            "minLength": 0,
            "type": "string"
        }
    }
}
```

CreditNoteArray

```JSON
{
    "type": "array",
    "items": {
        "$ref": "#/definitions/CreditNote"
    }
}
```

DocumentsViewModel
```JSON
{
    "type": "object",
    "properties": {
        "id": {
            "format": "uuid",
            "type": "string"
        },
        "value": {
            "format": "double",
            "type": "number"
        },
        "createdAt": {
            "format": "int64",
            "type": "integer"
        },
        "documentNumber": {
            "type": "string"
        },
        "documentType": {
            "type": "string"
        }
    }
}
```
DocumentsViewModelArray
```JSON
{
    "type": "array",
    "items": {
        "$ref": "#/definitions/DocumentsViewModel"
    }
}
```

Invoice
```JSON
{
    "type": "object",
    "properties": {
        "id": {
            "format": "uuid",
            "type": "string"
        },
        "value": {
            "format": "double",
            "type": "number"
        },
        "createdAt": {
            "format": "int64",
            "type": "integer"
        },
        "invoiceNumber": {
            "maxLength": 255,
            "minLength": 0,
            "type": "string"
        }
    }
}
```



## Links

Live Example:

https://apiinvoicesapi.azure-api.net/

This code was published using Azure and SQL Azure, with no security or authentication to be tested.


## Licensing

The code in this project is licensed under MIT license.
