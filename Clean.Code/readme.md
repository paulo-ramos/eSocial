### The net 8 DDD Clean attitecture solution

#### Requiredment

- Redis: latest
- RabbitMQ: latest
- Net 8

#### Setup

- Change solution name + project name (if needed)
- Init or update database:

```console
cd ./DDDTemplate.Persistence/ 
```

- Remove migrations (if needed):

```console
rm -rf ./Migrations
```

- Create new Migrations + update database (database connection string need be changed in ./DbContext.cs):

```console
dotnet ef migrations add *Name_of_migrations*
```

```console
dotnet ef database update
```

#### Running

- Runing redis and rabbit MQ (config can be change in ./DDDTemplate/WebApi/appsettings.json or
  appsettings.Development.json)
- Runing project to initial data (system data, user,...)

#### Default status codes:

`200 Ok (GET - HTTP GET, LIST - HTTP GET, INIT - HTTP GET,...):` các func get data, api call successfully, có dữ liệu
trả về.

`201 Created (CREATE - HTTP POST):` các func tạo

`204 No Content (UPDATE - HTTP PATCH, DELETE - HTTP DELETE):` các func update

`400 Bad Request:` Lỗi validate dữ liệu dữ liệu trả về dạng:

```json lines
{
  "type": "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "instance": "The customer status is not found.",
  // message
  "traceId": "CustomerStatus.NotFound",
  "errors": []
}

```

`401 Unauthorized:` Access denied.
`403 Forbidden:` Access denied.

`500 Internal Server Error:` exception threw.

#### Default Models

- `PageList (tất cả các function render list, select2,...)`
    - *requestAction: string*:
        - `empty`: tìm theo list (Ids're not required)
        - `find_by_values` : tìm theo từng thành phần (Ids required)
    - *page: int - required*: bắt đầu từ 1
    - *pageSize: int - required*: số record hiển thị trên page
    - *sortDir: string*: sắp xếp theo
    - *sortBy: string*: asc or desc

#### Default Error Messages

**ValidationErrors** <br/>

- **ValidationError** : `This is required field.` <br/>
- **ValidationError.IsFormatWrong** : `This is wrong format.` <br/>
- **ValidationError.MaximumLength** : `This is over maximum length. Max: {max} characters.` <br/>
- **ValidationError.GreaterThan**: `This is must greater than {number}.` <br/>

**ColorErrors**  <br/>

- **Color.NullOrEmpty**: `Color is null or empty.` <br/>
- **Color.InvalidFormat**: `Color format is invalid.` <br/>
- **Color.MaxLength**: `Color length must be less than or equal {MaxLength:D0} characters.` <br/>

**EmailAddressErrors**  <br/>

- **EmailAddress.NullOrEmpty**: `Email address is null or empty.` <br/>
- **EmailAddress.InvalidFormat**: `Email address format is invalid.` <br/>
- **EmailAddress.MaxLength**: `Email address length must be less than or equal {MaxLength:D0} characters.` <br/>

**HtmlErrors**  <br/>

- **HtmlErrors.NullOrEmpty**: `Html is null or empty.` <br/>

**IdCardNumberErrors**  <br/>

- **IdCardNumberErrors.NullOrEmpty**: `Id card number is null or empty.` <br/>
- **IdCardNumberErrors.InvalidFormat**: `Id card number format is invalid.` <br/>
- **IdCardNumberErrors.MaxLength**: `Id card number length must be less than or equal {MaxLength:D0} characters.` <br/>

**MobileNumberErrors**  <br/>

- **MobileNumberErrors.NullOrEmpty**: `Mobile number is null or empty`. <br/>
- **MobileNumberErrors.InvalidFormat**: `Mobile number format is invalid`. <br/>
- **MobileNumberErrors.MaxLength**: `Mobile number length must be less than or equal {MaxLength:D0} characters.` <br/>

**PasswordErrors**  <br/>

- **PasswordErrors.NullOrEmpty**: `Password is null or empty.` <br/>
- **PasswordErrors.InvalidFormat**: `Password format is invalid.` <br/>
- **PasswordErrors.MaxLength**: `Password length must be less than or equal {MaxLength:D0} characters.` <br/>
- **PasswordErrors.MinLength**: `Password length must be greater than or equal {MinLength:D0} characters.` <br/>

**TelephoneNumberErrors**  <br/>

- **TelephoneNumberErrors.NullOrEmpty**: `Telephone number is null or empty.` <br/>
- **TelephoneNumberErrors.InvalidFormat**: `Telephone number format is invalid.` <br/>
- **TelephoneNumberErrors.MaxLength
  **: `Telephone number length must be less than or equal {MaxLength:D0} characters.` <br/>

## WebAPI

- test case api is defined in ./http 

