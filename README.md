# BRTF Booking

BRTF Booking is an [MVC 5](https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/getting-started) web application for the [Broadcasting Radio Television and Film](https://www.niagaracollege.ca/media/program/broadcasting-film-production/#overview) program at [Niagara College](https://www.niagaracollege.ca/)

## Installation

Use the solution file to build and publish to [Azure](https://azure.microsoft.com/en-ca/)

```
./msbuild.exe BRTF_Booking
```

## Migrations

```python
Add-Migration -Context BRTFContext -o Data\BRTFMigrations Initial
Update-Database -Context BRTFContext 
Update-Database -Context ApplicationDbContext
```

## Contributing
Pull requests are private. Only those added to the project may make changes.

Please make sure to update tests as appropriate.
